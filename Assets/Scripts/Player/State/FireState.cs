using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireState : State
{
    public GameObject projectile;
    public Transform firePoint;
    private Bullet bullet;

    public FireState(Character _character, StateMachine _stateMachine, InputSystem _inputSystem) : base(_character, _stateMachine, _inputSystem)
    {
        character = _character;
        stateMachine = _stateMachine;
        inputSystem = _inputSystem;
    
        projectile = character.Projectile;
        firePoint = character.FirePoint;
    }

    public override void Enter()
    {
        base.Enter();

        ShootProjectile();
        character.StartCoroutine(FireCD());
        character.animator.SetTrigger("bullet");
        stateMachine.ChangeState(character.combatting);
    }

    IEnumerator FireCD()
    {
        character.isFireAvailable = false;
        UIManager.Default.SkillCD(2, character.FireCD);
        yield return new WaitForSeconds(character.FireCD);
        character.isFireAvailable = true;
    }

    void ShootProjectile()
    {
        InstantiateProjectileAtFirePoint();
    }

    void InstantiateProjectileAtFirePoint()
    {
        var projectileObj = GameObject.Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;

        bullet = projectileObj.GetComponent<Bullet>();
        RotateToDestination(projectileObj, firePoint.transform.forward * 1000, true);
        projectileObj.GetComponent<Rigidbody>().velocity = firePoint.transform.forward * bullet.speed;
    }

    void RotateToDestination(GameObject obj, Vector3 destination, bool onlyY)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if(onlyY)
        {
            rotation.x = 0;
            rotation.z = 0;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
    }
}
