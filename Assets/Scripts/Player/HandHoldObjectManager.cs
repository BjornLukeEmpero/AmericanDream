using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandHoldObjectManager : MonoBehaviour
{
    // 무기, 소비 아이템, 도구 등의 교체 중복 실행 방지
    public static bool isChangeHandHoldObject = false;

    [SerializeField]
    private float changeHandHoldObjectDelayTime; // 아이템 교체 지연시간
    private float changeHandHoldObjectEndDelayTime; // 아이템 교체가 완전히 끝난 시점

    [SerializeField]
    private Weapons[] corootRevolver; // 모든 종류의 무기를 가지는 배열

    // 관리 차원에서 이름으로 무기에 접근할 수 있는 자료구조
    private Dictionary<string, Weapons> corootRevolverDictionary = new Dictionary<string, Weapons>();

    [SerializeField]
    private string currentHandHoldObjectType; // 현재 손에 들고 있는 아이템 타입
    // 현재 손에 들린 아이템으로 static을 사용해 여러 스크립트에서 클래스명으로 바로 접근
    public static Transform currentHandHoldObject;

    [SerializeField]
    // 코루트 리볼버일 때 CorootRevolverController.cs 활성화, 다른 무기일 시 비활성화
    private CorootRevolverController corootRevolverController;

    public IEnumerator ChangeHandHoldObjectCoroutine(string type, string name)
    {
        isChangeHandHoldObject = true;

        yield return new WaitForSeconds(changeHandHoldObjectDelayTime);

        CancelPreHandHoldObjectAction();
        HandHoldObjectChange(type, name);

        yield return new WaitForSeconds(changeHandHoldObjectEndDelayTime);

        currentHandHoldObjectType = type;
        isChangeHandHoldObject = false;
    }

    private void CancelPreHandHoldObjectAction()
    {
        switch (currentHandHoldObjectType)
        {
            case "COROOT_REVOLVER":
                CorootRevolverController.isActivate = false; 
                break;
        }
    }

    private void HandHoldObjectChange(string type, string name)
    {
        if(type == "COROOT_REVOLVER")
            corootRevolverController.WeaponChange(corootRevolverDictionary[name]);
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < corootRevolver.Length; i++)
            corootRevolverDictionary.Add(corootRevolver[i].weaponName, corootRevolver[i]);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
