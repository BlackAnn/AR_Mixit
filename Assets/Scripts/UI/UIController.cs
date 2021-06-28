using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private GameObject learnModeTxt;

    void Start() {
        learnModeTxt = GameObject.Find("LearnHelpText");
    }

    public void Show() {
        Debug.Log("GAMEMODE: " + GameModeController.gameMode);

        GameModeController.previousWindow = GameModeController.gameMode;
        gameObject.SetActive(true);
        StartCoroutine(RemoveAfterSeconds(5, learnModeTxt));
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj) {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
