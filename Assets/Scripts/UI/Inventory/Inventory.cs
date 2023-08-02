using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    [SerializeField]
    private GameObject inventoryDialog;
    [SerializeField]
    private GameObject cellsParent;

    // �� ���� �������� �ִ� �� Ȯ��
    public bool[] fullCheck = new bool[32];

    // �������� ���� ��
    public Cell[] cells = new Cell[32];
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();        
    }

    private void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();
        }
    }

    // �κ��丮 â ����
    private void OpenInventory() => inventoryDialog.SetActive(true);
    // �κ��丮 â �ݱ�
    private void CloseInventory() => inventoryDialog.SetActive(false);

    // ������ ����
    public void AcquireItem(Item item, byte count = 1)
    {
        // �������� ����, ��, ������ �ƴ� ���
        if(Item.ItemType.Weapon != item.itemType || Item.ItemType.Armor != item.itemType || Item.ItemType.Tool != item.itemType)
        {
            // ���� ������ �������� �ִ� �� �˻�
            for (byte i = 0; i < cells.Length; i++)
            {
                //
                if (cells[i].item != null)
                {
                    // ���� ������ ������ ���� �κ��丮���� ã���鼭 ������ ���� ���� �ִ� ���� ���� ���� ��
                    if (cells[i].item.itemName == item.itemName && cells[i].item.Stack < item.maxStack)
                    {
                        cells[i].SetItemCount(count);
                        return;
                    }
                }
            }
        }

        // ���� ������ �������� ���ٸ� ���ο� ���� ����
        for(byte i = 0; i < cells.Length; i++)
        {
            if (cells[i].item == null)
            {
                cells[i].AddItem(item, count);
                return;
            }
        }
    }
}
