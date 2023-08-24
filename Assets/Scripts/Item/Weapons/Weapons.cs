using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour 
{
    // ���� �̸�
    public string weaponName;
    // ����ϴ� ź�� ����
    public string usingBullet;
    // ���� �����Ǿ� �ִ� �Ѿ� ��
    public byte bulletCnt;
    // �ִ�� ������ �� �ִ� �Ѿ� ��
    public byte maxBulletCnt;
    // ��ź �� �� ���� �����ϴ� �Ѿ� ��
    public byte reloadBulletCnt;

    // ������ �ҿ� �ð�
    public float reloadTime;
    // ��ź �߻���� �ҿ� �ð�
    public float fireRate;
    // ��Ÿ�
    public float range;
    // ���ݷ�
    public byte damage;

    // �߻� �Ҹ�
    public AudioClip fireSound;
    // ������ �Ҹ�
    public AudioClip reloadSound;    
}
