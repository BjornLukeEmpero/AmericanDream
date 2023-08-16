using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorootRevolverController : WeaponsController
{
    public static bool isActivate = false;
    
    // Start is called before the first frame update
    void Start()
    {
        HandHoldObjectManager.currentHandHoldObject = currentHoldWeapon.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate)
            TryFire();
    }

    public override void WeaponChange(Weapons weapons)
    {
        base.WeaponChange(weapons);
        isActivate = true;
    }
}
