using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockbackTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ore") && this.gameObject.CompareTag("Player"))
            other.GetComponent<Ore>().Damaged();

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hit.GetComponent<WildAnimal>().wildAnimalState = WildAnimalState.stagger;
                    other.GetComponent<WildAnimal>().Knockback(hit, knockbackTime);
                }
                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<Player>().playerState = PlayerState.stagger;
                    other.GetComponent<Player>().Knockback(knockbackTime);
                }

                
                hit.isKinematic = false;
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
            }
        }
    }
}
