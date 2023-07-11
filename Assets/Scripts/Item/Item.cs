// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public byte itemIndex;

    public string itemName;

    public string itemNameKor;

    public ushort buyPrice;

    public ushort sellPrice;

    public byte stack;

    public byte maxStack;

    public string itemExplanation;

    public Item(byte itemIndex, string itemName, string itemNameKor, ushort buyPrice, ushort sellPrice, byte stack, byte maxStack, string itemExplanation)
    {
        this.itemIndex = itemIndex;
        this.itemName = itemName;
        this.itemNameKor = itemNameKor;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
        this.stack = stack;
        this.maxStack = maxStack;
        this.itemExplanation = itemExplanation;
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
