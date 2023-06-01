// 작성자: 이재윤, 최근작성일자: 2023-06-01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

// 저장할 데이터
[System.Serializable]
public class SaveData
{
    // 레벨
    public string level;
    // 직업
    public string job;
    // 경험치
    public string exp;
    // 최대 경험치
    public string maxExp;
    // 생명력, 최대 생명력
    public string hp;
    public string maxHp;
    // 스태미나, 최대 스태미나
    public string stamina;
    public string maxStamina;
    // 포만감
    public string satiety;
    // 수분
    public string quench;
    // 현재 체온
    public string currentTemperature;
}

public class TitleManager : MonoBehaviour
{
    // 세이브파일 선택 창
    public Image StartDlg;
    // 메시지 창 모음, 0: 파일 미선택 안내, 1: 파일 없음 안내, 2: 초기화 안내, 3: 삭제 안내
    public Image[] messageBox = new Image[4];
    // 선택한 버튼 번호
    public sbyte clickStartDlgSaveBtn = -1;
    // 게임 데이터가 저장되는 곳
    public SaveData[] saveData = new SaveData[4];
    // 세이브파일 관련 정보를 저장하는 곳
    public TextMeshProUGUI[] saveDataInfo = new TextMeshProUGUI[4];


    // 게임 데이터 초기화
    public void ResetSave()
    {
        saveData[clickStartDlgSaveBtn].level = "1";
        saveData[clickStartDlgSaveBtn].job = "무직";
        saveData[clickStartDlgSaveBtn].exp = "0";
        saveData[clickStartDlgSaveBtn].maxExp = "100";
        saveData[clickStartDlgSaveBtn].hp = "100";
        saveData[clickStartDlgSaveBtn].maxHp = "100";
        saveData[clickStartDlgSaveBtn].stamina = "100";
        saveData[clickStartDlgSaveBtn].maxStamina = "100";
        saveData[clickStartDlgSaveBtn].satiety = "100";
        saveData[clickStartDlgSaveBtn].quench = "100";
        saveData[clickStartDlgSaveBtn].currentTemperature = "1";
    }
    
    // 게임 데이터 저장
    public void Save()
    {
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = JsonUtility.ToJson(saveData[clickStartDlgSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".txt", jsonData[clickStartDlgSaveBtn]);
    }

    // 저장한 게임 데이터 불러오기
    public void Load()
    { 
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".txt");
        saveData[clickStartDlgSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[clickStartDlgSaveBtn]);
    }


    // 게임 시작 창 띄우기
    public void GameStart()
    {
        // 세이브파일 선택 창을 띄운다
        StartDlg.gameObject.SetActive(true);
    }
    
    
    // 세이브 파일 1 클릭 시
    public void StartDlgSaveBtn1()
    {
        clickStartDlgSaveBtn = 0;
    }

    // 세이브 파일 2 클릭 시
    public void StartDlgSaveBtn2()
    {
        clickStartDlgSaveBtn = 1;
    }

    // 세이브 파일 3 클릭 시
    public void StartDlgSaveBtn3()
    {
        clickStartDlgSaveBtn = 2;
    }

    // 세이브 파일 4 클릭 시
    public void StartDlgSaveBtn4()
    {
        clickStartDlgSaveBtn = 3;
    }

    // 세이브파일 선택 창 이어하기
    public void StartDlgLoadGame()
    {
        // 선택한 창에 따라 서로 다른 세이브 파일을 불러온다
        // 선택하지 않고 클릭 시 파일 미선택 안내 메시지가 출력된다.
        switch(clickStartDlgSaveBtn)
        {
            case 0:
                if(!File.Exists(Application.persistentDataPath + "/SaveData0.txt"))
                {
                    messageBox[1].gameObject.SetActive(true);
                }
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 1:
                if (!File.Exists(Application.persistentDataPath + "/SaveData1.txt"))
                {
                    messageBox[1].gameObject.SetActive(true);
                }
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 2:
                if (!File.Exists(Application.persistentDataPath + "/SaveData2.txt"))
                {
                    messageBox[1].gameObject.SetActive(true);
                }
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 3:
                if (!File.Exists(Application.persistentDataPath + "/SaveData3.txt"))
                {
                    messageBox[1].gameObject.SetActive(true);
                }
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            default:
                messageBox[0].gameObject.SetActive(true);
                break;
        }
    }

    // 세이브파일 선택 창 새로하기
    public void StartDlgNewGame()
    {
        // 선택한 창에 따라 서로 다른 세이브 파일을 새로 시작한다.
        // 세이브 파일이 이미 있으면 초기화 안내 메시지를 출력한다.
        // 선택하지 않고 클릭 시 파일 미선택 안내 메시지가 출력된다.
        switch (clickStartDlgSaveBtn)
        {
            case 0:
                if (!File.Exists(Application.persistentDataPath + "/SaveData0.txt"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[0].text = "Save 1";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                {
                    messageBox[2].gameObject.SetActive(true);
                }
                break;
            case 1:
                if (!File.Exists(Application.persistentDataPath + "/SaveData1.txt"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[1].text = "Save 2";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                {
                    messageBox[2].gameObject.SetActive(true);
                    
                }
                break;
            case 2:
                if (!File.Exists(Application.persistentDataPath + "/SaveData2.txt"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[2].text = "Save 3";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                {
                    messageBox[2].gameObject.SetActive(true);
                }
                break;
            case 3:
                if (!File.Exists(Application.persistentDataPath + "/SaveData3.txt"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[3].text = "Save 4";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                {
                    messageBox[2].gameObject.SetActive(true);
                }
                break;
            default:
                messageBox[0].gameObject.SetActive(true);
                break;
        }
    }

    public void StartDlgDeleteGame()
    {

    }

    // 세이브파일 선택 창 닫기
    public void StartDlgClose()
    {
        StartDlg.gameObject.SetActive(false);
    }

    // 메시지 박스 닫기
    public void MsgBoxClose()
    {
        // 메시지 창의 모든 닫기, 취소 버튼으로 창 닫기 기능 활성화
        for(sbyte i = 0; i < messageBox.Length; i++)
        {
            messageBox[i].gameObject.SetActive(false);
        }
    }

    // 메시지 박스로 게임 새로 시작하기
    public void MsgBoxNewStart()
    {
        ResetSave();
        saveDataInfo[clickStartDlgSaveBtn].text = "Save " + $"{clickStartDlgSaveBtn + 1}";
        SceneManager.LoadScene("PlayingTown");
    }

    public void MsgBoxDelete()
    {

    }

    // 
    public void Exit()
    {
// 유니티 에디터에서 게임을 종료한다
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        if (clickStartDlgSaveBtn != -1)
            Load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
