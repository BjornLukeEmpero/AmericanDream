using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCellController : MonoBehaviour
{
    [SerializeField] private Cell[] quickCells = new Cell[4]; // 4°³ÀÇ Äü¼¿
    [SerializeField] private Transform transformParent; // Äü¼¿ÀÇ ºÎ¸ð ¿ÀºêÁ§Æ®

    private byte selectedCell; // ¼±ÅÃÇÑ Äü¼¿ ÀÎµ¦½º(0~3)
    [SerializeField] private GameObject quickCellImage; // ¼±ÅÃµÈ Äü¼¿ ÀÌ¹ÌÁö

    [SerializeField]
    private WeaponsController weaponController;

    
    private void TryInputNumber()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeCell(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeCell(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeCell(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeCell(3);
    }

    private void ChangeCell(byte num)
    {
        SelectedCell(num);
        //Execute();
    }

    private void SelectedCell(byte num)
    {
        selectedCell = num; // ¼±ÅÃµÈ ¼¿
        // ¼±ÅÃµÈ Äü¼¿·Î ÀÌ¹ÌÁö ÀÌµ¿
        quickCellImage.transform.position = quickCells[selectedCell].transform.position;
    }

    /*
    private void Execute()
    {
        if (quickCells[selectedCell].item != null)
        {
            if (quickCells[selectedCell].item.itemType == Item.ItemType.Weapon || quickCells[selectedCell].item.itemType == Item.ItemType.Tool)
                
        }
    }
    */
    // Start is called before the first frame update
    void Start()
    {
        quickCells = transformParent.GetComponentsInChildren<Cell>();
        selectedCell = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TryInputNumber();        
    }
}
