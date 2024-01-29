using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            
            // var currentDirection = transform.forward;
            // var directionToWaypoint = (waypoint.transform.position - transform.position).normalized;
            // var timer = 0f;
            
            // while (!Mathf.Approximately(Vector3.Dot(transform.forward, directionToWaypoint), 1f))
            // {
            //     transform.forward = Vector3.Slerp(currentDirection, directionToWaypoint, timer * 10f);
            //     timer += Time.deltaTime;
            //     yield return new WaitForEndOfFrame();
            // }

            transform.LookAt(endPosition);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
