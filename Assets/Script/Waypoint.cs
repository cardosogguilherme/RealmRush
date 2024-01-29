using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] bool isPlaceble;

    private void OnMouseDown() {
        if (isPlaceble)
        {
            Debug.Log(transform.name);
        }
    }
}
