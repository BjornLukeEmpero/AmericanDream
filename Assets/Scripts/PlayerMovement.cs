using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // �÷��̾��� �̵��ӵ�
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
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector.x = Input.GetAxis("Horizontal");
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector.y = Input.GetAxis("Vertical");
        if (vector != Vector3.zero)
            MoveCharacter();
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }
}
