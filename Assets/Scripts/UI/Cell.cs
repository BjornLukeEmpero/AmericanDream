using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Item item;

    public byte itemCount;
    public byte maxItemCount;

    public Image itemImage;

    [SerializeField]
    private Text itemCountTxt;
    [SerializeField]
    private GameObject countImage;

    // 아이템 이미지의 투명도 조절
    private void SetColor(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    // 새로운 아이템 획득
    public void AddItem(Item item, byte count = 1)
    {
        this.item = item;
        itemCount = count;
        itemImage.sprite = item.itemIamge;

        if(item.itemType != Item.ItemType.Weapon || item.itemType != Item.ItemType.Armor || item.itemType != Item.ItemType.Tool)
        {
            countImage.SetActive(true);
            itemCountTxt.text = itemCount.ToString();
        }
        else
        {
            itemCountTxt.text = "0";
            countImage.SetActive(false);
        }

        SetColor(1);
    }

    // 기존에 가진 아이템의 수량 변경
    public void SetItem(byte count)
    {
        itemCount += count;
        itemCountTxt.text = itemCount.ToString();
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
