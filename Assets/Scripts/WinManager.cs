using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class WinManager : MonoBehaviour {
    public float winAtDistance;

    private DistanceOnPath _distanceComponent;

    void Start() {
        _distanceComponent = GetComponent<DistanceOnPath>();
        Debug.Assert(_distanceComponent != null);

        Debug.Assert(winAtDistance > 0);
    }

    void Update() {
        if (_distanceComponent.distance > winAtDistance) {
            RestartDialog.instance.Show(isDead: false);
        }
    }
}

}
