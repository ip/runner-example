using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

// Moves along the path.
public class PathCursor : MonoBehaviour {
    // Index of the current point
    public int currentIndex;

    // Offset from the current point towards the next one
    public float offset;

    public float totalDistance;

    private LinearPath _path;

    void Start() {
        _path = LinearPath.instance;
    }

    void Update() {
        _UpdateTransform();
    }

    public void Step(float distance) {
        float distanceLeft = distance;
        while (distanceLeft > 0) {
            bool isLastPoint = currentIndex == _path.positions.Length - 1;
            if (isLastPoint) {
                return;
            }

            Vector3 nextPoint = _path.positions[currentIndex + 1];
            float distanceToNext =
                Vector3.Distance(transform.position, nextPoint);
            if (distanceLeft > distanceToNext) {
                totalDistance += distanceToNext;
                distanceLeft -= distanceToNext;
                offset = 0;
                currentIndex++;
            } else {
                totalDistance += distanceLeft;
                offset += distanceLeft;
                distanceLeft = 0;
            }
        }
    }

    private void _UpdateTransform() {
        Vector3 direction = _path.GetDirection(currentIndex);
        transform.position = _path.positions[currentIndex]
            + direction * offset;

        Vector3 directionInterpolated = _GetInterpolatedDirection();
        transform.LookAt(
            worldPosition: transform.position + directionInterpolated,
            Vector3.up
        );
    }

    private Vector3 _GetInterpolatedDirection() {
        bool isLastPoint = currentIndex == _path.positions.Length - 1;
        if (isLastPoint) {
            return _path.GetDirection(currentIndex);
        }

        Vector3 currentPoint = _path.positions[currentIndex];
        Vector3 nextPoint = _path.positions[currentIndex + 1];
        float factor = offset / Vector3.Distance(currentPoint, nextPoint);
        return Vector3.Lerp(
            _path.GetDirection(currentIndex),
            _path.GetDirection(currentIndex + 1),
            factor
        );
    }
}

}
