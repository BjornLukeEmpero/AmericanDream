using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public byte bulletCnt;
    public byte maxBulletCnt;
    public byte reloadBulletCnt;

    public float reloadTime;
    public float fireRate;
    public float range;

    public AudioClip fireSound;
    public AudioClip reloadSound;    
}
