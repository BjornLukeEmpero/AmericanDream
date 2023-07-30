using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemCountTxt;
    public Button button;
    public byte id;

    public void Init(byte id, Sprite icon, byte amount)
    {
        this.id = id;
        this.icon.sprite = icon;
        this.itemCountTxt.text = amount.ToString();
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
