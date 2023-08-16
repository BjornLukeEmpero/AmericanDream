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
    // �ǰ� Ƚ��: �� Ƚ���� �������� �ִ� �������� �ջ� ���� ����
    public byte damagedCount;
    
    // �߻������� �ȱ� �ӵ�, �޸��� �ӵ�;
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
