// 작성자: 이재윤

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 총알 오브젝트
    public GameObject bullet;
    // 총의 이름
    public string gunName;
    // 최대 사거리
    public float range;
    // 연사에 걸리는 시간
    public float fireRate;
    // 재장전 시간
    public float reloadTime;
    // 총의 공격력
    public short damage;
    // 급탄 수
    public sbyte reloadAmmoCount;
    // 현재 탄창에 남아있는 총알 수
    public sbyte currentAmmoCount;
    // 최대 보유 가능 총알 수
    public sbyte maxAmmoCount;

    public AudioClip fireSound;

}
