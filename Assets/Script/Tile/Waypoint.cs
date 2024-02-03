using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceble;
    public bool IsPlaceble { get { return isPlaceble;} }

    private void OnMouseDown() {
        if (isPlaceble)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceble = false;
        }
    }
}
