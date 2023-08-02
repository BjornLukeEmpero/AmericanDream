using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpContact : MonoBehaviour
{
    private Inventory inventory;

    public GameObject itemObject;

    private void Awake()
    {
        inventory = GameObject.FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for(byte i = 0; i < inventory.cells.Length; i++)
            {
                if (inventory.fullCheck[i] == false)
                {
                    inventory.fullCheck[i] = true;
                    inventory.AcquireItem(itemObject.transform.GetComponent<ItemPickUp>().item);
                    Destroy(itemObject.transform.gameObject);
                    break;
                }
            }
        }
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
