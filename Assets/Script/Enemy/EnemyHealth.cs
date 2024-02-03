using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 5;
    int currentHitPoints = 0;
    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--;

        if (currentHitPoints < 1)
        {
            enemy.RewardGold(); 
            gameObject.SetActive(false);
        }
    }
}
