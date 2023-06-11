using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> itemWeaponData = CSVReader.Read("Item_Weapon");
        for (int i = 0; i < itemWeaponData.Count; i++)
            Debug.Log("index " + (i).ToString() + itemWeaponData[i]["itemNameKor"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
