using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // ����ġ, �ִ� ����ġ
    public ushort exp;
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
}

public class DataManager : MonoBehaviour
{
    // �̱���
    public static DataManager instance;
    // ���� �����Ͱ� ����Ǵ� ��
    public SaveData[] saveData = new SaveData[4];
    // Ÿ��Ʋ UI�� ����
    private TitleUIManager titleUIManager;

    // ������ ��ư ��ȣ
#if UNITY_EDITOR
    public sbyte clickSaveBtn = 0;
#else
    public sbyte clickSaveBtn = -1;
#endif

    public sbyte ClickSaveBtn
    {
        get { return clickSaveBtn; }
        set 
        {
            // ������ ��ư ��ȣ ���� ����
            if (clickSaveBtn >= -1 && clickSaveBtn <= 3)
                clickSaveBtn = value;
        }
    }

    public static DataManager Instance
    {
        get
        {
            if(instance == null)
                return null;
            return instance;
        }
    }


    /// <summary>
    /// ���� ������ �ʱ�ȭ
    /// </summary>
    public void ResetSave()
    {
        saveData[ClickSaveBtn].level = 1;
        saveData[ClickSaveBtn].job = "����";
        saveData[ClickSaveBtn].exp = 0;
        saveData[ClickSaveBtn].maxExp = 1000;
        saveData[ClickSaveBtn].hp = 100;
        saveData[ClickSaveBtn].maxHp = 100;
        saveData[ClickSaveBtn].stamina = 100;
        saveData[ClickSaveBtn].maxStamina = 100;
        saveData[ClickSaveBtn].satiety = 100;
        saveData[ClickSaveBtn].quench = 100;
        saveData[ClickSaveBtn].currentTemperature = 1;
        saveData[ClickSaveBtn].direction = 0;
    }

    /// <summary>
    /// ���� ������ ����
    /// </summary>
    public void SaveData()
    {
        string[] jsonData = new string[4];
        jsonData[ClickSaveBtn] = JsonUtility.ToJson(saveData[ClickSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json", jsonData[ClickSaveBtn]);
    }

    /// <summary>
    /// ������ ���� ������ �ҷ�����
    /// </summary>
    public void LoadData()
    {
        string[] jsonData = new string[4];
        jsonData[ClickSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json");
        saveData[ClickSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[ClickSaveBtn]);
    }

    /// <summary>
    /// ����� ���� ������ ����
    /// </summary>
    public void Delete()
    {
        // ���̺� ���ϸ��� �ǵ��� ����ٰ� ǥ��
        //titleUIManager.saveDataInfo[ClickSaveBtn].text = "Empty " + $"{ClickSaveBtn + 1}";
        File.Delete(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json");
        titleUIManager.messageBox[3].gameObject.SetActive(false);
    }

    /// <summary>
    /// ���� ���̺� ������ �̹� �ִ� �� Ȯ��
    /// </summary>
    /// <param name="clickSaveBtn"></param>
    /// <returns></returns>
    public bool FileCheck(sbyte clickSaveBtn)
    {
        if (File.Exists(Application.persistentDataPath + $"/SaveData{clickSaveBtn}.json"))
            return true;
        else
            return false;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        titleUIManager = GetComponent<TitleUIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
