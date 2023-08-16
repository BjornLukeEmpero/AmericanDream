using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : WildAnimal
{
    public float chaseRadius;
    public float attackRadius;

    private Rigidbody2D rigidbody2D;

   
    
    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= chaseRadius && 
            Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if(wildAnimalState == WildAnimalState.idle || wildAnimalState == WildAnimalState.move && wildAnimalState != WildAnimalState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime);
                rigidbody2D.MovePosition(temp);
                ChangeState(WildAnimalState.move);
            }
        }
    }
    
    private void ChangeState(WildAnimalState newState)
    {
        if(wildAnimalState != newState)
        {
            wildAnimalState = newState;
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        wildAnimalState = WildAnimalState.idle;
        target = GameObject.FindWithTag("Player").transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }
}
