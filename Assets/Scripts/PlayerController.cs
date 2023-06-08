// 작성자: 이재윤, 작성일자: 2023-06-01
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
/// 플레이어 관련 클래스
/// </summary>
[System.Serializable]
public class PlayerController : MonoBehaviour
{
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

    // 플레이어 위치
    private Vector3 vector;
    public Vector3 Vector
    {
        get { return vector; }
        set { vector = value; }
    }

    // 플레이어의 현재 이동속도, 걷기 속도, 달리기 속도
    private float speed = 0;
    private float walkSpeed = 120;
    private float runSpeed = 240;

    // 일시정지 상태 알림
    private bool pauseState = false;

    // 플레이어 상태 UI
    // 0: 레벨, 1: 직업, 2: 경험치, 3: 생명력, 4: 스태미나, 5: 포만감, 6: 수분
    public TextMeshProUGUI[] statUI = new TextMeshProUGUI[7];

    // 일시정지 패널    
    public Image pausePanel;

    
    
    // 세이브 파일 저장 변수
    public SaveData[] saveData = new SaveData[4];
    // 현재 세이브 파일의 위치
    public sbyte currentSaveNum;

    // 플레이어의 중력 관할
    private Rigidbody2D rb;

    // 플레이어의 애니메이션을 관할
    private Animator animator;

    private AudioManager theAudio;

    /// <summary>
    /// 일시정지 함수
    /// </summary>
    void Pause()
    {
        if (pauseState == false)
        {
            pausePanel.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseState = true;
                // 시간 정지
                Time.timeScale = 0;
            }
        }

        else
        {
            pausePanel.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseState = false;
                // 시간을 정상 값으로 전환해 흐르게 한다
                Time.timeScale = 1;
            }
        }
    }

    /// <summary>
    /// 게임으로 돌아가는 버튼
    /// </summary>
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(true);
        pauseState = false;
        // 시간을 정상 값으로 전환해 흐르게 한다
        Time.timeScale = 1;
    }

    /// <summary>
    /// 설정 조작으로 들어가는 버튼
    /// </summary>
    public void Pause_Setting()
    {
        
    }

    /// <summary>
    /// 게임을 저장하는 버튼
    /// </summary>
    public void Pause_SaveGame() => Save();
    
    /// <summary>
    /// 게임을 저장하고 종료해 타이틀 화면으로 가는 버튼
    /// </summary>
    public void Pause_SaveAndExitGame()
    {
        Save();
        SceneManager.LoadScene("_Title");
    }

    /// <summary>
    /// 저장한 데이터를 게임 내로 불러오기
    /// </summary>
    public void Load()
    {
        string[] jsonData = new string[4];
        jsonData[currentSaveNum]
            = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{currentSaveNum}" + ".json");
        saveData[currentSaveNum] = JsonUtility.FromJson<SaveData>(jsonData[currentSaveNum]);

        // 불러올 데이터들
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
    /// 게임 내 데이터를 파일로 저장하기
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
        statUI[6].text = "Lv: " + $"{quench}" + "/" + $"{maxQuench}";   
    }

    // 플레이어 이동 시 애니메이션을 재생하는 파라미터를 조작하는 함수
    void UpdateAnimationAndMove()
    {
        if (Vector != Vector3.zero)
        {
            
            
            // 왼쪽 Alt 키를 누르면서 방향키로 조작하면 달려가며 누르지 않은 경우 걷는다.
            speed = Input.GetKey(KeyCode.LeftAlt) ? runSpeed : walkSpeed;
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
        
        Load();
        UIActivate();
    }

    // Update is called once per frame
    void Update()
    {
        
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");

        //Vector = this.transform.position;
        
        UIActivate();
        UpdateAnimationAndMove();
        Pause();
    }
}
