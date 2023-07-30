using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
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

    void SetBullet(Vector2 velocity, Vector3 direction)
    {
        rigidbody2D.velocity = velocity.normalized * bulletSpeed;
        transform.rotation = Quaternion.Euler(direction);
    }
}
