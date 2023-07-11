using UnityEngine;
using System.IO;

/// <summary>
/// �÷��̾� ���� Ŭ����
/// </summary>
[System.Serializable]
public class Player : MonoBehaviour
{
    #region �÷��̾� �ɷ�ġ
    // �÷��̾� ����
    private byte level = 1;
    public byte Level
    {
        get { return level; }
        set
        {
            // ���� ���� ����
            if (level >= 1 && level <= maxLevel)
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
            if (exp >= 0 && exp <= maxExp)
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
            if (stamina >= 0 && stamina <= maxStamina)
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

    // �÷��̾��� ���¹̳��� ���ϴ� �ð�
    private float changeStaminaTime;
    // ���¹̳� ���� ����
    private bool staminaUsed;

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
    public byte MaxSatiety
    {
        get { return maxSatiety; }
    }

    // �÷��̾��� ����
    private byte quench;
    public byte Quench
    {
        get { return quench; }
        set
        {
            if (quench >= 0 && quench <= maxQuench)
                quench = value;
        }
    }

    // �÷��̾��� �ִ� ����
    private byte maxQuench = 100;
    public byte MaxQuench
    {
        get { return maxQuench; }
    }

    // �÷��̾��� ���� ü��, ���� ü��, �ִ� ü��
    private byte currentTemperature;
    public byte CurrentTemperature
    {
        get { return currentTemperature; }
        set
        {
            if (currentTemperature >= minTemperature && currentTemperature <= maxTemperature)
                currentTemperature = value;
        }
    }

    // �÷��̾��� ���� ü��
    private byte minTemperature = 0;
    public byte MinTemperature
    {
        get { return minTemperature; }
    }

    // �÷��̾��� �ִ� ü��
    private byte maxTemperature = 2;
    public byte MaxTemperature
    {
        get { return maxTemperature; }
    }

    // �÷��̾ �ٶ󺸴� ����
    // 0: ����, 1:����, 2: ���� 3: ����
    private byte direction;
    public byte Direction
    {
        get { return direction; }
        set
        {
            if (direction >= 0 && direction <= 3)
                direction = value;
        }
    }
    #endregion

    #region �÷��̾� �ɷ�ġ�� ��ȭ
    public void RecoverStaminaTime()
    {
        if (staminaUsed)
        {
            if (changeStaminaTime < 1)
                changeStaminaTime++;
            else
                staminaUsed = false;
        }
    }

    public void IncreaseStamina(byte addStamina)
    {
        if (!staminaUsed && Stamina < MaxStamina && !Input.GetKey(KeyCode.LeftAlt))
        {
            Stamina += addStamina;
        }
    }

    public void DecreaseStamina(byte subStamina)
    {
        staminaUsed = true;
        changeStaminaTime = 0;

        if (Stamina - subStamina > 0)
            Stamina -= subStamina;
        else
            Stamina = 0;
    }
    #endregion

    private bool canPlayerMove = true;
    public bool CanPlayerMove
    {
        get { return canPlayerMove; }
        set { canPlayerMove = value; }
    }


    private Vector3 vector3;
    public Vector3 Vector3
    {
        get { return vector3; }
    }

    // �÷��̾��� ���� �̵��ӵ�, �ȱ� �ӵ�, �޸��� �ӵ�
    private float speed = 0;
    private float walkSpeed = 15;
    private float runSpeed = 30;

    // �÷��̾��� �߷� ����
    private Rigidbody2D rb;

    // �÷��̾��� �ִϸ��̼��� ����
    private Animator animator;

    private PlayerUI playerUI;

    public SaveData[] saveData = new SaveData[4];

    void Move()
    {
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector3.x = Input.GetAxis("Horizontal");
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector3.y = Input.GetAxis("Vertical");

        if (Vector3 != Vector3.zero)
        {
            // AltŰ�� �����ְ� ���¹̳��� �����ִٸ�
            if (Input.GetKey(KeyCode.LeftAlt) && Stamina > 0)
            {
                // �޸���
                speed = runSpeed;
                // ���¹̳��� 1�� ���ҽ�Ų��.
                DecreaseStamina(1);
            }
            else
            {
                // �ȴ´�
                speed = walkSpeed;
            }

            // ���� Alt Ű�� �����鼭 ����Ű�� �����ϸ� �޷����� ������ ���� ��� �ȴ´�.
            // speed = Input.GetKey(KeyCode.LeftAlt) && Stamina != 0 ? runSpeed : walkSpeed;
            rb.MovePosition(transform.position + vector3 * speed * Time.deltaTime);
            animator.SetFloat("moveX", vector3.x);
            animator.SetFloat("moveY", vector3.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // ������ ���
            speed = 0;
            animator.SetBool("moving", false);
        }
    }

    /// <summary>
    /// ������ �����͸� ���� ���� �ҷ�����
    /// </summary>
    public void Load()
    {
        string[] jsonData = new string[4];
        jsonData[DataManager.instance.ClickSaveBtn]
            = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{DataManager.instance.ClickSaveBtn}" + ".json");
        saveData[DataManager.instance.ClickSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[DataManager.instance.ClickSaveBtn]);

        // �ҷ��� �����͵�
        Level = saveData[DataManager.instance.ClickSaveBtn].level;
        PlayerJob = saveData[DataManager.instance.ClickSaveBtn].job;
        Exp = saveData[DataManager.instance.ClickSaveBtn].exp;
        MaxExp = saveData[DataManager.instance.ClickSaveBtn].maxExp;
        Hp = saveData[DataManager.instance.ClickSaveBtn].hp;
        MaxHp = saveData[DataManager.instance.ClickSaveBtn].maxHp;
        Stamina = saveData[DataManager.instance.ClickSaveBtn].stamina;
        MaxStamina = saveData[DataManager.instance.ClickSaveBtn].maxStamina;
        Satiety = saveData[DataManager.instance.ClickSaveBtn].satiety;
        Quench = saveData[DataManager.instance.ClickSaveBtn].quench;
        CurrentTemperature = saveData[DataManager.instance.ClickSaveBtn].currentTemperature;
        Direction = saveData[DataManager.instance.ClickSaveBtn].direction;
    }
    
    /// <summary>
    /// ���� �� �����͸� ���Ϸ� �����ϱ�
    /// </summary>
    public void Save()
    {
        saveData[DataManager.instance.ClickSaveBtn].level = Level;
        saveData[DataManager.instance.ClickSaveBtn].job = PlayerJob;
        saveData[DataManager.instance.ClickSaveBtn].exp = Exp;
        saveData[DataManager.instance.ClickSaveBtn].maxExp = MaxExp;
        saveData[DataManager.instance.ClickSaveBtn].hp = Hp;
        saveData[DataManager.instance.ClickSaveBtn].maxHp = MaxHp;
        saveData[DataManager.instance.ClickSaveBtn].stamina = Stamina;
        saveData[DataManager.instance.ClickSaveBtn].maxStamina = MaxStamina;
        saveData[DataManager.instance.ClickSaveBtn].satiety = Satiety;
        saveData[DataManager.instance.ClickSaveBtn].quench = Quench;
        saveData[DataManager.instance.ClickSaveBtn].currentTemperature = CurrentTemperature;
        saveData[DataManager.instance.ClickSaveBtn].direction = Direction;

        string[] jsonData = new string[4];
        jsonData[DataManager.instance.ClickSaveBtn]
            = JsonUtility.ToJson(saveData[DataManager.instance.ClickSaveBtn]);
        File.WriteAllText
            (Application.persistentDataPath + "/SaveData" + $"{DataManager.instance.ClickSaveBtn}" + ".json", jsonData[DataManager.instance.ClickSaveBtn]);
    }
    
    public void StopGameTime()
    {
        if(CanPlayerMove == true)
            CanPlayerMove = false;
        // �ð� ����
        
    }

    public void MoveGameTime()
    {
        if(CanPlayerMove == false)
            CanPlayerMove = true;
        // �ð� ����
        
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerUI = GetComponent<PlayerUI>();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        MoveGameTime();
    }

    private void FixedUpdate()
    {
        if(CanPlayerMove == true)
            Move();

        RecoverStaminaTime();
        IncreaseStamina(1);
    }
}
