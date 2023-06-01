// �ۼ���: ������, �ֱ��ۼ�����: 2023-06-01

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

// ������ ������
[System.Serializable]
public class SaveData
{
    // ����
    public string level;
    // ����
    public string job;
    // ����ġ
    public string exp;
    // �ִ� ����ġ
    public string maxExp;
    // �����, �ִ� �����
    public string hp;
    public string maxHp;
    // ���¹̳�, �ִ� ���¹̳�
    public string stamina;
    public string maxStamina;
    // ������
    public string satiety;
    // ����
    public string quench;
    // ���� ü��
    public string currentTemperature;
}

public class TitleManager : MonoBehaviour
{
    // ���̺����� ���� â
    public Image StartDlg;
    // �޽��� â ����, 0: ���� �̼��� �ȳ�, 1: ���� ���� �ȳ�, 2: �ʱ�ȭ �ȳ�, 3: ���� �ȳ�
    public Image[] messageBox = new Image[4];
    // ������ ��ư ��ȣ
    public sbyte clickStartDlgSaveBtn = -1;
    // ���� �����Ͱ� ����Ǵ� ��
    public SaveData[] saveData = new SaveData[4];
    // ���̺����� ���� ������ �����ϴ� ��
    public TextMeshProUGUI[] saveDataInfo = new TextMeshProUGUI[4];


    // ���� ������ �ʱ�ȭ
    public void ResetSave()
    {
        saveData[clickStartDlgSaveBtn].level = "1";
        saveData[clickStartDlgSaveBtn].job = "����";
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
    
    // ���� ������ ����
    public void Save()
    {
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = JsonUtility.ToJson(saveData[clickStartDlgSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".txt", jsonData[clickStartDlgSaveBtn]);
    }

    // ������ ���� ������ �ҷ�����
    public void Load()
    { 
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".txt");
        saveData[clickStartDlgSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[clickStartDlgSaveBtn]);
    }


    // ���� ���� â ����
    public void GameStart()
    {
        // ���̺����� ���� â�� ����
        StartDlg.gameObject.SetActive(true);
    }
    
    
    // ���̺� ���� 1 Ŭ�� ��
    public void StartDlgSaveBtn1()
    {
        clickStartDlgSaveBtn = 0;
    }

    // ���̺� ���� 2 Ŭ�� ��
    public void StartDlgSaveBtn2()
    {
        clickStartDlgSaveBtn = 1;
    }

    // ���̺� ���� 3 Ŭ�� ��
    public void StartDlgSaveBtn3()
    {
        clickStartDlgSaveBtn = 2;
    }

    // ���̺� ���� 4 Ŭ�� ��
    public void StartDlgSaveBtn4()
    {
        clickStartDlgSaveBtn = 3;
    }

    // ���̺����� ���� â �̾��ϱ�
    public void StartDlgLoadGame()
    {
        // ������ â�� ���� ���� �ٸ� ���̺� ������ �ҷ��´�
        // �������� �ʰ� Ŭ�� �� ���� �̼��� �ȳ� �޽����� ��µȴ�.
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

    // ���̺����� ���� â �����ϱ�
    public void StartDlgNewGame()
    {
        // ������ â�� ���� ���� �ٸ� ���̺� ������ ���� �����Ѵ�.
        // ���̺� ������ �̹� ������ �ʱ�ȭ �ȳ� �޽����� ����Ѵ�.
        // �������� �ʰ� Ŭ�� �� ���� �̼��� �ȳ� �޽����� ��µȴ�.
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

    // ���̺����� ���� â �ݱ�
    public void StartDlgClose()
    {
        StartDlg.gameObject.SetActive(false);
    }

    // �޽��� �ڽ� �ݱ�
    public void MsgBoxClose()
    {
        // �޽��� â�� ��� �ݱ�, ��� ��ư���� â �ݱ� ��� Ȱ��ȭ
        for(sbyte i = 0; i < messageBox.Length; i++)
        {
            messageBox[i].gameObject.SetActive(false);
        }
    }

    // �޽��� �ڽ��� ���� ���� �����ϱ�
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
        if (clickStartDlgSaveBtn != -1)
            Load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
