using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    List<Node> path = new List<Node>();

    Enemy enemy;
    private Pathfinder pathfinder;
    private GridManager gridManager;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void OnEnable()
    {
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void RecalculatePath()
    {
        path.Clear();
        path = pathfinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            var startPosition = transform.position;
            var endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            var currentDirection = transform.forward != Vector3.zero ? transform.forward : new Vector3Int(0, 0, 1);
            var directionToWaypoint = (endPosition - transform.position).normalized;
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
