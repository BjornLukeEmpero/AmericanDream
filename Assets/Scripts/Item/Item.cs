// �ۼ���: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // �������� ����
    public enum ItemType
    {
        Weapon, Bullet, Armor, Tool, Food, Other
    }
    
    // �����۸�
    public string itemName;
    // ������ ���� �� ����
    public ushort buyPrice;
    // ������ �Ǹ� �� ����
    public ushort sellPrice;
    // ���� ������ ������ ��
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

    // ���� �ִ� ���� ������ ������ ��
    public byte maxStack;
    // �����ۿ� ���� ����
    public string itemExplanation;
    // �������� ����
    public ItemType itemType;
    // �������� �̹���
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
