// �ۼ���: ������, �ֱ��ۼ�����: 2023-06-04

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    // ������ ��ȣ
    public string itemIndex;
    // �����۸�
    public string itemName; 
    // �����۸� �ѱ���
    public string itemNameKor;
    // �÷��̾� ���忡�� ������ ���Ű�
    public ushort buyPrice;
    // �÷��̾� ���忡�� ������ �ǸŰ�
    public ushort sellPrice;
    // ���� ������ �ִ� ���� ����
    public byte maxStack;
    // ������ ����
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
