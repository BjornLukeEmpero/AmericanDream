using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    // 세이브파일 선택 창
    public Image StartDlg;
    // 메시지 창 모음, 0: 파일 미선택 안내, 1: 파일 없음 안내, 2: 초기화 안내, 3: 삭제 안내
    public Image[] messageBox = new Image[4];
    
    // 
    private bool[] alreadyExist = new bool[4];
    // 세이브파일 관련 정보를 저장하는 곳
    public TextMeshProUGUI[] saveDataInfo = new TextMeshProUGUI[4];

    //private DataManager dataManager;

    /// <summary>
    /// 게임 시작 창 띄우기
    /// </summary>
    public void GameStart() => StartDlg.gameObject.SetActive(true);

    public void GoGame() => SceneManager.LoadScene("PlayingTown");

    /// <summary>
    /// 세이브 파일 1 클릭 시
    /// </summary>
    public void StartDlgSaveBtn1()
    {
        DataManager.instance.ClickSaveBtn = 0;
        StartDlgSaveBtnLog();
    }
    /// <summary>
    /// 세이브 파일 2 클릭 시
    /// </summary>
    public void StartDlgSaveBtn2() 
    {
        DataManager.instance.ClickSaveBtn = 1;
        StartDlgSaveBtnLog();
    } 

    /// <summary>
    /// 세이브 파일 3 클릭 시
    /// </summary>
    public void StartDlgSaveBtn3()
    {
        DataManager.instance.ClickSaveBtn = 2;
        StartDlgSaveBtnLog();
    }

    /// <summary>
    /// 세이브 파일 4 클릭 시
    /// </summary>
    public void StartDlgSaveBtn4()
    {
        DataManager.instance.ClickSaveBtn = 3;
        StartDlgSaveBtnLog();
    }

    public void StartDlgSaveBtnLog() => Debug.Log("Current Number is " + DataManager.instance.ClickSaveBtn);

    /// <summary>
    /// 세이브파일 선택 창 이어하기
    /// </summary>
    public void StartDlgLoadGame()
    {
        // 선택한 창에 따라 서로 다른 세이브 파일을 불러온다
        // 선택하지 않고 클릭 시 파일 미선택 안내 메시지가 출력된다.
        alreadyExist[DataManager.instance.ClickSaveBtn] = DataManager.instance.FileCheck(DataManager.instance.ClickSaveBtn);
        Debug.Log("Load: Current selected save number is" + DataManager.instance.ClickSaveBtn);
        if (DataManager.instance.ClickSaveBtn >= 0 && DataManager.instance.ClickSaveBtn <= 3)
        {
            if (alreadyExist[DataManager.instance.ClickSaveBtn] == true)
            {
                DataManager.instance.LoadData();
                SceneManager.LoadScene("PlayingTown");
            }
            else
                messageBox[1].gameObject.SetActive(true);
        }
        if(DataManager.instance.clickSaveBtn == -1)
            messageBox[0].gameObject.SetActive(true);
    }

    /// <summary>
    /// 세이브파일 선택 창 새로하기
    /// </summary>
    public void StartDlgNewGame()
    {
        alreadyExist[DataManager.instance.ClickSaveBtn] = DataManager.instance.FileCheck(DataManager.instance.ClickSaveBtn);
        Debug.Log("New: Current selected save number is " + DataManager.instance.ClickSaveBtn);
        if (DataManager.instance.ClickSaveBtn >= 0 && DataManager.instance.ClickSaveBtn <= 3)
        {
            if (alreadyExist[DataManager.instance.ClickSaveBtn] == true)
                messageBox[2].gameObject.SetActive(true);
            else
            {
                DataManager.instance.ResetSave();
                DataManager.instance.SaveData();
                saveDataInfo[DataManager.instance.ClickSaveBtn].text = $"Save {DataManager.instance.ClickSaveBtn + 1}";
                SceneManager.LoadScene("PlayingTown");
            }
        }
        if(DataManager.instance.ClickSaveBtn == -1)
            messageBox[0].gameObject.SetActive(true);
    }

    /// <summary>
    /// 세이브파일 삭제하기
    /// </summary>
    public void StartDlgDeleteGame()
    {
        alreadyExist[DataManager.instance.ClickSaveBtn] = DataManager.instance.FileCheck(DataManager.instance.ClickSaveBtn);
        Debug.Log("Delete: Current selected save number is " + DataManager.instance.ClickSaveBtn);
        if (DataManager.instance.ClickSaveBtn >= 0 && DataManager.instance.ClickSaveBtn <= 3)
        {
            if (alreadyExist[DataManager.instance.ClickSaveBtn] == true)
                messageBox[3].gameObject.SetActive(true);
            else
                messageBox[1].gameObject.SetActive(true);
        }
        if (DataManager.instance.ClickSaveBtn == -1)
            messageBox[0].gameObject.SetActive(true);
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
        for (sbyte i = 0; i < messageBox.Length; i++)
            messageBox[i].gameObject.SetActive(false);
    }

    /// <summary>
    /// 메시지 박스로 게임 새로 시작하기
    /// </summary>
    public void MsgBoxNewStart()
    {
        DataManager.instance.ResetSave();
        saveDataInfo[DataManager.instance.ClickSaveBtn].text = "Save " + $"{DataManager.instance.ClickSaveBtn + 1}";
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
        //dataManager = GetComponent<DataManager>();
        // if (DataManager.instance.ClickSaveBtn != -1)
        //     DataManager.instance.LoadData();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
