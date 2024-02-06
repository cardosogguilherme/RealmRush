using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceble;
    public bool IsPlaceble { get { return isPlaceble;} }

    private void OnMouseDown() {
        if (isPlaceble)
        {
            var wasTowerPlaced = towerPrefab.CreateTower(towerPrefab, transform.position);
            isPlaceble = !wasTowerPlaced;
        }
    }
}
