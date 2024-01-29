using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble;
    [SerializeField] GameObject towerPrefab;

    private void OnMouseDown() {
        if (isPlaceble)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceble = false;
        }
    }
}
