using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

    public GameObject LearnModeButton;
    public GameObject QuizModeButton;

    public GameObject MainMenuUI;
    public GameObject OptionsUI;

    public GameObject ModeTitle;
    public GameObject ModeHelpText;

    public void PlayGame(string modeInput) {
        SceneManager.LoadScene(1);
        GameModeController.gameMode = modeInput;
    }

    public void CheckModeOptions() {
        if (GameModeController.gameMode.Equals("Learn")) {
            LearnModeButton.SetActive(false);
            QuizModeButton.SetActive(true);
        } else if (GameModeController.gameMode.Equals("Quiz")) {
            LearnModeButton.SetActive(true);
            QuizModeButton.SetActive(false);
        }
    }

    public void CheckMenuOption() {
        if (GameModeController.menuMode.Equals("Options")) {
            GameModeController.previousWindow = GameModeController.gameMode;
            MainMenuUI.SetActive(false);
            OptionsUI.SetActive(true);
        } else if (GameModeController.menuMode.Equals("MainMenu")) {
            MainMenuUI.SetActive(true);
            OptionsUI.SetActive(false);
        }
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void GoToOptions() {
        GameModeController.previousWindow = "MainMenu";
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(true);
    }

    public void GoBack() {
        switch (GameModeController.previousWindow) {
            case "MainMenu":
                MainMenuUI.SetActive(true);
                OptionsUI.SetActive(false);
                break;
            case "Learn":
                SceneManager.LoadScene(1);
                break;
            case "Quiz":
                SceneManager.LoadScene(1);
                break;
        }
    }

    public void setHelpPage() {
        switch (GameModeController.gameMode) {
            case "Learn":
                ModeTitle.GetComponent<TextMeshProUGUI>().text = "";
                break;
            case "Quiz":

                break;
        }
    }

    public void QuitApplication() {
        Debug.Log("quitting app");
        Application.Quit();
    }

    void Start() {
        CheckMenuOption();
        CheckModeOptions();

        setHelpPage();
    }
}
