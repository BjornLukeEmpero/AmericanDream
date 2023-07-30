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
    // 아이템 
    public ushort buyPrice;
    // 셀당 보유한 아이템 수
    public byte stack;
    // 셀당 최대 보유 가능한 아이템 수
    public byte maxStack;
    // 아이템에 대한 설명
    public string itemExplanation;

    public ItemType itemType;

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
