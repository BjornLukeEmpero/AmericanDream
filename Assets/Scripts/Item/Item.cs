// �ۼ���: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
[System.Serializable]
public abstract class Item : MonoBehaviour
{
    // ������ ��ȣ
    public byte itemIndex;
    // �����۸�
    public string itemName; 
    // �����۸� �ѱ���
    public string itemNameKor;
    // �÷��̾� ���忡�� ������ ���Ű�
    public ushort buyPrice;
    // �÷��̾� ���忡�� ������ �ǸŰ�
    public ushort sellPrice;
    // ������ ����
    public byte stack;
    // ���� ������ �ִ� ���� ����
    public byte maxStack;
    // ������ ����
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
