using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runner {

public class LinearPath : MonoBehaviour {
    public static LinearPath instance;

    public Vector3[] positions;
    public Vector3[] normals;
    public float[] distances;
    public int debugDrawPointIndex = 0;
    public int PointCount => positions.Length;

    void Awake() {
        instance = this;

        var mesh = GetComponent<MeshFilter>().mesh;

        _GetPoints(mesh);
        _CalculateNormals();
        _CalculateDistances();
    }

    // void Update() {
    //     _DebugDraw();
    // }

    public Vector3 GetDirection(int index) =>
        index == positions.Length - 1 ?
            GetDirection(index - 1) :
            (positions[index + 1] - positions[index]).normalized;

    public Plane GetSegmentPlane(int index) {
        var plane = new Plane();
        plane.SetNormalAndPosition(
            inNormal: GetDirection(index),
            inPoint: positions[index]
        );
        return plane;
    }

    public float GetSegmentLength(int index) =>
        (positions[index + 1] - positions[index]).magnitude;

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

    private void _CalculateDistances() {
        distances = new float[PointCount];
        float distance = 0;
        distances[0] = 0;

        for (int i = 1; i < PointCount; ++i) {
            distance += GetSegmentLength(i - 1);
            distances[i] = distance;
        }
    }
}

}
