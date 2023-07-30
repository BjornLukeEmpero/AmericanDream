using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildAnimal : MonoBehaviour
{
    public byte hp;
    // 피격 횟수: 이 횟수를 바탕으로 주는 아이템의 손상 여부 결정
    public byte damagedCount;
    
    // 야생동물의 현재 이동속도, 걷기 속도, 달리기 속도
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
