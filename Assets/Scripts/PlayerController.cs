using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 플레이어의 경험치와 최대 경험치
    public int exp = 0;
    public int maxExp = 100;
    
    // 플레이어의 생명력과 최대 생명력
    public short hp = 100;
    public short maxHp = 100;

    // 플레이어의 스태미나와 최대 스태미나
    public sbyte stamina = 100;
    public sbyte maxStamina = 100;

    // 플레이어의 포만감과 최대 포만감
    public sbyte satiety = 100;
    public sbyte maxSatiety = 100;

    // 플레이어의 수분과 최대 수분
    public sbyte quench = 100;
    public sbyte maxQuench = 100;

    // 플레이어의 현재 체온, 최저 체온, 최대 체온
    public sbyte currrentTemperature = 1;
    public sbyte minTemperature = 0;
    public sbyte maxTemperature = 2;

    // 플레이어의 이동속도
    public float speed = 0;
    public float walkSpeed = 120;
    public float runSpeed = 240;

    // 플레이어의 중력 관할
    private Rigidbody2D rb;
    // 플레이어 위치를 벡터로 받아 관할
    private Vector3 vector;
    // 플레이어의 애니메이션을 관할
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
        // 좌우 방향키를 눌러 좌우로 이동 가능
        vector.x = Input.GetAxis("Horizontal");
        // 상하 방향키를 눌러 상하로 이동 가능
        vector.y = Input.GetAxis("Vertical");
        UpdateAnimationAndMove();
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
}
