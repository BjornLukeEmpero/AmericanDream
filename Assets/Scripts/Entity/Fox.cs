using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FoxState
{
    idle, move, escape, damaged, die
}

public class Fox : WildAnimal
{
    public FoxState currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = FoxState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= eyesight)
        {
            if(currentState == FoxState.idle || currentState == FoxState.move && currentState != FoxState.escape) 
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime);
                ChangeState(FoxState.move);
            }
        }
    }

    private void ChangeState(FoxState newState)
    {
        if(currentState != newState)
        {

        }
    }
}
