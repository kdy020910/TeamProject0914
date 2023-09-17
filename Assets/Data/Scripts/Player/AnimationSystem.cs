using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSystem : SystemProPerty
{
    // Player���� �ִϸ��̼�, �Ķ���� ����
    protected void SetWeaponAnimationParameter(WeaponType weaponType)
    {
        myAnim.SetBool("IsAxe", weaponType == WeaponType.Axe || weaponType == WeaponType.FragileAxe || weaponType == WeaponType.GoldAxe);
        myAnim.SetBool("IsShovel", weaponType == WeaponType.Shovel || weaponType == WeaponType.FragileShovel || weaponType == WeaponType.GoldShovel);
        myAnim.SetBool("IsFishingPole", weaponType == WeaponType.FishingPole || weaponType == WeaponType.FragileFishingPole);

        myAnim.SetBool("IsRake", weaponType == WeaponType.Rake);
        myAnim.SetBool("IsWateringCan", weaponType == WeaponType.WateringCan);
        myAnim.SetBool("IsSeed", weaponType == WeaponType.Carrot || weaponType == WeaponType.Corn || weaponType == WeaponType.Pumpkin || weaponType == WeaponType.Tomato || weaponType == WeaponType.Potato);
    }

    protected void AnimationParameterFalse()
    {
        myAnim.SetBool("IsAxe", false);
        myAnim.SetBool("IsWateringCan", false);
        myAnim.SetBool("IsShovel", false);
        myAnim.SetBool("IsFishingPole", false);
        myAnim.SetBool("IsRake", false);
        myAnim.SetBool("IsSeed", false);
    }
    //����
    protected void HandleAxeAction()
    {
        myAnim.SetTrigger("Axing");
    }
    //��
    protected void HandleShovelAction()
    {
        myAnim.SetTrigger("Shoveling");
    }
    // ���Ѹ���
    protected void HandleWateringCanAction()
    {
        myAnim.SetTrigger("Sprayingwater");
    }
}
