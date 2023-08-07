using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    public enum Weapon { Melee, Ranged }

    public static Weapon currentWeapon;

    public static float currentCooldown;
    public static float maxCooldown;

    public static bool hasRanged;

    void Start()
    {
        currentWeapon = Weapon.Melee;
    }

    void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (!Player.isAlive || currentCooldown > 0) return;

        if (Input.GetKeyDown(KeyCode.Alpha1) && currentWeapon != Weapon.Melee)
        {
            currentWeapon = Weapon.Melee;
            AudioManager.instance.PlaySfx(AudioManager.SFX.Swap);
            ApplyCooldown(.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentWeapon != Weapon.Ranged && hasRanged)
        {
            currentWeapon = Weapon.Ranged;
            AudioManager.instance.PlaySfx(AudioManager.SFX.Swap);
            ApplyCooldown(.5f);
        }
    }

    public static void ApplyCooldown(float time)
    {
        maxCooldown = currentCooldown = time;
    }
}
