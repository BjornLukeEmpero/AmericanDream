using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField]
    // ���� ��� �ִ� ����
    private Weapons currentHoldWeapon;
    // �� ���� 0���� ũ�� �Ѿ��� �߻���� �ʴ´�
    private float currentFireRate;
    // ���� ������ ���°� �ƴ� �� false�̸� �߻絵 false�϶��� �����ϴ�
    private bool isReload = false;
    // �߻� �Ҹ� ���
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponsFireRate();
        Aim();
    }

    private void WeaponsFireRate()
    {
        if(currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    // �߻� �Է��� �޴´�
    private void Aim()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0)
            Fire();
    }

    // �߻縦 ���� ����
    private void Fire()
    {
        currentFireRate = currentHoldWeapon.fireRate;
        Shoot();
    }

    // ���� ����ü �߻�
    private void Shoot()
    {
        currentHoldWeapon.bulletCnt--;
        currentFireRate = currentHoldWeapon.fireRate;
        
        PlaySFX(currentHoldWeapon.fireSound);   
    }

    private void TryReload()
    {
        if (currentHoldWeapon.bulletCnt <= 0 && !isReload && Input.GetKey(KeyCode.LeftControl))
        {

        }
            
    }


    
    // �Է��� �޾� �Ҹ� ���
    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}

