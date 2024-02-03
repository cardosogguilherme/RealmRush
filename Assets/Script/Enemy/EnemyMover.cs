using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject waypointsParent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform waypoint in waypointsParent.transform)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (var waypoint in path)
        {
            var startPosition = transform.position;
            var endPosition = waypoint.transform.position;
            float travelPercent = 0f;
            
            var currentDirection = transform.forward != Vector3.zero ? transform.forward : new Vector3Int(0, 0, 1);
            var directionToWaypoint = (waypoint.transform.position - transform.position).normalized;
            var timer = 0f;
        
            if (directionToWaypoint == Vector3.zero) {
                directionToWaypoint = new Vector3Int(0, 0, 1);
            }

            while (!Mathf.Approximately(Vector3.Dot(transform.forward, directionToWaypoint), 1f))
            {
                // Debug.Log($"currentDirection:{currentDirection} directionToWaypoint:{directionToWaypoint}");
                transform.forward = Vector3.Slerp(currentDirection, directionToWaypoint, timer * 10f);
                timer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            // transform.LookAt(endPosition);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        
        gameObject.SetActive(false);
    }
}
