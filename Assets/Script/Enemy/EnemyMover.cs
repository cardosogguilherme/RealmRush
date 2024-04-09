using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;

    Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Tile waypoint = child.GetComponent<Tile>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
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

            if (directionToWaypoint == Vector3.zero)
            {
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

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
