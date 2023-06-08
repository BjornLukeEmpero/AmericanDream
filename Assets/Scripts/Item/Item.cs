// 작성자: 이재윤, 최근작성일자: 2023-06-04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // 아이템 번호
    public string itemIndex;
    // 아이템명
    public string itemName; 
    // 아이템명 한국어
    public string itemNameKor;
    // 플레이어 입장에서 아이템 구매가
    public ushort buyPrice;
    // 플레이어 입장에서 아이템 판매가
    public ushort sellPrice;
    // 셀당 아이템 최대 보유 수량
    public byte maxStack;
    // 아이템 설명
    public string itemExplanation;

    public Sprite itemIcon;

    public Item(string itemIndex, string itemName, string itemNameKor, ushort buyPrice, ushort sellPrice, byte maxStack, string itemExplanation)
    {
        this.itemIndex = itemIndex;
        this.itemName = itemName;
        this.itemNameKor = itemNameKor;
        this.buyPrice = buyPrice;
        this.sellPrice = sellPrice;
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
