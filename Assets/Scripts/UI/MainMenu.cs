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

    private string learnModeTxt = "Halte 2 Karten vor die Kamera.\nMische die Farbbaelle indem du mit dem Finger zwischen ihnen hin und her steichst.";
    private string quizModeTxt = "Schau dir an welche Farbe gemischt werden soll.\nWaehle 2 Karten aus und halte sie vor die Kamera.\nMische die Farbbaelle indem du mit dem Finger zwischen ihnen hin und her steichst.\nOb du richtig lagst erfaehrst du danach!";

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
        ModeTitle.GetComponent<TextMeshProUGUI>().text = GameModeController.gameMode+"-Modus";
        switch (GameModeController.gameMode) {
            case "Learn":
                ModeHelpText.GetComponent<TextMeshProUGUI>().text = learnModeTxt;
                break;
            case "Quiz":
                ModeHelpText.GetComponent<TextMeshProUGUI>().text = quizModeTxt;
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
