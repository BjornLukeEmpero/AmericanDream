// �ۼ���: ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // �Ѿ� ������Ʈ
    public GameObject bullet;
    // ���� �̸�
    public string gunName;
    // �ִ� ��Ÿ�
    public float range;
    // ���翡 �ɸ��� �ð�
    public float fireRate;
    // ������ �ð�
    public float reloadTime;
    // ���� ���ݷ�
    public short damage;
    // ��ź ��
    public sbyte reloadAmmoCount;
    // ���� źâ�� �����ִ� �Ѿ� ��
    public sbyte currentAmmoCount;
    // �ִ� ���� ���� �Ѿ� ��
    public sbyte maxAmmoCount;

    public AudioClip fireSound;

}
