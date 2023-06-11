// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
[System.Serializable]
public abstract class Item : MonoBehaviour
{
    // 아이템 번호
    public byte itemIndex;
    // 아이템명
    public string itemName; 
    // 아이템명 한국어
    public string itemNameKor;
    // 플레이어 입장에서 아이템 구매가
    public ushort buyPrice;
    // 플레이어 입장에서 아이템 판매가
    public ushort sellPrice;
    // 아이템 개수
    public byte stack;
    // 셀당 아이템 최대 보유 수량
    public byte maxStack;
    // 아이템 설명
    public string itemExplanation;

    public Sprite itemIcon;

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
