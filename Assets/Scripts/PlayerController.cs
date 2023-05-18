// 구현자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 생명력과 최대 생명력
    public short hp = 100;
    public short maxHp = 100;

    // 플레이어의 스태미나와 최대 스태미나
    public sbyte stamina = 100;
    public sbyte maxStamina = 100;

    // 플레이어의 이동속도
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

    // 플레이어가 가리키는 방향
    public sbyte direction;

    //// 탄창 수
    //public sbyte[] ammo = { 6, 15, 1, 5 };

    //// 재장전 관할
    //public float[] reloadTime = { 0.9f, 1.5f, 2.2f, 2.2f };
    //public float[] nextReload = { 0.0f, 0.0f, 0.0f, 0.0f };

    //// 차탄 발사 관할
    //public float[] fireRate = { 0.5f, 0.3f, 1.5f, 1.5f };
    //public float[] nextFire = { 0.0f, 0.0f, 0.0f, 0.0f };

    //// 발사 여부
    //public bool[] fireflag = { false, false, false, false };
    

    // 플레이어의 중력 관할
    private Rigidbody2D rb;
    // 플레이어 위치를 벡터로 받아 관할
    private Vector3 vector;
    // 플레이어의 애니메이션을 관할
    private Animator animator;
    // 플레이어의 사운드 관할
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
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");
        
        UpdateAnimationAndMove();
        //Attack();
    }

    // 플레이어 이동 시 애니메이션을 재생하는 파라미터를 조작하는 함수
    void UpdateAnimationAndMove()
    {
        if (vector != Vector3.zero)
        {
            // 왼쪽 Alt 키를 누르면서 방향키로 조작하면 달려가며 누르지 않은 경우 걷는다.
            speed = Input.GetKey(KeyCode.LeftAlt) ? runSpeed : walkSpeed;
            MoveCharacter();
            animator.SetFloat("moveX", vector.x);
            animator.SetFloat("moveY", vector.y);
            animator.SetBool("moving", true);

            // 방향키를 누르면 방향이 지정된다.
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
            // 정지한 경우
            speed = 0;
            animator.SetBool("moving", false);
        }
    }

    //플레이어 이동 함수
    void MoveCharacter()
    {
        rb.MovePosition(transform.position + vector * speed * Time.deltaTime);
    }
    

    //void Attack()
    //{
    //    // 리볼버 발사
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
