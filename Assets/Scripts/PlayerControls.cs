using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class PlayerControls : MonoBehaviour {
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 _moveSpeed = Vector3.zero;
    private CharacterController _controller;
    private int _surfaceLayerMask;

    void Start() {
        _controller = GetComponent<CharacterController>();
        Debug.Assert(_controller != null);

        _surfaceLayerMask = LayerMask.NameToLayer("Distance calculation");
    }

    void Update() {
        bool moveAlongSurface = false;
        if (_controller.isGrounded) {
            _moveSpeed = _CalculateMoveDirection();
            _moveSpeed *= speed;

            bool shouldJump = Input.GetButtonDown("Jump");
            moveAlongSurface = !shouldJump;
            if (shouldJump) {
                _moveSpeed.y = jumpSpeed;
            }
        }

        bool hasMovedAlongSurface = false;
        if (moveAlongSurface) {
            hasMovedAlongSurface = _MoveAlongSurface();
        }

        if (!hasMovedAlongSurface) {
            // Move using CharacterController

            // Apply gravity
            _moveSpeed.y -= gravity * Time.deltaTime;

            // Move the controller
            _controller.Move(_moveSpeed * Time.deltaTime);
        }
    }

    private Vector3 _CalculateMoveDirection() {
        var direction = new Vector3(
            -Input.GetAxis("Horizontal"),
            0.0f,
            -Input.GetAxis("Vertical")
        );

        Vector3 cameraDirection = Camera.main.cameraToWorldMatrix
            .MultiplyVector(Vector3.forward);
        cameraDirection.y = 0;
        cameraDirection.Normalize();

        var transformMat = Matrix4x4.LookAt(
            from: transform.position,
            to: transform.position + cameraDirection,
            up: Vector3.up
        );

        return transformMat.MultiplyVector(direction);
    }

    // Returns true, if successfully moved and snapped to the surface.
    private bool _MoveAlongSurface() {
        Vector3 moveDelta = _moveSpeed * Time.deltaTime;

        if (moveDelta.magnitude < 0.01f) {
            return true;
        }

        // Distance from ground to player pivot
        float playerOffset = 1.99f;
        RaycastHit hitInfo;
        bool rayIntersected = Physics.Raycast(
            origin: transform.position + moveDelta,
            direction: Vector3.down,
            out hitInfo,
            maxDistance: playerOffset + 1f,
            layerMask: _surfaceLayerMask
        );
        if (!rayIntersected) {
            return false;
        }
        Vector3 targetPosition = hitInfo.point + Vector3.up * playerOffset;
        _controller.Move(targetPosition - transform.position);

        return true;
    }
}

}
