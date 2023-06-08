using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Weapon : Item
{
    public string usingBullet;

    public sbyte bulletCnt;

    public sbyte maxBulletCnt;

    public float reloadTime;

    public float fireRate;
    
    public Item_Weapon(string itemIndex, string itemName, string itemNameKor, ushort buyPrice, ushort sellPrice, byte maxStack, string itemExplanation,
        string usingBullet, sbyte bulletCnt, sbyte maxBulletCnt, float reloadTime, float fireRate) 
        : base(itemIndex, itemName, itemNameKor, buyPrice, sellPrice, maxStack, itemExplanation)
    {
        this.usingBullet = usingBullet;
        this.bulletCnt = bulletCnt;
        this.maxBulletCnt = bulletCnt;
        this.reloadTime = reloadTime;
        this.fireRate = fireRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
