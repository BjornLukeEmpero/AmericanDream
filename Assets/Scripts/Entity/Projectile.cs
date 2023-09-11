using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProjectile(Vector2 velocity, Vector3 direction)
    {

        rigidbody2D.velocity = velocity.normalized * projectileSpeed;
        transform.rotation = Quaternion.Euler(direction);
    }
}
