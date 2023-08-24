using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    // ���̺����� ���� â
    public Image StartDlg;
    // �޽��� â ����, 0: ���� �̼��� �ȳ�, 1: ���� ���� �ȳ�, 2: �ʱ�ȭ �ȳ�, 3: ���� �ȳ�
    public Image[] messageBox = new Image[4];
    
    // 
    private bool[] alreadyExist = new bool[4];
    // ���̺����� ���� ������ �����ϴ� ��
    public TextMeshProUGUI[] saveDataInfo = new TextMeshProUGUI[4];

    //private DataManager dataManager;

    /// <summary>
    /// ���� ���� â ����
    /// </summary>
    public void GameStart() => StartDlg.gameObject.SetActive(true);

    public void GoGame() => SceneManager.LoadScene("PlayingTown");

    /// <summary>
    /// ���̺� ���� 1 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn1()
    {
        DataManager.instance.ClickSaveBtn = 0;
        StartDlgSaveBtnLog();
    }
    /// <summary>
    /// ���̺� ���� 2 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn2() 
    {
        DataManager.instance.ClickSaveBtn = 1;
        StartDlgSaveBtnLog();
    } 

    /// <summary>
    /// ���̺� ���� 3 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn3()
    {
        DataManager.instance.ClickSaveBtn = 2;
        StartDlgSaveBtnLog();
    }

    /// <summary>
    /// ���̺� ���� 4 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn4()
    {
        DataManager.instance.ClickSaveBtn = 3;
        StartDlgSaveBtnLog();
    }

    public void StartDlgSaveBtnLog() => Debug.Log("Current Number is " + DataManager.instance.ClickSaveBtn);

    /// <summary>
    /// ���̺����� ���� â �̾��ϱ�
    /// </summary>
    public void StartDlgLoadGame()
    {
        // ������ â�� ���� ���� �ٸ� ���̺� ������ �ҷ��´�
        // �������� �ʰ� Ŭ�� �� ���� �̼��� �ȳ� �޽����� ��µȴ�.
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
    /// ���̺����� ���� â �����ϱ�
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
    /// ���̺����� �����ϱ�
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
    /// ���̺����� ���� â �ݱ�
    /// </summary>
    public void StartDlgClose() => StartDlg.gameObject.SetActive(false);

    /// <summary>
    /// �޽��� �ڽ� �ݱ�
    /// </summary>
    public void MsgBoxClose()
    {
        // �޽��� â�� ��� �ݱ�, ��� ��ư���� â �ݱ� ��� Ȱ��ȭ
        for (sbyte i = 0; i < messageBox.Length; i++)
            messageBox[i].gameObject.SetActive(false);
    }

    /// <summary>
    /// �޽��� �ڽ��� ���� ���� �����ϱ�
    /// </summary>
    public void MsgBoxNewStart()
    {
        DataManager.instance.ResetSave();
        saveDataInfo[DataManager.instance.ClickSaveBtn].text = "Save " + $"{DataManager.instance.ClickSaveBtn + 1}";
        SceneManager.LoadScene("PlayingTown");
    }

    /// <summary>
    /// ���� ����
    /// </summary>    
    public void Exit()
    {
        // ����Ƽ �����Ϳ��� ������ �����Ѵ�
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
