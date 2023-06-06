// �ۼ���: ������, �ۼ�����: 2023-06-01
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using UnityEditor.UIElements;
using System.Resources;

/// <summary>
/// �÷��̾� ���� Ŭ����
/// </summary>
public class PlayerController : MonoBehaviour
{
    // �÷��̾� ������ �ִ� ����
    public byte level = 1;
    private byte maxLevel = 10;

    // �÷��̾� ����
    public string playerJob = "����";
    
    // �÷��̾��� ����ġ�� �ִ� ����ġ
    public ushort exp;
    public ushort maxExp;
    
    // �÷��̾��� ����°� �ִ� �����
    public byte hp;
    public byte maxHp;

    // �÷��̾��� ���¹̳��� �ִ� ���¹̳�
    public byte stamina;
    public byte maxStamina;

    // �÷��̾��� �������� �ִ� ������
    public byte satiety;
    private byte maxSatiety = 100;

    // �÷��̾��� ���а� �ִ� ����
    public byte quench;
    private byte maxQuench = 100;

    // �÷��̾��� ���� ü��, ���� ü��, �ִ� ü��
    public byte currrentTemperature;
    private byte minTemperature = 0;
    private byte maxTemperature = 2;

    // �÷��̾��� ���� �̵��ӵ�, �ȱ� �ӵ�, �޸��� �ӵ�
    private float speed = 0;
    private float walkSpeed = 120;
    private float runSpeed = 240;

    // �Ͻ����� ���� �˸�
    private bool pauseState = false;

    // �÷��̾� ���� UI
    // 0: ����, 1: ����, 2: ����ġ, 3: �����, 4: ���¹̳�, 5: ������, 6: ����
    public TextMeshProUGUI[] statUI = new TextMeshProUGUI[7];

    // �Ͻ����� �г�    
    public Image pausePanel;

    
    
    // ���̺� ���� ���� ����
    public SaveData[] saveData = new SaveData[4];
    // ���� ���̺� ������ ��ġ
    public sbyte currentSaveNum;

    // �÷��̾��� �߷� ����
    private Rigidbody2D rb;
    // �÷��̾� ��ġ�� ���ͷ� �޾� ����
    private Vector3 vector;
    // �÷��̾��� �ִϸ��̼��� ����
    private Animator animator;

    private AudioManager theAudio;

    /// <summary>
    /// �Ͻ����� �Լ�
    /// </summary>
    void Pause()
    {
        if (pauseState == false)
        {
            pausePanel.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseState = true;
                // �ð� ����
                Time.timeScale = 0;
            }

        }

        else
        {
            pausePanel.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseState = false;
                // �ð��� ���� ������ ��ȯ�� �帣�� �Ѵ�
                Time.timeScale = 1;
            }
        }
    }

    // �������� ���ư��� ��ư
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(true);
        pauseState = false;
        // �ð��� ���� ������ ��ȯ�� �帣�� �Ѵ�
        Time.timeScale = 1;
    }

    // ���� �������� ���� ��ư
    public void Pause_Setting()
    {
        
    }

    // ������ �����ϴ� ��ư
    public void Pause_SaveGame() => Save();
    
    // ������ �����ϰ� ������ Ÿ��Ʋ ȭ������ ���� ��ư
    public void Pause_SaveAndExitGame()
    {
        Save();
        SceneManager.LoadScene("_Title");
    }

    public void Load()
    {
        string[] jsonData = new string[4];
        jsonData[currentSaveNum]
            = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{currentSaveNum}" + ".txt");
        saveData[currentSaveNum] = JsonUtility.FromJson<SaveData>(jsonData[currentSaveNum]);
    }

    // ���ڿ��� ����� �����͸� �÷��̾��� ������ ���� ����
    
    public void LoadedInfoInput()
    {
        level = Convert.ToByte(saveData[currentSaveNum].level);
        playerJob = saveData[currentSaveNum].job;
        exp = Convert.ToUInt16(saveData[currentSaveNum].exp);
        maxExp = Convert.ToUInt16(saveData[currentSaveNum].maxExp);
        hp = Convert.ToByte(saveData[currentSaveNum].hp);
        maxHp = Convert.ToByte(saveData[currentSaveNum].maxHp);
        stamina = Convert.ToByte(saveData[currentSaveNum].stamina);
        maxStamina = Convert.ToByte(saveData[currrentTemperature].maxStamina);
        satiety = Convert.ToByte(saveData[currentSaveNum].satiety);
        quench = Convert.ToByte(saveData[currentSaveNum].quench);
        currentSaveNum = Convert.ToSByte(saveData[currentSaveNum].currentTemperature);

    }

    public void Save()
    {
        string[] jsonData = new string[4];
        jsonData[currentSaveNum]
            = JsonUtility.ToJson(saveData);
        File.WriteAllText
            (Application.persistentDataPath + "/SaveData" + $"{currentSaveNum}" + ".txt", jsonData[currentSaveNum]);
    }
    
    // �÷��̾� ������ ����� ���� UI�� ǥ��
    public void UIActivate()
    {
        // 0: ����, 1: ����, 2: ����ġ/�ִ� ����ġ, 3: �����/�ִ� �����, 4: ���¹̳�/�ִ� ���¹̳�
        // 5: ������/�ִ� ������, 6: ����/�ִ� ����
        statUI[0].text = "Lv: " + $"{level}";
        statUI[1].text = playerJob;
        statUI[2].text = "Exp: " + $"{exp}" + "/" + $"{maxExp}";
        statUI[3].text = "HP: " + $"{hp}" + "/" + $"{maxHp}";
        statUI[4].text = "ST: " + $"{stamina}" + "/" + $"{maxStamina}";
        statUI[5].text = "SA: " + $"{satiety}" + "/" + $"{maxSatiety}";
        statUI[6].text = "Lv: " + $"{quench}" + "/" + $"{maxQuench}";   
    }

    // �÷��̾� �̵� �� �ִϸ��̼��� ����ϴ� �Ķ���͸� �����ϴ� �Լ�
    void UpdateAnimationAndMove()
    {
        if (vector != Vector3.zero)
        {
            // ���� Alt Ű�� �����鼭 ����Ű�� �����ϸ� �޷����� ������ ���� ��� �ȴ´�.
            speed = Input.GetKey(KeyCode.LeftAlt) ? runSpeed : walkSpeed;
            MoveCharacter();
            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // ������ ���
            speed = 0;
            animator.SetBool("moving", false);
        }
    }

    //�÷��̾� �̵� �Լ�
    void MoveCharacter() => rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    
    void Awake()
    {
        // ���� ���� �� �ð��� ���������� �帣�� �Ѵ�.
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        

        Load();
        
        LoadedInfoInput();
        UIActivate();
    }

    // Update is called once per frame
    void Update()
    {
        vector = Vector3.zero;
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector.x = Input.GetAxis("Horizontal");
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector.y = Input.GetAxis("Vertical");
        
        UIActivate();
        UpdateAnimationAndMove();
        Pause();
    }
}
