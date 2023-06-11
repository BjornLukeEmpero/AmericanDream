// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 저장할 데이터
/// </summary>
[System.Serializable]
public class SaveData
{
    // 레벨
    public byte level;
    // 직업
    public string job;
    // 경험치
    public ushort exp;
    // 최대 경험치
    public ushort maxExp;
    // 생명력, 최대 생명력
    public byte hp;
    public byte maxHp;
    // 스태미나, 최대 스태미나
    public byte stamina;
    public byte maxStamina;
    // 포만감
    public byte satiety;
    // 수분
    public byte quench;
    // 현재 체온
    public byte currentTemperature;
    // 플레이어가 바라보는 방향
    // 0: 남쪽, 1:서쪽, 2: 북쪽 3: 동쪽
    public byte direction;
    // 플레이어 위치
    public Vector3 playerLocation;
}

/// <summary>
/// 타이틀 창 관할
/// </summary>
/// <param name="clickStartDlgSaveBtn">현재 선택한 버튼</param>
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

    /// <summary>
    /// 게임 데이터 초기화
    /// </summary>
    public void ResetSave()
    {
        saveData[clickStartDlgSaveBtn].level = 1;
        saveData[clickStartDlgSaveBtn].job = "무직";
        saveData[clickStartDlgSaveBtn].exp = 0;
        saveData[clickStartDlgSaveBtn].maxExp = 1000;
        saveData[clickStartDlgSaveBtn].hp = 100;
        saveData[clickStartDlgSaveBtn].maxHp = 100;
        saveData[clickStartDlgSaveBtn].stamina = 100;
        saveData[clickStartDlgSaveBtn].maxStamina = 100;
        saveData[clickStartDlgSaveBtn].satiety = 100;
        saveData[clickStartDlgSaveBtn].quench = 100;
        saveData[clickStartDlgSaveBtn].currentTemperature = 1;
        saveData[clickStartDlgSaveBtn].direction = 0;
        saveData[clickStartDlgSaveBtn].playerLocation[0] = 0;
        saveData[clickStartDlgSaveBtn].playerLocation[1] = 0;
        saveData[clickStartDlgSaveBtn].playerLocation[2] = 0;
    }

    /// <summary>
    /// 게임 데이터 저장
    /// </summary>
    public void Save()
    {
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = JsonUtility.ToJson(saveData[clickStartDlgSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json", jsonData[clickStartDlgSaveBtn]);
    }

    /// <summary>
    /// 저장한 게임 데이터 불러오기
    /// </summary>
    public void Load()
    { 
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json");
        saveData[clickStartDlgSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[clickStartDlgSaveBtn]);
    }

    /// <summary>
    /// 저장된 게임 데이터 삭제
    /// </summary>
    public void Delete()
    {
        File.Delete(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json");
        // 세이브 파일명을 되돌려 비었다고 표시
        saveDataInfo[clickStartDlgSaveBtn].text = "Empty " + $"{clickStartDlgSaveBtn + 1}";
        messageBox[3].gameObject.SetActive(false);
    }


    /// <summary>
    /// 게임 시작 창 띄우기
    /// </summary>
    public void GameStart() => StartDlg.gameObject.SetActive(true);

    /// <summary>
    /// 세이브 파일 1 클릭 시
    /// </summary>
    public void StartDlgSaveBtn1() => clickStartDlgSaveBtn = 0;

    /// <summary>
    /// 세이브 파일 2 클릭 시
    /// </summary>
    public void StartDlgSaveBtn2() => clickStartDlgSaveBtn = 1;

    /// <summary>
    /// 세이브 파일 3 클릭 시
    /// </summary>
    public void StartDlgSaveBtn3() => clickStartDlgSaveBtn = 2;

    /// <summary>
    /// 세이브 파일 4 클릭 시
    /// </summary>
    public void StartDlgSaveBtn4() => clickStartDlgSaveBtn = 3;

    /// <summary>
    /// 세이브파일 선택 창 이어하기
    /// </summary>
    public void StartDlgLoadGame()
    {
        // 선택한 창에 따라 서로 다른 세이브 파일을 불러온다
        // 선택하지 않고 클릭 시 파일 미선택 안내 메시지가 출력된다.
        switch(clickStartDlgSaveBtn)
        {
            case 0:
                if(!File.Exists(Application.persistentDataPath + "/SaveData0.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 1:
                if (!File.Exists(Application.persistentDataPath + "/SaveData1.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 2:
                if (!File.Exists(Application.persistentDataPath + "/SaveData2.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                {
                    Load();
                    SceneManager.LoadScene("PlayingTown");
                }
                break;
            case 3:
                if (!File.Exists(Application.persistentDataPath + "/SaveData3.json"))
                    messageBox[1].gameObject.SetActive(true);
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

    /// <summary>
    /// 세이브파일 선택 창 새로하기
    /// </summary>
    public void StartDlgNewGame()
    {
        // 선택한 창에 따라 서로 다른 세이브 파일을 새로 시작한다.
        // 세이브 파일이 이미 있으면 초기화 안내 메시지를 출력한다.
        // 선택하지 않고 클릭 시 파일 미선택 안내 메시지가 출력된다.
        switch (clickStartDlgSaveBtn)
        {
            case 0:
                if (!File.Exists(Application.persistentDataPath + "/SaveData0.json"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[0].text = "Save 1";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                    messageBox[2].gameObject.SetActive(true);
                break;
            case 1:
                if (!File.Exists(Application.persistentDataPath + "/SaveData1.json"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[1].text = "Save 2";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                    messageBox[2].gameObject.SetActive(true);
                break;
            case 2:
                if (!File.Exists(Application.persistentDataPath + "/SaveData2.json"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[2].text = "Save 3";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                    messageBox[2].gameObject.SetActive(true);
                break;
            case 3:
                if (!File.Exists(Application.persistentDataPath + "/SaveData3.json"))
                {
                    ResetSave();
                    Save();
                    saveDataInfo[3].text = "Save 4";
                    SceneManager.LoadScene("PlayingTown");
                }
                else
                    messageBox[2].gameObject.SetActive(true);
                break;
            default:
                messageBox[0].gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 세이브파일 삭제하기
    /// </summary>
    public void StartDlgDeleteGame()
    {
        switch (clickStartDlgSaveBtn)
        {
            case 0:
                if(!File.Exists(Application.persistentDataPath + "/SaveData0.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                    messageBox[3].gameObject.SetActive(true);
                break;

            case 1:
                if (!File.Exists(Application.persistentDataPath + "/SaveData1.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                    messageBox[3].gameObject.SetActive(true);
                break;

            case 2:
                if (!File.Exists(Application.persistentDataPath + "/SaveData2.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                    messageBox[3].gameObject.SetActive(true);
                break;

            case 3:
                if (!File.Exists(Application.persistentDataPath + "/SaveData3.json"))
                    messageBox[1].gameObject.SetActive(true);
                else
                    messageBox[3].gameObject.SetActive(true);
                break;
            default:
                messageBox[0].gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 세이브파일 선택 창 닫기
    /// </summary>
    public void StartDlgClose() => StartDlg.gameObject.SetActive(false);

    /// <summary>
    /// 메시지 박스 닫기
    /// </summary>
    public void MsgBoxClose()
    {
        // 메시지 창의 모든 닫기, 취소 버튼으로 창 닫기 기능 활성화
        for(sbyte i = 0; i < messageBox.Length; i++)
        {
            messageBox[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 메시지 박스로 게임 새로 시작하기
    /// </summary>
    public void MsgBoxNewStart()
    {
        ResetSave();
        saveDataInfo[clickStartDlgSaveBtn].text = "Save " + $"{clickStartDlgSaveBtn + 1}";
        SceneManager.LoadScene("PlayingTown");
    }

    /// <summary>
    /// 게임 종료
    /// </summary>    
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
