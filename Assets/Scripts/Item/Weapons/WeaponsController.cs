using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponsController : MonoBehaviour
{
    [SerializeField]
    // 현재 들고 있는 무기
    protected Weapons currentHoldWeapon;
    // 이 값이 0보다 크면 총알은 발사되지 않는다
    protected float currentFireRate;
    // 총이 재장전 상태가 아닐 때 false이며 발사도 false일때만 가능하다
    protected bool isReload = false;
    // 발사 소리 재생
    protected AudioSource audioSource;

    private Animator animator;
    // 발사되는 총알
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponsFireRate();
        TryFire();
        TryReload();
    }

    private void WeaponsFireRate()
    {
        if(currentFireRate > 0)
            currentFireRate -= Time.deltaTime;
    }

    // 발사 입력을 받는다
    protected void TryFire()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0 && !isReload)
            Fire();
    }

    // 발사를 위한 과정
    private void Fire()
    {
        if(!isReload)
        {
            if (currentHoldWeapon.bulletCnt > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
        }
    }

    // 실제 투사체 발사
    private void Shoot()
    {
        // 투사체 발사 절차
        currentHoldWeapon.bulletCnt--;
        currentFireRate = currentHoldWeapon.fireRate;
        PlaySFX(currentHoldWeapon.fireSound);
        Debug.Log("Fire!");
    }

    private void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isReload && currentHoldWeapon.bulletCnt <= 0)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    IEnumerator ReloadCoroutine()
    {
        isReload = true;
        yield return new WaitForSeconds(currentHoldWeapon.reloadTime);
        currentHoldWeapon.bulletCnt = currentHoldWeapon.maxBulletCnt;
        isReload = false;
    }

   
    /*
    private void TryReload()
    {
        
            
    }

    IEnumerator ReloadCoroutine()
    {

    }
    */

    
    // 입력을 받아 소리 재생
    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    /// <summary>
    /// 무기 교체 관할 가상함수
    /// </summary>
    /// <param name="weapons">사용하려는 다른 무기</param>
    public virtual void WeaponChange(Weapons weapons)
    {
        if (HandHoldObjectManager.currentHandHoldObject != null)
            HandHoldObjectManager.currentHandHoldObject.gameObject.SetActive(false);

        currentHoldWeapon = weapons;
        HandHoldObjectManager.currentHandHoldObject = currentHoldWeapon.GetComponent<Transform>();
        
        currentHoldWeapon.transform.localPosition = Vector3.zero;
        currentHoldWeapon.gameObject.SetActive(true);
    }

}

