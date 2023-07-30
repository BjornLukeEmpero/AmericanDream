using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildAnimal : MonoBehaviour
{
    public byte hp;
    // �ǰ� Ƚ��: �� Ƚ���� �������� �ִ� �������� �ջ� ���� ����
    public byte damagedCount;
    
    // �߻������� ���� �̵��ӵ�, �ȱ� �ӵ�, �޸��� �ӵ�
    public float speed = 0;
    public float walkSpeed = 15;
    public float runSpeed = 30;

    public float eyesight;

    public Transform target;

    public Transform homePosition;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= eyesight)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, walkSpeed * Time.deltaTime);
        }
    }
}
