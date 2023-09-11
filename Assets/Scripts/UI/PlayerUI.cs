using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // �÷��̾� ���� UI
    // 0: ����, 1: ����, 2: ����ġ, 3: �����, 4: ���¹̳�, 5: ������, 6: ����
    //public TextMeshProUGUI[] statUI = new TextMeshProUGUI[7];
    
    // 0: ����ġ, 1: �����, 2: ���¹̳�, 3: ������, 4: ����, 5:ü��
    //public Slider[] barUI = new Slider[6];
    // �Ͻ����� ���� �˸�
    private bool pauseState = false;
    public bool PauseState
    {
        get { return pauseState; }
        set { pauseState = value; }
    }
    // �Ͻ����� �г�
    public Image pausePanel;

    private Player player;


    /*
    public void PlayerStatUIActivate()
    {
        // 0: ����, 1: ����, 2: ����ġ/�ִ� ����ġ, 3: �����/�ִ� �����, 4: ���¹̳�/�ִ� ���¹̳�
        // 5: ������/�ִ� ������, 6: ����/�ִ� ����
        statUI[0].text = "Lv: " + player.Level;
        statUI[1].text = player.PlayerJob;
        statUI[2].text = $"Exp: {player.Exp} / {player.MaxExp}";
        statUI[3].text = $"HP: {player.Hp} / {player.MaxHp}";
        statUI[4].text = $"ST: {player.Stamina} / {player.MaxStamina}";
        statUI[5].text = $"SA: {player.Satiety} / {player.MaxSatiety}";
        statUI[6].text = $"QU: {player.Quench} / {player.MaxQuench}";
    }

    // �÷��̾� ������ ����� ���� UI�� ǥ��
    public void PlayerUIActivate()
    {
        // 0: �ִ� ����ġ, 1: �ִ� �����, 2: �ִ� ���¹̳�, 3: �ִ� ������, 4: �ִ� ����, 5: �ִ� ü��
        barUI[0].maxValue = player.MaxExp;
        barUI[1].maxValue = player.MaxHp;
        barUI[2].maxValue = player.MaxStamina;
        barUI[3].maxValue = player.MaxSatiety;
        barUI[4].maxValue = player.MaxQuench;
        barUI[5].maxValue = player.MaxTemperature;

        // 0: ����ġ, 1: �����, 2: ���¹̳�, 3: ������, 4: ����, 5: ���� ü��
        barUI[0].value = player.Exp;
        barUI[1].value = player.Hp;
        barUI[2].value = player.Stamina;
        barUI[3].value = player.Satiety;
        barUI[4].value = player.Quench;
        barUI[5].value = player.CurrentTemperature;
    }
    */

    /// <summary>
    /// ESCŰ�� �̿��� ���� �Ͻ�����
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
    /// �Ͻ����� �г��� �������� ���ư��� ��ư
    /// </summary>
    public void Pause_BackToGame()
    {
        pausePanel.gameObject.SetActive(false);
        PauseState = false;
        //player.MoveGameTime();
    }

    /// <summary>
    /// ������ �����ϴ� ��ư
    /// </summary>
    //public void Pause_SaveGame() => player.Save();

    /// <summary>
    /// ������ �����ϰ� ������ Ÿ��Ʋ ȭ������ ���� ��ư
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
