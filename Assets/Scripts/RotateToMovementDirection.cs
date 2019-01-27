using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class RotateToMovementDirection : MonoBehaviour {
    private Vector3 _previousPosition;
    private Quaternion _targetRotation;

    void Update() {
        _UpdateTargetDirection();
        _Rotate();
    }

    private void _UpdateTargetDirection() {
        Vector3 delta = transform.position - _previousPosition;
        delta.y = 0;

        if (delta.magnitude > 0.01f) {
            var targetDirection = delta.normalized;
            _targetRotation = Quaternion.LookRotation(
                targetDirection, Vector3.up);
        }

        _previousPosition = transform.position;
    }

    private void _Rotate() {
        transform.rotation = _targetRotation;
    }
}

}
