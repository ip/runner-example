using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runner {

public class RestartDialog : MonoBehaviour {
    public static RestartDialog instance;

    private Text _text;

    void Awake() {
        instance = this;

        _text = transform.Find("Text")
            .GetComponent<Text>();
        Debug.Assert(_text != null);

        _BindRestartButton();

        gameObject.SetActive(false);
    }

    public void Show(bool isDead) {
        gameObject.SetActive(true);

        _text.text = isDead ? "You're dead!" : "You win!";
    }

    private void _BindRestartButton() {
        transform.Find("Restart button").GetComponent<Button>()
            .onClick.AddListener(_RestartGame);
    }

    private void _RestartGame() {
        SceneManager.LoadScene(0);
    }
}

}
