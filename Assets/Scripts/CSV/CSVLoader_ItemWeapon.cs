using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader_ItemWeapon : MonoBehaviour
{
    public List<Dictionary<string, object>> data;
    
    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("Item_Weapon");

        for (var i = 0; i < data.Count; i++)
        {
            Debug.Log("index " + i.ToString() + " : " + data[i]["itemIndex"].ToString() + " " + data[i]["itemName"].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
