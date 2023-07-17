using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField]

    private Weapons currentHoldWeapon;

    private float currentFireRate;

    private bool isReload = false;

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

    private void Aim()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0)
            Fire();
    }

    private void Fire()
    {
        currentFireRate = currentHoldWeapon.fireRate;
        Shoot();
    }

    private void Shoot()
    {
        currentHoldWeapon.bulletCnt--;
        currentFireRate = currentHoldWeapon.fireRate;
        
        PlaySFX(currentHoldWeapon.fireSound);   
    }

    

    

    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}

