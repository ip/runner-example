using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner {

public class LevelPath : MonoBehaviour {
    public Vector3[] _points;
    public int debugDrawPointIndex = 0;

    void Start() {
        _RetrievePathPoints();
    }

    // void Update() {
    //     _DebugDraw();
    // }

    private void _RetrievePathPoints() {
        var mesh = GetComponent<MeshFilter>().mesh;

        _points = mesh.vertices
            .Select(transform.TransformPoint)
            .ToArray();
    }

    private void _DebugDraw() {
        Vector3 x = _points[debugDrawPointIndex];
        Debug.DrawLine(x, x + Vector3.up, Color.red);
    }
}

}
