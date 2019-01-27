using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Boulder : MonoBehaviour {
    public float speed = 20;
    // public float radius = 5;
    public float startAtDistance;

    public Animator animator;

    private PathCursor _cursor;
    private Transform _transform;

    private DistanceOnPath _playerDistance;

    void Awake() {
        Debug.Assert(animator != null);

        _cursor = GetComponent<PathCursor>();
        Debug.Assert(_cursor != null);

        // _transform = transform.Find("Boulder");
        // Debug.Assert(_transform != null);

        _playerDistance = GameObject.Find("Player")
            .GetComponent<DistanceOnPath>();
        Debug.Assert(_playerDistance != null);
    }

    void Update() {
        // TODO
        // _Roll();

        if (_playerDistance.distance > startAtDistance) {
            animator.enabled = true;
        }
    }

    // private void _Roll() {
    //     float angle = _cursor.distance * 360 / (2 * Mathf.PI * radius);
    //     _transform.localEulerAngles = new Vector3(angle, 0, 0);
    // }
}

}
