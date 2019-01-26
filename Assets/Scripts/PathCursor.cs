using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

// Moves along the path.
public class PathCursor : MonoBehaviour {
    private LevelPath _path;

    // Index of the current point
    private int currentIndex;

    // Offset from the current point towards the next one
    private float offset;

    void Start() {
        _path = LevelPath.instance;
    }

    public void Step(float distance) {
        float distanceLeft = distance;
        while (distanceLeft > 0) {
            bool isLastPoint = currentIndex == _path.positions.Length - 1;
            if (isLastPoint) {
                return;
            }

            Vector3 currentPoint = _path.positions[currentIndex];
            Vector3 nextPoint = _path.positions[currentIndex + 1];
            float distanceToNext =
                Vector3.Distance(transform.position, nextPoint);
            if (distanceLeft > distanceToNext) {
                distanceLeft -= distanceToNext;
                offset = 0;
                currentIndex++;
            } else {
                offset += distanceLeft;
                distanceLeft = 0;
            }

            Vector3 direction = (nextPoint - currentPoint).normalized;
            transform.position = currentPoint + direction * offset;

            Vector3 directionInterpolated = _GetInterpolatedDirection();
            transform.LookAt(
                worldPosition: transform.position + directionInterpolated,
                Vector3.up
            );
        }
    }

    private Vector3 _GetDirection(int index) =>
        index == _path.positions.Length - 1 ?
            _GetDirection(index - 1) :
            (_path.positions[index + 1] - _path.positions[index]).normalized;

    private Vector3 _GetInterpolatedDirection() {
        bool isLastPoint = currentIndex == _path.positions.Length - 1;
        if (isLastPoint) {
            return _GetDirection(currentIndex);
        }

        Vector3 currentPoint = _path.positions[currentIndex];
        Vector3 nextPoint = _path.positions[currentIndex + 1];
        float factor = offset / Vector3.Distance(currentPoint, nextPoint);
        return Vector3.Lerp(
            _GetDirection(currentIndex),
            _GetDirection(currentIndex + 1),
            factor
        );
    }
}

}
