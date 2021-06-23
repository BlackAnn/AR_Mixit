using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public GameObject ColorToMixImg;

    public GameObject LearnModeUI;
    public GameObject QuizModeUI;

    public void GoToMainMenu() {
        GameModeController.menuMode = "Options";
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameModeController.gameMode.Equals("Learn")) {
            LearnModeUI.SetActive(true);
            QuizModeUI.SetActive(false);

        } else if (GameModeController.gameMode.Equals("Quiz")) {
            LearnModeUI.SetActive(false);
            QuizModeUI.SetActive(true);

        }
        ColorToMixImg = GameObject.Find("ColorToMixImage");
    }

    void Update() {
        switch(GameModeController.gameMode) {
            case "Learn":
                break;
            case "Quiz":
                break;
        }
    }

    public void pickColorToMix() {
    }

    public void checkColor() {
    }

    public void showColorInfo() {

    }
}
