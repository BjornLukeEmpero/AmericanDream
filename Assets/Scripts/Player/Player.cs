using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum PlayerState
{
    idle, move, attack, die, stagger
}

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

    // �÷��̾� ���� ����
    public PlayerState playerState;

    // �÷��̾��� �߷� ����
    private Rigidbody2D rigidbody2D;

    // �÷��̾��� �ִϸ��̼��� ����
    private Animator animator;

    private PlayerUI playerUI;

    public GameObject projectile;

    //public SaveData[] saveData = new SaveData[4];

    #region �÷��̾� �̵� �Լ�
    void Move()
    {
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
            rigidbody2D.MovePosition(transform.position + vector3 * speed * Time.deltaTime);
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
    #endregion

    public void Knockback(float knockbackTime)
    {
        StartCoroutine(KnockbackCoroutine(knockbackTime));
    }

    private IEnumerator KnockbackCoroutine(float knockbackTime)
    {
        if(rigidbody2D !=  null)
        {
            yield return new WaitForSeconds(knockbackTime);
            rigidbody2D.velocity = Vector2.zero;
            playerState = PlayerState.idle;
            rigidbody2D.velocity = Vector2.zero;
        }
    }


    #region ��������
    private IEnumerator MeleeAttackCoruotine()
    {
        playerState = PlayerState.attack;
        yield return null;
        yield return new WaitForSeconds(1f);
        playerState = PlayerState.idle;
    }
    #endregion

    #region ���Ÿ� ����
    private IEnumerator ProjectileAttackCoroutine()
    {
        animator.SetBool("attacking", true);
        playerState = PlayerState.attack;
        yield return null;
        MakeProjectile();
        animator.SetBool("attacking", false);
        playerState = PlayerState.idle;
        yield return new WaitForSeconds(0.5f);
        
    }
    #endregion

    #region
    private void MakeProjectile()
    {
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Projectile bullet = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        bullet.SetProjectile(temp, ChooseProjectileDirection());
    }
    #endregion

    #region
    Vector3 ChooseProjectileDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0,0,temp);
    }
    #endregion

    /*
    private IEnumerator Attack()
    {
        animator.SetBool("attacking", true);
        playerState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.5f);
        playerState = PlayerState.idle;
    }
    */

    /*
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
    */

    private void Awake()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.idle;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerUI = GetComponent<PlayerUI>();
        //Load();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //MoveGameTime();
        if (Input.GetKeyDown(KeyCode.LeftControl) && playerState != PlayerState.attack)
            StartCoroutine(Attack());*/
    }

    private void FixedUpdate()
    {
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector3.x = Input.GetAxis("Horizontal");
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector3.y = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.Space) && playerState != PlayerState.attack && playerState != PlayerState.stagger)
            StartCoroutine(MeleeAttackCoruotine());
        else if(Input.GetKeyDown(KeyCode.LeftControl) && playerState != PlayerState.attack && playerState != PlayerState.stagger)
            StartCoroutine(ProjectileAttackCoroutine());
        else if (playerState == PlayerState.idle)
            Move();

        RecoverStaminaTime();
        IncreaseStamina(1);
    }
}
