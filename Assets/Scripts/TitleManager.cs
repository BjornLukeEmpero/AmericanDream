// �ۼ���: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ ������
/// </summary>
[System.Serializable]
public class SaveData
{
    // ����
    public byte level;
    // ����
    public string job;
    // ����ġ
    public ushort exp;
    // �ִ� ����ġ
    public ushort maxExp;
    // �����, �ִ� �����
    public byte hp;
    public byte maxHp;
    // ���¹̳�, �ִ� ���¹̳�
    public byte stamina;
    public byte maxStamina;
    // ������
    public byte satiety;
    // ����
    public byte quench;
    // ���� ü��
    public byte currentTemperature;
    // �÷��̾ �ٶ󺸴� ����
    // 0: ����, 1:����, 2: ���� 3: ����
    public byte direction;
    // �÷��̾� ��ġ
    public Vector3 playerLocation;
}

/// <summary>
/// Ÿ��Ʋ â ����
/// </summary>
/// <param name="clickStartDlgSaveBtn">���� ������ ��ư</param>
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

    /// <summary>
    /// ���� ������ �ʱ�ȭ
    /// </summary>
    public void ResetSave()
    {
        saveData[clickStartDlgSaveBtn].level = 1;
        saveData[clickStartDlgSaveBtn].job = "����";
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
    /// ���� ������ ����
    /// </summary>
    public void Save()
    {
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = JsonUtility.ToJson(saveData[clickStartDlgSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json", jsonData[clickStartDlgSaveBtn]);
    }

    /// <summary>
    /// ������ ���� ������ �ҷ�����
    /// </summary>
    public void Load()
    { 
        string[] jsonData = new string[4];
        jsonData[clickStartDlgSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json");
        saveData[clickStartDlgSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[clickStartDlgSaveBtn]);
    }

    /// <summary>
    /// ����� ���� ������ ����
    /// </summary>
    public void Delete()
    {
        File.Delete(Application.persistentDataPath + "/SaveData" + $"{clickStartDlgSaveBtn}" + ".json");
        // ���̺� ���ϸ��� �ǵ��� ����ٰ� ǥ��
        saveDataInfo[clickStartDlgSaveBtn].text = "Empty " + $"{clickStartDlgSaveBtn + 1}";
        messageBox[3].gameObject.SetActive(false);
    }


    /// <summary>
    /// ���� ���� â ����
    /// </summary>
    public void GameStart() => StartDlg.gameObject.SetActive(true);

    /// <summary>
    /// ���̺� ���� 1 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn1() => clickStartDlgSaveBtn = 0;

    /// <summary>
    /// ���̺� ���� 2 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn2() => clickStartDlgSaveBtn = 1;

    /// <summary>
    /// ���̺� ���� 3 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn3() => clickStartDlgSaveBtn = 2;

    /// <summary>
    /// ���̺� ���� 4 Ŭ�� ��
    /// </summary>
    public void StartDlgSaveBtn4() => clickStartDlgSaveBtn = 3;

    /// <summary>
    /// ���̺����� ���� â �̾��ϱ�
    /// </summary>
    public void StartDlgLoadGame()
    {
        // ������ â�� ���� ���� �ٸ� ���̺� ������ �ҷ��´�
        // �������� �ʰ� Ŭ�� �� ���� �̼��� �ȳ� �޽����� ��µȴ�.
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
    /// ���̺����� ���� â �����ϱ�
    /// </summary>
    public void StartDlgNewGame()
    {
        // ������ â�� ���� ���� �ٸ� ���̺� ������ ���� �����Ѵ�.
        // ���̺� ������ �̹� ������ �ʱ�ȭ �ȳ� �޽����� ����Ѵ�.
        // �������� �ʰ� Ŭ�� �� ���� �̼��� �ȳ� �޽����� ��µȴ�.
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
    /// ���̺����� �����ϱ�
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
    /// ���̺����� ���� â �ݱ�
    /// </summary>
    public void StartDlgClose() => StartDlg.gameObject.SetActive(false);

    /// <summary>
    /// �޽��� �ڽ� �ݱ�
    /// </summary>
    public void MsgBoxClose()
    {
        // �޽��� â�� ��� �ݱ�, ��� ��ư���� â �ݱ� ��� Ȱ��ȭ
        for(sbyte i = 0; i < messageBox.Length; i++)
        {
            messageBox[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �޽��� �ڽ��� ���� ���� �����ϱ�
    /// </summary>
    public void MsgBoxNewStart()
    {
        ResetSave();
        saveDataInfo[clickStartDlgSaveBtn].text = "Save " + $"{clickStartDlgSaveBtn + 1}";
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
        if (clickStartDlgSaveBtn != -1)
            Load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
