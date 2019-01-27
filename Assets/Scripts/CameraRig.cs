using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class CameraRig : MonoBehaviour {
    public float animationDuration;

    private Animator _animator;
    private DistanceOnPath _distanceComponent;
    private float _totalDistance;

    void Start() {
        _animator = GetComponent<Animator>();
        Debug.Assert(_animator != null);

        _distanceComponent = GameObject.Find("Player")
            .GetComponent<DistanceOnPath>();
        Debug.Assert(_distanceComponent != null);

        Debug.Assert(animationDuration > 0);

        _totalDistance = LinearPath.instance.Length;
    }

    void FixedUpdate() {
        float currentDistance = _distanceComponent.distance;
        float currentTime =
            currentDistance * animationDuration / _totalDistance;
        _SetAnimationTime(currentTime);
    }

    private void _SetAnimationTime(float time) {
        float normalizedTime = time / animationDuration;
        normalizedTime = Mathf.Clamp(normalizedTime, 0, 0.9999f);

        _animator.Play(
            stateName: "Camera",
            layer: 0,
            normalizedTime
        );
    }
}

}
