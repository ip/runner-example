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

    void Start() {
        _controller = GetComponent<CharacterController>();
        Debug.Assert(_controller != null);
    }

    void Update() {
        if (_controller.isGrounded) {
            _moveSpeed = _CalculateMoveDirection();
            _moveSpeed *= speed;

            if (Input.GetButtonDown("Jump")) {
                _moveSpeed.y = jumpSpeed;
            }
        }

        // Apply gravity
        _moveSpeed.y -= gravity * Time.deltaTime;

        // Move the controller
        _controller.Move(_moveSpeed * Time.deltaTime);
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

        Debug.DrawLine(transform.position, transform.position + cameraDirection,
            Color.red);

        var transformMat = Matrix4x4.LookAt(
            from: transform.position,
            to: transform.position + cameraDirection,
            up: Vector3.up
        );

        return transformMat.MultiplyVector(direction);
    }
}

}
