// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // 무기 사용자 정보
    private PlayerController playerController;
    // 현재 들고 있는 무기
    private Weapon holdWeapon;
    // 이 값이 0보다 크면 차탄이 발사되지 않는다
    private float currentFireRate;
    // 재장전 여부 확인
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
        FireRate();
        TryFire();
        TryReload();
    }

    // 차탄 발사 대기
    private void FireRate()
    {
        // currentFireRate가 0보다 크면 시간이 지날 때마다 값을 감소시킨다
        if(currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        // 왼쪽 Ctrl키가 눌리고, currentFireRate 값이 0보다 작거나 같으면서, 장전되어 있을 때
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0 && !isReload)
            Fire();
    }

    private void Fire()
    {
        if (!isReload)
        {
            // 들고 있는 무기에 총알이 남아 있다면 발사한다
            if (holdWeapon.currentAmmoCount > 0)
                Shoot();
            // 총알이 남아 있지 않으면 장전을 한다
            else
                Reload();
        }
    }

    private void Shoot()
    {
        //Vector3 newPos = playerController.transform.position;
        GameObject bullet = Instantiate(holdWeapon.bullet) as GameObject;
        //bullet.transform.position = newPos;
        Rigidbody2D rbullet = bullet.GetComponent<Rigidbody2D>();
        switch(playerController.direction)
        {
            case 0:
                rbullet.AddForce(Vector2.down * 20, ForceMode2D.Impulse); break;
            case 1:
                rbullet.AddForce(Vector2.left * 20, ForceMode2D.Impulse); break;
            case 2:
                rbullet.AddForce(Vector2.right * 20, ForceMode2D.Impulse); break;
            case 3:
                rbullet.AddForce(Vector2.up * 20, ForceMode2D.Impulse); break;
        }

        holdWeapon.currentAmmoCount--;
        currentFireRate = holdWeapon.fireRate;
        PlaySFX(holdWeapon.fireSound);
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isReload && holdWeapon.currentAmmoCount <= 0)
            StartCoroutine(Reload());
    }

    IEnumerator Reload() 
    {
        isReload = true;

        yield return new WaitForSeconds(holdWeapon.fireRate);

        holdWeapon.currentAmmoCount = holdWeapon.reloadAmmoCount;

        isReload = false;
    }

    private void PlaySFX(AudioClip audioClipt)
    {
        audioSource.clip = audioClipt;
        audioSource.Play();
    }
}
