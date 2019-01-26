using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class PathCursor : MonoBehaviour {
    public float speed = 3;

    private LevelPath _path;
    private int _currentPoint;
    private float _offset;

    void Start() {
        _path = LevelPath.instance;
    }

    void Update() {
        _Step(Time.deltaTime * speed);
    }

    private void _Step(float distance) {
        float distanceLeft = distance;
        while (distanceLeft > 0) {
            bool isLastPoint = _currentPoint == _path.points.Length - 1;
            if (isLastPoint) {
                return;
            }

            Vector3 currentPoint = _path.points[_currentPoint];
            Vector3 nextPoint = _path.points[_currentPoint + 1];
            float distanceToNext =
                Vector3.Distance(transform.position, nextPoint);
            if (distanceLeft > distanceToNext) {
                distanceLeft -= distanceToNext;
                _offset = 0;
                _currentPoint++;
            } else {
                _offset += distanceLeft;
                distanceLeft = 0;
            }
            Vector3 direction = (nextPoint - currentPoint).normalized;
            transform.position = currentPoint + direction * _offset;
        }
    }
}

}
