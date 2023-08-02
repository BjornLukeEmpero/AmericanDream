using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Purchasing;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [SerializeField]
    public byte hp;

    public void Mining()
    {
        hp--;
        if (hp <= 0)
            Destruction();
    }

    private void Destruction()
    {
        Destroy(this);
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
