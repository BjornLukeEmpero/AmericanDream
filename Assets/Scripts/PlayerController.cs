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
using Unity.VisualScripting;

/// <summary>
/// �÷��̾� ���� Ŭ����
/// </summary>
[System.Serializable]
public class PlayerController : MonoBehaviour
{
    // �÷��̾� ����
    private byte level = 1;
    public byte Level
    {
        get { return level; }
        set
        {
            // ���� ���� ����
            if(level >= 1 && level <= maxLevel)
                level = value;
        }
    }
    // �÷��̾� �ִ� ����
    private byte maxLevel = 10;

    // �÷��̾� ����
    private string playerJob = "����";
    public string PlayerJob
    {
        get { return playerJob; }
        set
        {
            playerJob = value;
        }
    }
    
    // �÷��̾��� ����ġ
    private ushort exp;
    public ushort Exp
    {
        get { return exp; }
        set
        {
            // ����ġ �� ���� ����
            if(exp >= 0 && exp <= maxExp)
                exp = value;
        }
    }

    // �÷��̾��� �ִ� ����ġ
    private ushort maxExp;
    public ushort MaxExp
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
        }
    }
    
    // �÷��̾��� �����
    private byte hp;
    public byte Hp
    {
        get { return hp; }
        set
        {
            // ����� �� ���� ����
            if (hp >= 0 && hp <= maxHp)
                hp = value;
        }
    }

    // �÷��̾��� �ִ� �����
    private byte maxHp;
    public byte MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }

    // �÷��̾��� ���¹̳�
    private byte stamina;
    public byte Stamina
    {
        get { return stamina; }
        set
        {
            // ���¹̳� �� ���� ����
            if(stamina >= 0 && stamina <= maxStamina)
                stamina = value;
        }
    }

    // �÷��̾��� �ִ� ���¹̳�
    private byte maxStamina;
    public byte MaxStamina
    {
        get { return maxStamina; }
        set
        {
            maxStamina = value;
        }
    }

    // �÷��̾��� ������
    private byte satiety;
    public byte Satiety
    {
        get { return satiety; }
        set
        {
            if (satiety >= 0 && satiety <= maxSatiety)
                satiety = value;
        }
    }
    
    // �÷��̾��� �ִ� ������
    private byte maxSatiety = 100;

    // �÷��̾��� ����
    private byte quench;
    public byte Quench
    {
        get { return quench; }
        set
        {
            if(quench >= 0 &&  quench <= maxQuench)
                quench = value;
        }
    }
    
    // �÷��̾��� �ִ� ����
    private byte maxQuench = 100;

    // �÷��̾��� ���� ü��, ���� ü��, �ִ� ü��
    private byte currentTemperature;
    public byte CurrentTemperature
    {
        get { return currentTemperature; }
        set
        {
            if(currentTemperature >= minTemperature && currentTemperature <= maxTemperature)
                currentTemperature = value;
        }
    }

    // �÷��̾��� ���� ü��
    private byte minTemperature = 0;
    // �÷��̾��� �ִ� ü��
    private byte maxTemperature = 2;

    // �÷��̾ �ٶ󺸴� ����
    // 0: ����, 1:����, 2: ���� 3: ����
    private byte direction;
    public byte Direction
    {
        get { return direction; }
        set
        {
            if(direction >= 0 && direction <= 3)
                direction = value;
        }
    }

    // �÷��̾� ��ġ
    private Vector3 vector;
    public Vector3 Vector
    {
        get { return vector; }
        set { vector = value; }
    }

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

    /// <summary>
    /// �������� ���ư��� ��ư
    /// </summary>
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(true);
        pauseState = false;
        // �ð��� ���� ������ ��ȯ�� �帣�� �Ѵ�
        Time.timeScale = 1;
    }

    /// <summary>
    /// ���� �������� ���� ��ư
    /// </summary>
    public void Pause_Setting()
    {
        
    }

    /// <summary>
    /// ������ �����ϴ� ��ư
    /// </summary>
    public void Pause_SaveGame() => Save();
    
    /// <summary>
    /// ������ �����ϰ� ������ Ÿ��Ʋ ȭ������ ���� ��ư
    /// </summary>
    public void Pause_SaveAndExitGame()
    {
        Save();
        SceneManager.LoadScene("_Title");
    }

    /// <summary>
    /// ������ �����͸� ���� ���� �ҷ�����
    /// </summary>
    public void Load()
    {
        string[] jsonData = new string[4];
        jsonData[currentSaveNum]
            = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{currentSaveNum}" + ".json");
        saveData[currentSaveNum] = JsonUtility.FromJson<SaveData>(jsonData[currentSaveNum]);

        // �ҷ��� �����͵�
        Level = saveData[currentSaveNum].level;
        PlayerJob = saveData[currentSaveNum].job;
        Exp = saveData[currentSaveNum].exp;
        MaxExp = saveData[currentSaveNum].maxExp;
        Hp = saveData[currentSaveNum].hp;
        MaxHp = saveData[currentSaveNum].maxHp;
        Stamina = saveData[currentSaveNum].stamina;
        MaxStamina = saveData[currentSaveNum].maxStamina;
        Satiety = saveData[currentSaveNum].satiety;
        Quench = saveData[currentSaveNum].quench;
        CurrentTemperature = saveData[currentSaveNum].currentTemperature;
        Direction = saveData[currentSaveNum].direction;
        Vector = saveData[currentSaveNum].playerLocation;
    }

    /// <summary>
    /// ���� �� �����͸� ���Ϸ� �����ϱ�
    /// </summary>
    public void Save()
    {
        saveData[currentSaveNum].level = Level;
        saveData[currentSaveNum].job = PlayerJob;
        saveData[currentSaveNum].exp = Exp;
        saveData[currentSaveNum].maxExp = MaxExp;
        saveData[currentSaveNum].hp = Hp;
        saveData[currentSaveNum].maxHp = MaxHp;
        saveData[currentSaveNum].stamina = Stamina;
        saveData[currentSaveNum].maxStamina = MaxStamina;
        saveData[currentSaveNum].satiety = Satiety;
        saveData[currentSaveNum].quench = Quench;
        saveData[currentSaveNum].currentTemperature = CurrentTemperature;
        saveData[currentSaveNum].direction = Direction;
        saveData[currentSaveNum].playerLocation = Vector;
        
        string[] jsonData = new string[4];
        jsonData[currentSaveNum]
            = JsonUtility.ToJson(saveData);
        File.WriteAllText
            (Application.persistentDataPath + "/SaveData" + $"{currentSaveNum}" + ".json", jsonData[currentSaveNum]);
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
        if (Vector != Vector3.zero)
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
        UIActivate();
    }

    // Update is called once per frame
    void Update()
    {
        
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector.x = Input.GetAxis("Horizontal");
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector.y = Input.GetAxis("Vertical");

        //Vector = this.transform.position;
        
        UIActivate();
        UpdateAnimationAndMove();
        Pause();
    }
}
