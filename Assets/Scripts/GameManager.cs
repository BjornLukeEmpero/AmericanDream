using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ½Ì±ÛÅæ Á¢±Ù¿ë ÇÁ·ÎÆÛÆ¼    
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null) 
        { 
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        Debug.Log("Current Save Number is " + DataManager.instance.ClickSaveBtn);
    }

    void Update()
    {
        
    }
}
