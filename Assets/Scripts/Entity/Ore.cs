using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Purchasing;
using UnityEngine;

public class Ore : MonoBehaviour
{
    public void Damaged()
    {
        StartCoroutine(BreakCoroutine());
    }
    
    IEnumerator BreakCoroutine()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
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
