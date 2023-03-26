using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 플레이어의 이동속도
    public float speed;
    private Rigidbody2D rb;
    private Vector3 vector;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        vector = Vector3.zero;
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");
        if (vector != Vector3.zero)
            MoveCharacter();
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }
}
