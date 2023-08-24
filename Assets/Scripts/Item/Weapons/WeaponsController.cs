using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class WeaponsController : MonoBehaviour
{
    [SerializeField]
    // ���� ��� �ִ� ����
    protected Weapons currentHoldWeapon;
    // �� ���� 0���� ũ�� �Ѿ��� �߻���� �ʴ´�
    protected float currentFireRate;
    // ���� ������ ���°� �ƴ� �� false�̸� �߻絵 false�϶��� �����ϴ�
    protected bool isReload = false;
    // �߻� �Ҹ� ���
    protected AudioSource audioSource;

    private Animator animator;
    // �߻�Ǵ� �Ѿ�
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

    // �߻� �Է��� �޴´�
    protected void TryFire()
    {
        if (Input.GetKey(KeyCode.LeftControl) && currentFireRate <= 0 && !isReload)
            Fire();
    }

    // �߻縦 ���� ����
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

    // ���� ����ü �߻�
    private void Shoot()
    {
        // ����ü �߻� ����
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

    
    // �Է��� �޾� �Ҹ� ���
    private void PlaySFX(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    /// <summary>
    /// ���� ��ü ���� �����Լ�
    /// </summary>
    /// <param name="weapons">����Ϸ��� �ٸ� ����</param>
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

