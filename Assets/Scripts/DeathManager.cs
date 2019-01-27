using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner {

public class DeathManager : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Kills on collision") {
            _Die();
        }
    }

    private void _Die() {
        gameObject.SetActive(false);

        RestartDialog.instance.Show(isDead: true);
    }
}

}
