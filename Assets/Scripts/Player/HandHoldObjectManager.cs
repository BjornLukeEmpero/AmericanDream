using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandHoldObjectManager : MonoBehaviour
{
    // ����, �Һ� ������, ���� ���� ��ü �ߺ� ���� ����
    public static bool isChangeHandHoldObject = false;

    [SerializeField]
    private float changeHandHoldObjectDelayTime; // ������ ��ü �����ð�
    private float changeHandHoldObjectEndDelayTime; // ������ ��ü�� ������ ���� ����

    [SerializeField]
    private Weapons[] corootRevolver; // ��� ������ ���⸦ ������ �迭

    // ���� �������� �̸����� ���⿡ ������ �� �ִ� �ڷᱸ��
    private Dictionary<string, Weapons> corootRevolverDictionary = new Dictionary<string, Weapons>();

    [SerializeField]
    private string currentHandHoldObjectType; // ���� �տ� ��� �ִ� ������ Ÿ��
    // ���� �տ� �鸰 ���������� static�� ����� ���� ��ũ��Ʈ���� Ŭ���������� �ٷ� ����
    public static Transform currentHandHoldObject;

    [SerializeField]
    // �ڷ�Ʈ �������� �� CorootRevolverController.cs Ȱ��ȭ, �ٸ� ������ �� ��Ȱ��ȭ
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
