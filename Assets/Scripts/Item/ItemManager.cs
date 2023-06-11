using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{
    static public ItemManager instance;
    private CSVLoader_ItemWeapon _ItemWeapon;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        _ItemWeapon = GetComponent<CSVLoader_ItemWeapon>();
    }

    public List<Item_Weapon>[] itemWeapons = new List<Item_Weapon>[8];

    // Start is called before the first frame update
    void Start()
    {
        

        for (byte i = 0; i < 8; i++)
        {
            itemWeapons[i].Add(new  ((byte)_ItemWeapon.data[i]["itemIndex"], (string)_ItemWeapon.data[i]["itemName"],
                                            (string)_ItemWeapon.data[i]["itemNameKor"], (ushort)_ItemWeapon.data[i]["buyPrice"],
                                            (byte)_ItemWeapon.data[i]["sellPrice"], (byte)_ItemWeapon.data[i]["stack"], 
                                            (byte)_ItemWeapon.data[i]["maxStack"], (string)_ItemWeapon.data[i]["itemExplanation"],
                                            (string)_ItemWeapon.data[i]["usingBullet"], (byte)_ItemWeapon.data[i]["bulletCnt"], 
                                            (byte)_ItemWeapon.data[i]["maxBulletCnt"], (float)_ItemWeapon.data[i]["reloadTime"],
                                            (float)_ItemWeapon.data[i]["fireRage"]));

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
