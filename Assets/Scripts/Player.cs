using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Player : MonoBehaviour {
    public float speed = 10;

    private Rigidbody _rigidbody;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        Debug.Assert(_rigidbody != null);
    }

    void FixedUpdate() {
        _Move();
    }

    private void _Move() {
        var velocity = _rigidbody.velocity;
        velocity.x = Input.GetAxis("Vertical") * speed;
        velocity.z = -Input.GetAxis("Horizontal") * speed;
        _rigidbody.velocity = velocity;
    }
}

}
