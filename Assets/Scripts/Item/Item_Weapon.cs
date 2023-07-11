using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item_Weapon", menuName = "Item/Item_Weapon")]
[System.Serializable]
public class Item_Weapon : Item
{
    public string usingBullet;

    public byte bulletCnt;

    public byte maxBulletCnt;

    public float reloadTime;

    public float fireRate;

    public float range;

    public byte getItemIndex()
    {
        return this.itemIndex;
    }

    public string getItemName()
    {
        return this.itemName;
    }

    public string getItemNameKor()
    {
        return this.itemNameKor;
    }

    public ushort getBuyPrice()
    {
        return this.buyPrice;
    }

    public ushort getSellPrice()
    {
        return this.sellPrice;
    }

    public byte getStack()
    {
        return this.stack;
    }

    public byte getMaxStack()
    {
        return this.maxStack;
    }

    public string getItemExplanation()
    {
        return this.itemExplanation;
    }

    public Item_Weapon(byte itemIndex, string itemName, string itemNameKor, ushort buyPrice, ushort sellPrice, byte stack, byte maxStack, string itemExplanation,
        string usingBullet, byte bulletCnt, byte maxBulletCnt, float reloadTime, float fireRate, float range)
        : base(itemIndex, itemName, itemNameKor, buyPrice, sellPrice, stack, maxStack, itemExplanation)
    {
        this.usingBullet = usingBullet;
        this.bulletCnt = bulletCnt;
        this.maxBulletCnt = bulletCnt;
        this.reloadTime = reloadTime;
        this.fireRate = fireRate;
        this.range = range;
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
