// ������: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // �÷��̾��� ����°� �ִ� �����
    public short hp = 100;
    public short maxHp = 100;

    // �÷��̾��� ���¹̳��� �ִ� ���¹̳�
    public sbyte stamina = 100;
    public sbyte maxStamina = 100;

    // �÷��̾��� �̵��ӵ�
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

    // �÷��̾ ����Ű�� ����
    public sbyte direction;

    //// źâ ��
    //public sbyte[] ammo = { 6, 15, 1, 5 };

    //// ������ ����
    //public float[] reloadTime = { 0.9f, 1.5f, 2.2f, 2.2f };
    //public float[] nextReload = { 0.0f, 0.0f, 0.0f, 0.0f };

    //// ��ź �߻� ����
    //public float[] fireRate = { 0.5f, 0.3f, 1.5f, 1.5f };
    //public float[] nextFire = { 0.0f, 0.0f, 0.0f, 0.0f };

    //// �߻� ����
    //public bool[] fireflag = { false, false, false, false };
    

    // �÷��̾��� �߷� ����
    private Rigidbody2D rb;
    // �÷��̾� ��ġ�� ���ͷ� �޾� ����
    private Vector3 vector;
    // �÷��̾��� �ִϸ��̼��� ����
    private Animator animator;
    // �÷��̾��� ���� ����
    AudioSource audioSource;

    public Weapon revolver;

    public Weapon repeater;

    public Weapon rifle;

    public Weapon shotgun;

    // Start is called before the first frame update
    void Start()
    {
        // 
        animator = GetComponent<Animator>();
        
        rb = GetComponent<Rigidbody2D>();

        revolver = GetComponent<Weapon>();
        repeater = GetComponent<Weapon>();
        rifle = GetComponent<Weapon>();
        shotgun = GetComponent<Weapon>();
    }

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    // Update is called once per frame
    void Update()
    {
        vector = Vector3.zero;
        // �¿� ����Ű�� ���� �¿�� �̵� ����
        vector.x = Input.GetAxis("Horizontal");
        
        // ���� ����Ű�� ���� ���Ϸ� �̵� ����
        vector.y = Input.GetAxis("Vertical");
        
        UpdateAnimationAndMove();
        //Attack();
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

            // ����Ű�� ������ ������ �����ȴ�.
            if (Input.GetKey(KeyCode.DownArrow))
                direction = 0;
            if (Input.GetKey(KeyCode.LeftArrow))
                direction = 1;
            if(Input.GetKey(KeyCode.RightArrow))
                direction = 2;
            if (Input.GetKey(KeyCode.UpArrow)) 
                direction = 3;
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
    

    //void Attack()
    //{
    //    // ������ �߻�
    //    if(Input.GetKey(KeyCode.Z))
    //    {
    //        if (fireflag[0] == false && ammo[0] > 0 && nextFire[0] <= 0)
    //        {
    //            fireflag[0] = true;
                
                
    //            Vector3 newPos = this.transform.position;
                
    //            GameObject bulletRvv = Instantiate(bulletRevolver) as GameObject;
    //            bulletRvv.transform.position = newPos;
    //            Rigidbody2D rbullet = bulletRvv.GetComponent<Rigidbody2D>();
    //            switch(direction)
    //            {
    //                case 0:
    //                    rbullet.AddForce(Vector2.down * 20, ForceMode2D.Impulse); break;
    //                case 1:
    //                    rbullet.AddForce(Vector2.left * 20, ForceMode2D.Impulse); break;
    //                case 2:
    //                    rbullet.AddForce(Vector2.right * 20, ForceMode2D.Impulse); break;
    //                case 3:
    //                    rbullet.AddForce(Vector2.up * 20, ForceMode2D.Impulse); break;
    //            }
                
    //            ammo[0]--;
                
    //            audioSource.Play();
    //            nextFire[0] -= Time.deltaTime;
    //        }
    //        else if (fireflag[0] == false && ammo[0] <= 0 && nextReload[0] <= 0)
    //        {
    //            nextReload[0] -= Time.deltaTime;
    //            ammo[0] += 6;
    //        }

    //        else
    //            fireflag[0] = false;
    //    }
        
    //}
}
