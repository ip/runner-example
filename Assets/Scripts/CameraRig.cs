using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class CameraRig : MonoBehaviour {
    public float offsetFromPlayer = 0;

    private DistanceOnPath _distanceComponent;
    private PathCursor _cursor;

    void Start() {
        _distanceComponent = GameObject.Find("Player")
            .GetComponent<DistanceOnPath>();
        Debug.Assert(_distanceComponent != null);

        _cursor = GetComponent<PathCursor>();
        Debug.Assert(_cursor != null);
    }

    void Update() {
        _cursor.distance = _distanceComponent.distance + offsetFromPlayer;
    }
}

}
