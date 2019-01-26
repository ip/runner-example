using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

// Moves along the path.
[ExecuteInEditMode]
public class PathCursor : MonoBehaviour {
    public float distance;

    private LinearPath _path;
    private float _previousDistance = float.PositiveInfinity;

    // Index of the current point
    private int _currentIndex;

    // Offset from the current point towards the next one
    private float _offset;

    void Start() {
        _path = LinearPath.instance;
    }

    void Update() {
        bool needUpdate = distance != _previousDistance;
        if (!needUpdate) {
            return;
        }

        _previousDistance = distance;
        _UpdateCurrentIndexAndOffset();
        _UpdateTransform();
    }

    private void _UpdateCurrentIndexAndOffset() {
        _currentIndex = FindMaximumLowerValue(_path.distances, distance);
        _offset = distance - _path.distances[_currentIndex];
    }

    private void _UpdateTransform() {
        Vector3 direction = _path.GetDirection(_currentIndex);
        transform.position = _path.positions[_currentIndex]
            + direction * _offset;

        Vector3 directionInterpolated = _GetInterpolatedDirection();
        transform.LookAt(
            worldPosition: transform.position + directionInterpolated,
            Vector3.up
        );
    }

    private Vector3 _GetInterpolatedDirection() {
        bool isLastPoint = _currentIndex == _path.positions.Length - 1;
        if (isLastPoint) {
            return _path.GetDirection(_currentIndex);
        }

        Vector3 currentPoint = _path.positions[_currentIndex];
        Vector3 nextPoint = _path.positions[_currentIndex + 1];
        float factor = _offset / Vector3.Distance(currentPoint, nextPoint);
        return Vector3.Lerp(
            _path.GetDirection(_currentIndex),
            _path.GetDirection(_currentIndex + 1),
            factor
        );
    }

    private static int FindMaximumLowerValue(float[] arr, float item) {
        return FindMaximumLowerValue(arr, item, 0, arr.Length);
    }

    // TODO: binary search
    private static int FindMaximumLowerValue(
        float[] arr, float item, int start, int end
    ) {
        for (int i = 0; i < arr.Length; ++i) {
            if (arr[i] > item) {
                return i > 0 ? i - 1 : 0;
            }
        }
        return arr.Length - 1;
    }
}

}
