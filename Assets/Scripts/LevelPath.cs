using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner {

public class LevelPath : MonoBehaviour {
    public static LevelPath instance;

    public Vector3[] positions;
    public Vector3[] normals;
    public int debugDrawPointIndex = 0;

    void Awake() {
        instance = this;
        _GetVertices();
    }

    // void Update() {
    //     _DebugDraw();
    // }

    private void _GetVertices() {
        var mesh = GetComponent<MeshFilter>().mesh;

        _GetPoints(mesh);
        _CalculateNormals();
    }

    private void _GetPoints(Mesh mesh) {
        positions = mesh.vertices
            .Select(transform.TransformPoint)
            .ToArray();
    }

    private void _CalculateNormals() {
        var normals = new Vector3[positions.Length];

        for (int i = 0; i < positions.Length; ++i) {
            Vector3 normal;
            if (i == positions.Length - 1) {
                normal = normals[positions.Length - 2];
            } else {
                var lookAtMatrix = Matrix4x4.LookAt(
                    from: positions[i],
                    to: positions[i + 1],
                    up: Vector3.up
                );
                normal = lookAtMatrix.MultiplyVector(Vector3.up);
            }
            normals[i] = normal;
        }

        this.normals = normals;
    }

    private void _DebugDraw() {
        Vector3 position = positions[debugDrawPointIndex];
        Vector3 normal = normals[debugDrawPointIndex];
        Debug.DrawLine(position, position + normal, Color.red);
    }
}

}
