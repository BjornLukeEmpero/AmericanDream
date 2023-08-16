using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // 무기 교체 중복 실행 방지
    public static bool isChangeWeapons = false;

    [SerializeField]
    // 무기 교체 시 소요 시간
    private float changeWeaponsDelayTime;
    [SerializeField]
    // 무기 교체가 완전히 끝난 시점
    private float changeWeaponsEndDelayTime;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
