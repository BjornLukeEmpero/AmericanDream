using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� ����ġ�� �ִ� ����ġ
    public int exp = 0;
    public int maxExp = 100;
    
    // �÷��̾��� ����°� �ִ� �����
    public short hp = 100;
    public short maxHp = 100;

    // �÷��̾��� ���¹̳��� �ִ� ���¹̳�
    public sbyte stamina = 100;
    public sbyte maxStamina = 100;

    // �÷��̾��� �������� �ִ� ������
    public sbyte satiety = 100;
    public sbyte maxSatiety = 100;

    // �÷��̾��� ���а� �ִ� ����
    public sbyte quench = 100;
    public sbyte maxQuench = 100;

    // �÷��̾��� ���� ü��, ���� ü��, �ִ� ü��
    public sbyte currrentTemperature = 1;
    public sbyte minTemperature = 0;
    public sbyte maxTemperature = 2;

    // �÷��̾��� �̵��ӵ�
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

    // �÷��̾��� �߷� ����
    private Rigidbody2D rb;
    // �÷��̾� ��ġ�� ���ͷ� �޾� ����
    private Vector3 vector;
    // �÷��̾��� �ִϸ��̼��� ����
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // 
        animator = GetComponent<Animator>();
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
        UpdateAnimationAndMove();
    }

    // �÷��̾� �̵� �� �ִϸ��̼��� ����ϴ� �Ķ���͸� �����ϴ� �Լ�
    void UpdateAnimationAndMove()
    {
        if (vector != Vector3.zero)
        {
            // ���� Alt Ű�� �����鼭 ����Ű�� �����ϸ� �޷����� ������ ���� ��� �ȴ´�.
            speed = Input.GetKey(KeyCode.LeftAlt) ? runSpeed : walkSpeed;
            MoveCharacter();
            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            animator.SetBool("moving", true);
        }
        else
        {
            // ������ ���
            speed = 0;
            animator.SetBool("moving", false);
        }
    }

    //�÷��̾� �̵� �Լ�
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }
}
