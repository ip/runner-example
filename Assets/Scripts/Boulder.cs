using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class Boulder : MonoBehaviour {
    public float speed = 20;

    private PathCursor _cursor;

    void Awake() {
        _cursor = GetComponent<PathCursor>();
        Debug.Assert(_cursor != null);
    }

    void Update() {
        _cursor.Step(Time.deltaTime * speed);
    }
}

}
