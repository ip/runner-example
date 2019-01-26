using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner {

public class LevelPath : MonoBehaviour {
    public static LevelPath instance;

    public Vector3[] points;
    public int debugDrawPointIndex = 0;

    void Awake() {
        instance = this;
        _RetrievePathPoints();
    }

    // void Update() {
    //     _DebugDraw();
    // }

    private void _RetrievePathPoints() {
        var mesh = GetComponent<MeshFilter>().mesh;

        points = mesh.vertices
            .Select(transform.TransformPoint)
            .ToArray();
    }

    private void _DebugDraw() {
        Vector3 x = points[debugDrawPointIndex];
        Debug.DrawLine(x, x + Vector3.up, Color.red);
    }
}

}
