using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class PlayerSpin : MonoBehaviour {
    public float spinDuration = 1.5f;

    // Degrees per second
    public float speed = 360*2f;

    private Transform _transform;
    private bool _isSpinning = false;

    void Start() {
        _transform = transform.GetChild(0);
    }

    void Update() {
        if (Input.GetButtonDown("Fire1") && !_isSpinning) {
            StartCoroutine(_Spin());
        }
    }

    private IEnumerator _Spin() {
        _isSpinning = true;

        float endTime = Time.time + spinDuration;
        while (Time.time < endTime) {
            float deltaAngle = Time.deltaTime * speed;
            _transform.Rotate(Vector3.up, deltaAngle, Space.Self);
            yield return new WaitForEndOfFrame();
        }
        _transform.localRotation = Quaternion.identity;

        _isSpinning = false;
    }
}

}
