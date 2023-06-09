// 작성자: 이재윤
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어 관련 클래스
/// </summary>
[System.Serializable]
public class PlayerController : MonoBehaviour
{
    // 플레이어 능력치
    #region
    // 플레이어 레벨
    private byte level = 1;
    public byte Level
    {
        get { return level; }
        set
        {
            // 레벨 범위 제한
            if(level >= 1 && level <= maxLevel)
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
            if(exp >= 0 && exp <= maxExp)
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
            if(stamina >= 0 && stamina <= maxStamina)
                stamina = value;

            if(stamina > 0)
            {
                
            }
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

    // 플레이어의 수분
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
    
    // 플레이어의 최대 수분
    private byte maxQuench = 100;

    // 플레이어의 현재 체온, 최저 체온, 최대 체온
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

    // 플레이어의 최저 체온
    private byte minTemperature = 0;
    // 플레이어의 최대 체온
    private byte maxTemperature = 2;

    // 플레이어가 바라보는 방향
    // 0: 남쪽, 1:서쪽, 2: 북쪽 3: 동쪽
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
    #endregion

    // 플레이어 위치
    private Vector3 vector;
    public Vector3 Vector
    {
        get { return vector; }
        set { vector = value; }
    }

    // 플레이어의 현재 이동속도, 걷기 속도, 달리기 속도
    private float speed = 0;
    private float walkSpeed = 15;
    private float runSpeed = 30;


    // 플레이어 상태 UI
    // 0: 레벨, 1: 직업, 2: 경험치, 3: 생명력, 4: 스태미나, 5: 포만감, 6: 수분
    public TextMeshProUGUI[] statUI = new TextMeshProUGUI[7];
    // 0: 경험치, 1: 생명력, 2: 스태미나, 3: 포만감, 4: 수분, 5:체온
    public Slider[] barUI = new Slider[6];
    
    

    private EnvironmentManager environmentManager;

    // 플레이어의 중력 관할
    private Rigidbody2D rb;

    // 플레이어의 애니메이션을 관할
    private Animator animator;

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
        if(!staminaUsed && Stamina < MaxStamina && !Input.GetKey(KeyCode.LeftAlt))
        {
            Stamina += addStamina;
        }
    }

    public void DecreaseStamina(byte subStamina)
    {
        staminaUsed = true;
        changeStaminaTime = 0;

        if(Stamina - subStamina > 0)
            Stamina -= subStamina;
        else
            Stamina = 0;
    }

    public void IncreaseTemperature()
    {
        if(CurrentTemperature < environmentManager.temperature)
        {
            CurrentTemperature = environmentManager.temperature;
        }
    }

    public void DecreaseTemperature()
    {
        if(CurrentTemperature > environmentManager.temperature) 
        {
            CurrentTemperature = environmentManager.temperature;
        }
    }
    #endregion

    // 플레이어 변수에 저장된 값을 UI에 표시
    public void UIActivate()
    {
        // 0: 레벨, 1: 직업, 2: 경험치/최대 경험치, 3: 생명력/최대 생명력, 4: 스태미나/최대 스태미나
        // 5: 포만감/최대 포만감, 6: 수분/최대 수분
        statUI[0].text = "Lv: " + $"{level}";
        statUI[1].text = playerJob;
        statUI[2].text = "Exp: " + $"{exp}" + "/" + $"{maxExp}";
        statUI[3].text = "HP: " + $"{hp}" + "/" + $"{maxHp}";
        statUI[4].text = "ST: " + $"{stamina}" + "/" + $"{maxStamina}";
        statUI[5].text = "SA: " + $"{satiety}" + "/" + $"{maxSatiety}";
        statUI[6].text = "QU: " + $"{quench}" + "/" + $"{maxQuench}";

        // 0: 최대 경험치, 1: 최대 생명력, 2: 최대 스태미나, 3: 최대 포만감, 4: 최대 수분, 5: 최대 체온
        barUI[0].maxValue = MaxExp;
        barUI[1].maxValue = MaxHp;
        barUI[2].maxValue = MaxStamina;
        barUI[3].maxValue = maxSatiety;
        barUI[4].maxValue = maxQuench;
        barUI[5].maxValue = maxTemperature;

        // 0: 경험치, 1: 생명력, 2: 스태미나, 3: 포만감, 4: 수분, 5: 현재 체온
        barUI[0].value = Exp;
        barUI[1].value = Hp;
        barUI[2].value = Stamina;
        barUI[3].value = Satiety;
        barUI[4].value = Quench;
        barUI[5].value = currentTemperature;
    }

    // 플레이어 이동 시 애니메이션을 재생하는 파라미터를 조작하는 함수
    void UpdateAnimationAndMove()
    {
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");

        if (Vector != Vector3.zero)
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
            MoveCharacter();
            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // 정지한 경우
            speed = 0;
            animator.SetBool("moving", false);
        }
    }

    //플레이어 이동 함수
    void MoveCharacter() => rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    
    void Awake()
    {
        // 게임 시작 전 시간이 정상적으로 흐르게 한다.
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        environmentManager = GetComponent<EnvironmentManager>();
        
        
        //UIActivate();
    }

    private void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateAnimationAndMove();
        UIActivate();
        //Vector = this.transform.position;
        RecoverStaminaTime();
        IncreaseStamina(1);

        
        
    }
}
