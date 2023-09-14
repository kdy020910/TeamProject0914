using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : SystemProPerty
{
    // Player관련 애니메이션, 파라메터 관리
    protected void SetWeaponAnimationParameter(WeaponType weaponType)
    {
        myAnim.SetBool("IsAxe", weaponType == WeaponType.Axe || weaponType == WeaponType.FragileAxe || weaponType == WeaponType.GoldAxe);
        myAnim.SetBool("IsShovel", weaponType == WeaponType.Shovel || weaponType == WeaponType.FragileShovel || weaponType == WeaponType.GoldShovel);
        myAnim.SetBool("IsFishingPole", weaponType == WeaponType.FishingPole || weaponType == WeaponType.FragileFishingPole);

        myAnim.SetBool("IsRake", weaponType == WeaponType.Rake);
        myAnim.SetBool("IsWateringCan", weaponType == WeaponType.WateringCan);
    }

    protected void AnimationParameterFalse()
    {
        myAnim.SetBool("IsAxe", false);
        myAnim.SetBool("IsWateringCan", false);
        myAnim.SetBool("IsShovel", false);
        myAnim.SetBool("IsFishingPole", false);
        myAnim.SetBool("IsRake", false);
    }
}
