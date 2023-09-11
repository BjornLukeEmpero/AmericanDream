using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum PlayerState
{
    idle, move, attack, die, stagger
}

/// <summary>
/// 플레이어 관련 클래스
/// </summary>
[System.Serializable]
public class Player : MonoBehaviour
{
    #region 플레이어 능력치
    // 플레이어 레벨
    private byte level = 1;
    public byte Level
    {
        get { return level; }
        set
        {
            // 레벨 범위 제한
            if (level >= 1 && level <= maxLevel)
                level = value;
        }
    }
    // 플레이어 최대 레벨
    private byte maxLevel = 10;

    // 플레이어 직업
    private string playerJob = "무직";
    public string PlayerJob
    {
        get { return playerJob; }
        set
        {
            playerJob = value;
        }
    }

    // 플레이어의 경험치
    private ushort exp;
    public ushort Exp
    {
        get { return exp; }
        set
        {
            // 경험치 값 범위 제한
            if (exp >= 0 && exp <= maxExp)
                exp = value;
        }
    }

    // 플레이어의 최대 경험치
    private ushort maxExp;
    public ushort MaxExp
    {
        get { return maxExp; }
        set
        {
            maxExp = value;
        }
    }

    // 플레이어의 생명력
    private byte hp;
    public byte Hp
    {
        get { return hp; }
        set
        {
            // 생명력 값 범위 제한
            if (hp >= 0 && hp <= maxHp)
                hp = value;
        }
    }

    // 플레이어의 최대 생명력
    private byte maxHp;
    public byte MaxHp
    {
        get { return maxHp; }
        set
        {
            maxHp = value;
        }
    }

    // 플레이어의 스태미나
    private byte stamina;
    public byte Stamina
    {
        get { return stamina; }
        set
        {
            // 스태미나 값 범위 제한
            if (stamina >= 0 && stamina <= maxStamina)
                stamina = value;

        }
    }

    // 플레이어의 최대 스태미나
    private byte maxStamina;
    public byte MaxStamina
    {
        get { return maxStamina; }
        set
        {
            maxStamina = value;
        }
    }

    // 플레이어의 스태미나가 변하는 시간
    private float changeStaminaTime;
    // 스태미나 감소 여부
    private bool staminaUsed;

    // 플레이어의 포만감
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

    // 플레이어의 최대 포만감
    private byte maxSatiety = 100;
    public byte MaxSatiety
    {
        get { return maxSatiety; }
    }

    // 플레이어의 수분
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

    // 플레이어의 최대 수분
    private byte maxQuench = 100;
    public byte MaxQuench
    {
        get { return maxQuench; }
    }

    // 플레이어의 현재 체온, 최저 체온, 최대 체온
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

    // 플레이어의 최저 체온
    private byte minTemperature = 0;
    public byte MinTemperature
    {
        get { return minTemperature; }
    }

    // 플레이어의 최대 체온
    private byte maxTemperature = 2;
    public byte MaxTemperature
    {
        get { return maxTemperature; }
    }

    // 플레이어가 바라보는 방향
    // 0: 남쪽, 1:서쪽, 2: 북쪽 3: 동쪽
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

    #region 플레이어 능력치의 변화
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

    // 플레이어의 현재 이동속도, 걷기 속도, 달리기 속도
    private float speed = 0;
    private float walkSpeed = 15;
    private float runSpeed = 30;

    // 플레이어 상태 관할
    public PlayerState playerState;

    // 플레이어의 중력 관할
    private Rigidbody2D rigidbody2D;

    // 플레이어의 애니메이션을 관할
    private Animator animator;

    private PlayerUI playerUI;

    public GameObject projectile;

    //public SaveData[] saveData = new SaveData[4];

    #region 플레이어 이동 함수
    void Move()
    {
        if (Vector3 != Vector3.zero)
        {
            // Alt키가 눌려있고 스태미나가 남아있다면
            if (Input.GetKey(KeyCode.LeftAlt) && Stamina > 0)
            {
                // 달린다
                speed = runSpeed;
                // 스태미나를 1씩 감소시킨다.
                DecreaseStamina(1);
            }
            else
            {
                // 걷는다
                speed = walkSpeed;
            }

            // 왼쪽 Alt 키를 누르면서 방향키로 조작하면 달려가며 누르지 않은 경우 걷는다.
            // speed = Input.GetKey(KeyCode.LeftAlt) && Stamina != 0 ? runSpeed : walkSpeed;
            rigidbody2D.MovePosition(transform.position + vector3 * speed * Time.deltaTime);
            animator.SetFloat("moveX", vector3.x);
            animator.SetFloat("moveY", vector3.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // 정지한 경우
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


    #region 근접공격
    private IEnumerator MeleeAttackCoruotine()
    {
        playerState = PlayerState.attack;
        yield return null;
        yield return new WaitForSeconds(1f);
        playerState = PlayerState.idle;
    }
    #endregion

    #region 원거리 공격
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
    /// 저장한 데이터를 게임 내로 불러오기
    /// </summary>
    public void Load()
    {
        string[] jsonData = new string[4];
        jsonData[DataManager.instance.ClickSaveBtn]
            = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{DataManager.instance.ClickSaveBtn}" + ".json");
        saveData[DataManager.instance.ClickSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[DataManager.instance.ClickSaveBtn]);

        // 불러올 데이터들
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
    /// 게임 내 데이터를 파일로 저장하기
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
        // 시간 정지
        
    }

    public void MoveGameTime()
    {
        if(CanPlayerMove == false)
            CanPlayerMove = true;
        // 시간 정지
        
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
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector3.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
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
