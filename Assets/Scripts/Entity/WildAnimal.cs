using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WildAnimalState
{
    idle, move, attack, stagger
}

public class WildAnimal : MonoBehaviour
{
    public byte hp;
    // 피격 횟수: 이 횟수를 바탕으로 주는 아이템의 손상 여부 결정
    public byte damagedCount;
    
    // 야생동물의 걷기 속도, 달리기 속도;
    public float walkSpeed;
    public float runSpeed;

    public float eyesight;

    public WildAnimalState wildAnimalState;
    
    public Transform target;

    public Transform homePosition;

    public void Knockback(Rigidbody2D rigidbody2D, float knockbackTime)
    {
        StartCoroutine(KnockbackCoroutine(rigidbody2D, knockbackTime));
    }

    private IEnumerator KnockbackCoroutine(Rigidbody2D rigidbody2D, float knockbackTime)
    {
        if (rigidbody2D != null)
        {
            yield return new WaitForSeconds(knockbackTime);
            rigidbody2D.velocity = Vector2.zero;
            wildAnimalState = WildAnimalState.idle;
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
