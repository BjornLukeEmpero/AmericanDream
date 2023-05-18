// �ۼ���: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // ���� ����� ����
    private PlayerController playerController;
    // ���� ��� �ִ� ����
    private Weapon holdWeapon;
    // �� ���� 0���� ũ�� ��ź�� �߻���� �ʴ´�
    private float currentFireRate;
    // ������ ���� Ȯ��
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
        FireRate();
        TryFire();
        TryReload();
    }

    // ��ź �߻� ���
    private void FireRate()
    {
        // currentFireRate�� 0���� ũ�� �ð��� ���� ������ ���� ���ҽ�Ų��
        if(currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    private void TryFire()
    {
        // ���� CtrlŰ�� ������, currentFireRate ���� 0���� �۰ų� �����鼭, �����Ǿ� ���� ��
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0 && !isReload)
            Fire();
    }

    private void Fire()
    {
        if (!isReload)
        {
            // ��� �ִ� ���⿡ �Ѿ��� ���� �ִٸ� �߻��Ѵ�
            if (holdWeapon.currentAmmoCount > 0)
                Shoot();
            // �Ѿ��� ���� ���� ������ ������ �Ѵ�
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
