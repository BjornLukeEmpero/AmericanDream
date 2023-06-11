using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item_Weapon : Item
{
    public string usingBullet;

    public byte bulletCnt;

    public byte maxBulletCnt;

    public float reloadTime;

    public float fireRate;
    
    public Item_Weapon(byte itemIndex, string itemName, string itemNameKor, ushort buyPrice, ushort sellPrice, byte stack, byte maxStack, string itemExplanation,
        string usingBullet, byte bulletCnt, byte maxBulletCnt, float reloadTime, float fireRate) 
        : base(itemIndex, itemName, itemNameKor, buyPrice, sellPrice, stack, maxStack, itemExplanation)
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
