using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Boulder : MonoBehaviour {
    public float speed = 20;
    float radius = 5;

    private PathCursor _cursor;
    private Transform _transform;

    void Awake() {
        _cursor = GetComponent<PathCursor>();
        Debug.Assert(_cursor != null);

        _transform = transform.Find("Boulder");
        Debug.Assert(_transform != null);
    }

    void Update() {
        _cursor.Step(Time.deltaTime * speed);
        _Roll();
    }

    private void _Roll() {
        float angle = _cursor.totalDistance * 360 / (2 * Mathf.PI * radius);
        _transform.localEulerAngles = new Vector3(angle, 0, 0);
    }
}

}
