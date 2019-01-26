using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Player : MonoBehaviour {
    public float moveSpeed = 30;
    public float jumpSpeed = 30;

    private Rigidbody _rigidbody;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(_rigidbody != null);
    }

    void FixedUpdate() {
        _Move();
        _Jump();
    }

    private void _Move() {
        var velocity = _rigidbody.velocity;
        velocity.x = Input.GetAxis("Vertical") * moveSpeed;
        velocity.z = -Input.GetAxis("Horizontal") * moveSpeed;
        _rigidbody.velocity = velocity;
    }

    private void _Jump() {
        var velocity = _rigidbody.velocity;
        if (Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = jumpSpeed;
        }
        _rigidbody.velocity = velocity;
    }
}

}
