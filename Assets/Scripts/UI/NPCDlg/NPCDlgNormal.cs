//

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDlgNormal : MonoBehaviour
{
    public GameObject npcDlgNormal;
    public TextMeshProUGUI npcNameTxt;
    public TextMeshProUGUI npcDlgNormalTxt;
    public string npcNameContent;
    public string npcDlgNormalContent;
    private bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (npcDlgNormal.activeInHierarchy)
                npcDlgNormal.SetActive(false);
            else
            {
                npcDlgNormal.SetActive(true);
                npcNameTxt.text = npcNameContent;
                npcDlgNormalTxt.text = npcDlgNormalContent;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            npcDlgNormal.SetActive(false);
        }
    }    
}
