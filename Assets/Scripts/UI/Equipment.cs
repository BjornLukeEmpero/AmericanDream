using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public static bool equipmentActivated = false;

    [SerializeField]
    private GameObject equipmentDialog;
    [SerializeField]
    private GameObject CellsParent;

    private Cell[] weaponCells;

    private Cell[] bulletCells;
    
    // Start is called before the first frame update
    void Start()
    {
        weaponCells = CellsParent.GetComponentsInChildren<Cell>();
        bulletCells = CellsParent.GetComponentsInChildren<Cell>();
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenEquipment();
    }

    private void TryOpenEquipment()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            equipmentActivated = !equipmentActivated;

            if(equipmentActivated )
                OpenEquipment();
            else
                CloseEquipment();
        }
    }

    private void OpenEquipment() => equipmentDialog.SetActive(true);

    private void CloseEquipment() => equipmentDialog.SetActive(false);

    public void AcquireItem(Item item, byte count = 1)
    {
        
    }

}
