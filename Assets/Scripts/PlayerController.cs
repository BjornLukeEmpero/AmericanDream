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
    public sbyte level = 1;
    public sbyte maxLevel = 10;

    // �÷��̾� ����
    public string playerJob = "����";
    
    // �÷��̾��� ����ġ�� �ִ� ����ġ
    public int exp;
    public int maxExp;
    
    // �÷��̾��� ����°� �ִ� �����
    public short hp;
    public short maxHp;

    // �÷��̾��� ���¹̳��� �ִ� ���¹̳�
    public sbyte stamina;
    public sbyte maxStamina;

    // �÷��̾��� �������� �ִ� ������
    public sbyte satiety;
    public sbyte maxSatiety = 100;

    // �÷��̾��� ���а� �ִ� ����
    public sbyte quench;
    public sbyte maxQuench = 100;

    // �÷��̾��� ���� ü��, ���� ü��, �ִ� ü��
    public sbyte currrentTemperature;
    public sbyte minTemperature = 0;
    public sbyte maxTemperature = 2;

    // �÷��̾��� ���� �̵��ӵ�, �ȱ� �ӵ�, �޸��� �ӵ�
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

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
    public void Pause_SaveGame()
    {
        Save();
    }

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
        level = Convert.ToSByte(saveData[currentSaveNum].level);
        playerJob = saveData[currentSaveNum].job;
        exp = Convert.ToSByte(saveData[currentSaveNum].exp);
        maxExp = Convert.ToSByte(saveData[currentSaveNum].maxExp);
        hp = Convert.ToInt16(saveData[currentSaveNum].hp);
        maxHp = Convert.ToInt16(saveData[currentSaveNum].maxHp);
        stamina = Convert.ToSByte(saveData[currentSaveNum].stamina);
        maxStamina = Convert.ToSByte(saveData[currrentTemperature].maxStamina);
        satiety = Convert.ToSByte(saveData[currentSaveNum].satiety);
        quench = Convert.ToSByte(saveData[currentSaveNum].quench);
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
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }

    
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
