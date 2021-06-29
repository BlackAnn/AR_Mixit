using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {

    [SerializeField] private QuizUIController quizUIController;
    [SerializeField] private GameManager gameManager;

    List<Color> cNames;
    List<ColorPreset> cList;

    private Color currentColor;


    //setup for quiz-mode
    public void SetupGame() {
        CreateColorList();
        quizUIController.SetColorToMixImage(GetRandomColor());
        quizUIController.Show();
    }

    //starts user-interaction with the game
    public void StartGame() {
        gameManager.ActivateUserInteraction();
    }

    //method to continue the game after mixing a color
    public void ContinueGame() {
        if (cNames.Count <= 0) {
            CreateColorList();
        }

        cNames.Remove(quizUIController.GetColorToMixImageColor());
        quizUIController.SetColorToMixImage(GetRandomColor());
    }

    //picks a random color out of the color list
    public Color GetRandomColor() {
        int rnd = Random.Range(0, cNames.Count);
        Color rndColor = cNames[rnd];

        return rndColor;
    }

    //generates a color list with mixable colors
    public void CreateColorList() {
        cNames = new List<Color>();
        cList = new List<ColorPreset>();
        cList = ColorPreset.GetValues();

        for (int j = 0; j < cList.Count; j++) {
            if (cList[(int)j].GetMixable()) {
                cNames.Add(cList[j].GetColor());
            }
        }
    }

    //gets called when a color was mixed
    public void EvaluateResult(Color resultColor) {
        bool result;
        currentColor = quizUIController.GetColorToMixImageColor();

        if (currentColor == resultColor) {
            result = true;
        } else {
            result = false;
        }
        quizUIController.ShowMixResult(result, ColorPreset.GetDisplayNameByColor(currentColor), ColorPreset.GetDisplayNameByColor(resultColor));
    }
}