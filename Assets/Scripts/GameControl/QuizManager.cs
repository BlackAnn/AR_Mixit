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


    //setup for quiz-modehel
    public void SetupGame() {
        CreateColorList();
        quizUIController.SetColorToMixImage(GetRandomColor());
        quizUIController.Show();
    }

    //Methode, die die Nutzer-Interaktion fuer das Spiel beginnen laesst
    public void StartGame() {
        gameManager.ActivateUserInteraction();
    }

    public void ContinueGame() {
        if (cNames.Count <= 0) {
            CreateColorList();
        }

        cNames.Remove(quizUIController.GetColorToMixImageColor());
        quizUIController.SetColorToMixImage(GetRandomColor());
    }

    public Color GetRandomColor() {
        int rnd = Random.Range(0, cNames.Count);
        Color rndColor = cNames[rnd];

        return rndColor;
    }

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

    //DUMMY_METHODE: Methode, die aufgerufen wird, wenn fertig gemischt wurde
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