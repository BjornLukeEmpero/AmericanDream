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

/// <summary>
/// 플레이어 관련 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 플레이어 레벨과 최대 레벨
    public sbyte level = 1;
    public sbyte maxLevel = 10;

    // 플레이어 직업
    public string playerJob = "무직";
    
    // 플레이어의 경험치와 최대 경험치
    public int exp;
    public int maxExp;
    
    // 플레이어의 생명력과 최대 생명력
    public short hp;
    public short maxHp;

    // 플레이어의 스태미나와 최대 스태미나
    public sbyte stamina;
    public sbyte maxStamina;

    // 플레이어의 포만감과 최대 포만감
    public sbyte satiety;
    public sbyte maxSatiety = 100;

    // 플레이어의 수분과 최대 수분
    public sbyte quench;
    public sbyte maxQuench = 100;

    // 플레이어의 현재 체온, 최저 체온, 최대 체온
    public sbyte currrentTemperature;
    public sbyte minTemperature = 0;
    public sbyte maxTemperature = 2;

    // 플레이어의 현재 이동속도, 걷기 속도, 달리기 속도
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

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
    // 플레이어 위치를 벡터로 받아 관할
    private Vector3 vector;
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

    // 게임으로 돌아가는 버튼
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(true);
        pauseState = false;
        // 시간을 정상 값으로 전환해 흐르게 한다
        Time.timeScale = 1;
    }

    // 설정 조작으로 들어가는 버튼
    public void Pause_Setting()
    {
        
    }

    // 게임을 저장하는 버튼
    public void Pause_SaveGame()
    {
        Save();
    }

    // 게임을 저장하고 종료해 타이틀 화면으로 가는 버튼
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

    // 문자열로 저장된 데이터를 플레이어의 변수에 각각 대입
    
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
        if (vector != Vector3.zero)
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
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }

    
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
        
        LoadedInfoInput();
        UIActivate();
    }

    // Update is called once per frame
    void Update()
    {
        vector = Vector3.zero;
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");
        
        UIActivate();
        UpdateAnimationAndMove();
        Pause();
    }
}
