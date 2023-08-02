using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickCellController : MonoBehaviour
{
    [SerializeField] private Cell[] quickCells = new Cell[4]; // 4���� ����
    [SerializeField] private Transform transformParent; // ������ �θ� ������Ʈ

    private byte selectedCell; // ������ ���� �ε���(0~3)
    [SerializeField] private GameObject quickCellImage; // ���õ� ���� �̹���

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
        selectedCell = num; // ���õ� ��
        // ���õ� ������ �̹��� �̵�
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
