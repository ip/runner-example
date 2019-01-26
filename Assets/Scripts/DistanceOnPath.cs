using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

// Calculates how far the current object is along the path.
public class DistanceOnPath : MonoBehaviour {
    public float distance;

    private LinearPath _path;

    private int _segmentIndex;

    void Start() {
        _path = LinearPath.instance;
    }

    void Update() {
        RaycastHit hitInfo;
        Physics.Raycast(
            origin: transform.position + Vector3.up,
            direction: Vector3.down,
            out hitInfo
        );
        Vector3 position = hitInfo.point;
        _DrawPoint(position, Color.white);

        _UpdateSegmentIndex(position);

        Plane startPlane = _path.GetSegmentPlane(_segmentIndex);
        Plane endPlane = _path.GetSegmentPlane(_segmentIndex + 1);

        var direction = _path.GetDirection(_segmentIndex);
        var ray = new Ray(position, direction);
        float startDistance, endDistance;
        startPlane.Raycast(ray, out startDistance);
        endPlane.Raycast(ray, out endDistance);
        // Debug.Assert(startDistance < 0);
        // Debug.Assert(endDistance > 0);
        startDistance = Mathf.Abs(startDistance);
        endDistance = Mathf.Abs(endDistance);

        float offsetFactor = startDistance / (startDistance + endDistance);
        var offsetLength = _path.GetSegmentLength(_segmentIndex) * offsetFactor;
        distance = _path.distances[_segmentIndex] + offsetLength;
    }

    private void _DrawPoint(Vector3 position, Color color) {
        Debug.DrawLine(position, position + Vector3.up, color);
    }

    private void _UpdateSegmentIndex(Vector3 position) {
        int currentSegment = _segmentIndex > 0 ? _segmentIndex - 1 : 0;
        while (true) {
            Plane segmentPlane = _path.GetSegmentPlane(currentSegment);
            bool isOnPositiveSide = segmentPlane.GetSide(position);
            bool nextSegmentIsLast = currentSegment == _path.PointCount - 1;

            if (!isOnPositiveSide || nextSegmentIsLast) {
                _segmentIndex = currentSegment - 1;
                return;
            }

            currentSegment++;
        }
    }
}

}
