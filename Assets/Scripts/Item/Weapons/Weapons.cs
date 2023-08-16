using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour 
{
    // 무기 이름
    public string weaponName;
    // 사용하는 탄의 종류
    public string usingBullet;
    // 현재 장전되어 있는 총알 수
    public byte bulletCnt;
    // 최대로 장전할 수 있는 총알 수
    public byte maxBulletCnt;
    // 급탄 시 한 번에 삽입하는 총알 수
    public byte reloadBulletCnt;

    // 재장전 소요 시간
    public float reloadTime;
    // 차탄 발사까지 소요 시간
    public float fireRate;
    // 사거리
    public float range;
    // 공격력
    public byte damage;

    // 발사 소리
    public AudioClip fireSound;
    // 재장전 소리
    public AudioClip reloadSound;    
}
