using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // 플레이어 상태 UI
    // 0: 레벨, 1: 직업, 2: 경험치, 3: 생명력, 4: 스태미나, 5: 포만감, 6: 수분
    //public TextMeshProUGUI[] statUI = new TextMeshProUGUI[7];
    
    // 0: 경험치, 1: 생명력, 2: 스태미나, 3: 포만감, 4: 수분, 5:체온
    //public Slider[] barUI = new Slider[6];
    // 일시정지 상태 알림
    private bool pauseState = false;
    public bool PauseState
    {
        get { return pauseState; }
        set { pauseState = value; }
    }
    // 일시정지 패널
    public Image pausePanel;

    private Player player;


    /*
    public void PlayerStatUIActivate()
    {
        // 0: 레벨, 1: 직업, 2: 경험치/최대 경험치, 3: 생명력/최대 생명력, 4: 스태미나/최대 스태미나
        // 5: 포만감/최대 포만감, 6: 수분/최대 수분
        statUI[0].text = "Lv: " + player.Level;
        statUI[1].text = player.PlayerJob;
        statUI[2].text = $"Exp: {player.Exp} / {player.MaxExp}";
        statUI[3].text = $"HP: {player.Hp} / {player.MaxHp}";
        statUI[4].text = $"ST: {player.Stamina} / {player.MaxStamina}";
        statUI[5].text = $"SA: {player.Satiety} / {player.MaxSatiety}";
        statUI[6].text = $"QU: {player.Quench} / {player.MaxQuench}";
    }

    // 플레이어 변수에 저장된 값을 UI에 표시
    public void PlayerUIActivate()
    {
        // 0: 최대 경험치, 1: 최대 생명력, 2: 최대 스태미나, 3: 최대 포만감, 4: 최대 수분, 5: 최대 체온
        barUI[0].maxValue = player.MaxExp;
        barUI[1].maxValue = player.MaxHp;
        barUI[2].maxValue = player.MaxStamina;
        barUI[3].maxValue = player.MaxSatiety;
        barUI[4].maxValue = player.MaxQuench;
        barUI[5].maxValue = player.MaxTemperature;

        // 0: 경험치, 1: 생명력, 2: 스태미나, 3: 포만감, 4: 수분, 5: 현재 체온
        barUI[0].value = player.Exp;
        barUI[1].value = player.Hp;
        barUI[2].value = player.Stamina;
        barUI[3].value = player.Satiety;
        barUI[4].value = player.Quench;
        barUI[5].value = player.CurrentTemperature;
    }
    */

    /// <summary>
    /// ESC키를 이용한 게임 일시정지
    /// </summary>
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseState == false)
        {
            pausePanel.gameObject.SetActive(true);
            PauseState = true;
            Time.timeScale = 0;
            //player.StopGameTime();
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && pauseState == true)
        {
            pausePanel.gameObject.SetActive(false);
            PauseState = false;
            Time.timeScale = 1;
            //player.MoveGameTime();
        }
    }

    /// <summary>
    /// 일시정지 패널의 게임으로 돌아가는 버튼
    /// </summary>
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(false);
        PauseState = false;
        //player.MoveGameTime();
    }

    /// <summary>
    /// 게임을 저장하는 버튼
    /// </summary>
    //public void Pause_SaveGame() => player.Save();

    /// <summary>
    /// 게임을 저장하고 종료해 타이틀 화면으로 가는 버튼
    /// </summary>
    public void Pause_SaveAndExitGame()
    {
        //player.Save();
        SceneManager.LoadScene("_Title");
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
    }

    /*
    void FixedUpdate()
    {
        PlayerStatUIActivate();
        PlayerUIActivate();
    }
    */
}
