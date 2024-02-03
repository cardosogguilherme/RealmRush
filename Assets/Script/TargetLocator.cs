using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem boltParticleSystem;
    [SerializeField] float range = 15f;
    Transform target;

   void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        Attack(targetDistance < range);
    

        // var currentDirection = weapon.forward != Vector3.zero ? weapon.forward : new Vector3Int(0, 0, 1);
        // var directionToWaypoint = (target.position - weapon.position).normalized;
        // var timer = 0f;

        // if (directionToWaypoint == Vector3.zero)
        // {
        //     directionToWaypoint = new Vector3Int(0, 0, 1);
        // }

        // while (!Mathf.Approximately(Vector3.Dot(weapon.forward, directionToWaypoint), 1f))
        // {
        //     weapon.forward = Vector3.Slerp(currentDirection, directionToWaypoint, timer * 10f);
        //     timer += Time.deltaTime;
        // }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = boltParticleSystem.emission;
        emissionModule.enabled = isActive;
    }
}
