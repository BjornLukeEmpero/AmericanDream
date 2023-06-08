using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
    public TextAsset item;
    
    // Start is called before the first frame update
    void Start()
    {
        string currentData = item.text.Substring(0, item.text.Length - 1);
        string[] line = currentData.Split('\n');
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
