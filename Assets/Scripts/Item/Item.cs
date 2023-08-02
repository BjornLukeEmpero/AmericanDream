// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // 아이템의 유형
    public enum ItemType
    {
        Weapon, Bullet, Armor, Tool, Food, Other
    }
    
    // 아이템명
    public string itemName;
    // 아이템 구매 시 가격
    public ushort buyPrice;
    // 아이템 판매 시 가격
    public ushort sellPrice;
    // 셀당 보유한 아이템 수
    public byte stack;
    public byte Stack
    {
        get { return stack; } 
        set 
        {
            if (stack >= 0 && stack <= maxStack)
                stack = value;
        }
    }

    // 셀당 최대 보유 가능한 아이템 수
    public byte maxStack;
    // 아이템에 대한 설명
    public string itemExplanation;
    // 아이템의 종류
    public ItemType itemType;
    // 아이템의 이미지
    public Sprite itemIamge;

    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
