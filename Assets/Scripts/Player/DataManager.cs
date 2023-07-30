using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    // 경험치, 최대 경험치
    public ushort exp;
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
}

public class DataManager : MonoBehaviour
{
    // 싱글톤
    public static DataManager instance;
    // 게임 데이터가 저장되는 곳
    public SaveData[] saveData = new SaveData[4];
    // 타이틀 UI와 접근
    private TitleUIManager titleUIManager;

    // 선택한 버튼 번호
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
            // 선택한 버튼 번호 범위 제한
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
    /// 게임 데이터 초기화
    /// </summary>
    public void ResetSave()
    {
        saveData[ClickSaveBtn].level = 1;
        saveData[ClickSaveBtn].job = "무직";
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
    /// 게임 데이터 저장
    /// </summary>
    public void SaveData()
    {
        string[] jsonData = new string[4];
        jsonData[ClickSaveBtn] = JsonUtility.ToJson(saveData[ClickSaveBtn]);
        File.WriteAllText(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json", jsonData[ClickSaveBtn]);
    }

    /// <summary>
    /// 저장한 게임 데이터 불러오기
    /// </summary>
    public void LoadData()
    {
        string[] jsonData = new string[4];
        jsonData[ClickSaveBtn] = File.ReadAllText(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json");
        saveData[ClickSaveBtn] = JsonUtility.FromJson<SaveData>(jsonData[ClickSaveBtn]);
    }

    /// <summary>
    /// 저장된 게임 데이터 삭제
    /// </summary>
    public void Delete()
    {
        // 세이브 파일명을 되돌려 비었다고 표시
        //titleUIManager.saveDataInfo[ClickSaveBtn].text = "Empty " + $"{ClickSaveBtn + 1}";
        File.Delete(Application.persistentDataPath + "/SaveData" + $"{ClickSaveBtn}" + ".json");
        titleUIManager.messageBox[3].gameObject.SetActive(false);
    }

    /// <summary>
    /// 게임 세이브 파일이 이미 있는 지 확인
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
