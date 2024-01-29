using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem boltParticleSystem;
    Transform target;

    void Start()
    {
        target = FindObjectOfType<EnemyMover>()?.transform;
    }

    void Update()
    {
        AimWeapon();
    }

    private void AimWeapon()
    {
        if (target == null) {
            boltParticleSystem.Stop();
            return;
        } else if (boltParticleSystem.isStopped) {
            boltParticleSystem.Play();
        }

        weapon.LookAt(target);
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
}
