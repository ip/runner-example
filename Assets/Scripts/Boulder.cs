using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Boulder : MonoBehaviour {
    public float radius = 5;
    public float startAtDistance;

    public Animator animator;

    private PathCursor _cursor;
    private Transform _transformAnimated;
    private Transform _transformFree;
    private DistanceOnPath _playerDistance;

    // For roll
    private bool _justFell = true;
    private float _startDistance;

    void Awake() {
        Debug.Assert(animator != null);

        _cursor = GetComponent<PathCursor>();
        Debug.Assert(_cursor != null);

        _transformAnimated = transform.GetChild(0);
        Debug.Assert(_transformAnimated != null);

        _transformFree = _transformAnimated.GetChild(0);
        Debug.Assert(_transformFree != null);

        _playerDistance = GameObject.Find("Player")
            .GetComponent<DistanceOnPath>();
        Debug.Assert(_playerDistance != null);
    }

    void Update() {
        if (_playerDistance.distance > startAtDistance) {
            _KickOff();
        }

        _Roll();
    }

    private void _KickOff() => animator.enabled = true;

    // Rolling

    private void _Roll() {
        if (!_isOnGround) {
            return;
        }

        if (_justFell) {
            _justFell = false;
            _startDistance = _cursor.distance;
        }

        float distanceFromStart = _cursor.distance - _startDistance;
        float angle = distanceFromStart * 360 / (2 * Mathf.PI * radius);
        _transformFree.localEulerAngles = new Vector3(angle, 0, 0);
    }

    private bool _isOnGround => _transformAnimated.localPosition.y == radius;
}

}
