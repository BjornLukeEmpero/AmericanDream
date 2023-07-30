using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public GameObject[] arrEmpty;
    private List<InventorySlot> items;
    
    private GameObject itemPrefab;
    private SpriteAtlas spriteAtlas;
    public Transform empty;

    // Start is called before the first frame update
    void Start()
    {
        spriteAtlas = Resources.Load<SpriteAtlas>("UI/UIImage");
        
        this.itemPrefab = Resources.Load<GameObject>("UI/UIImage");
        this.items = new List<InventorySlot>();

        /*
        var slot = Instantiate<GameObject>(this.itemPrefab, empty.transform);
        slot.transform.SetParent(this.empty);
        */

        this.AddItem(9);
    }

    private void AddItem(byte itemIndex)
    {
        var parent = this.arrEmpty[this.items.Count];
        var slot = Instantiate<GameObject>(this.itemPrefab, parent.transform);
        var uiItem = slot.GetComponent<InventorySlot>();
        this.items.Add(uiItem);
        string itemName = "";

        if (itemIndex == 9)
            itemName = "Items_009_.45Coroot";

        Sprite sprite = this.spriteAtlas.GetSprite(itemName);
        uiItem.Init(itemIndex, sprite, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
