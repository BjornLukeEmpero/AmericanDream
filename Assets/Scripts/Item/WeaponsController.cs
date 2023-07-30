using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField]
    // 현재 들고 있는 무기
    private Weapons currentHoldWeapon;
    // 이 값이 0보다 크면 총알은 발사되지 않는다
    private float currentFireRate;
    // 총이 재장전 상태가 아닐 때 false이며 발사도 false일때만 가능하다
    private bool isReload = false;
    // 발사 소리 재생
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

    // 발사 입력을 받는다
    private void Aim()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0)
            Fire();
    }

    // 발사를 위한 과정
    private void Fire()
    {
        currentFireRate = currentHoldWeapon.fireRate;
        Shoot();
    }

    // 실제 투사체 발사
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


    
    // 입력을 받아 소리 재생
    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}

