using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
 
public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    public GameObject weapon;
    [SerializeField] GameObject weaponSheath;
    private DamageDealer damageDealer;
    public VisualEffect[] ProcessSlashes;
    [HideInInspector]
    public int attackNum = 0;
    
    private void Start() 
    {
        damageDealer = weapon.GetComponentInChildren<DamageDealer>();
    }

    public void DrawWeapon()
    {
        weapon.transform.parent = weaponHolder.transform;
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
    }
 
    public void SheathWeapon()
    {
        weapon.transform.parent = weaponSheath.transform;
        weapon.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
    }
 
    public void StartDealDamage()
    {
        damageDealer.StartDealDamage();
    }
    public void EndDealDamage()
    {
        damageDealer.EndDealDamage();
    }

    public void ProcessSlash(int num)
    {
        ProcessSlashes[num].Play();
    }
}