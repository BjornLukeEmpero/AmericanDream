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

    // 셀 내에 아이템이 있는 지 확인
    public bool[] fullCheck = new bool[32];

    // 아이템이 들어가는 셀
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

    // 인벤토리 창 열기
    private void OpenInventory() => inventoryDialog.SetActive(true);
    // 인벤토리 창 닫기
    private void CloseInventory() => inventoryDialog.SetActive(false);

    // 아이템 습득
    public void AcquireItem(Item item, byte count = 1)
    {
        // 아이템이 무기, 방어구, 도구가 아닐 경우
        if(Item.ItemType.Weapon != item.itemType || Item.ItemType.Armor != item.itemType || Item.ItemType.Tool != item.itemType)
        {
            // 같은 종류의 아이템이 있는 지 검사
            for (byte i = 0; i < cells.Length; i++)
            {
                //
                if (cells[i].item != null)
                {
                    // 같은 종류의 아이템 셀을 인벤토리에서 찾으면서 아이탬 스택 값이 최대 스택 값과 작을 시
                    if (cells[i].item.itemName == item.itemName && cells[i].item.Stack < item.maxStack)
                    {
                        cells[i].SetItemCount(count);
                        return;
                    }
                }
            }
        }

        // 같은 종류의 아이템이 없다면 새로운 슬롯 마련
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
