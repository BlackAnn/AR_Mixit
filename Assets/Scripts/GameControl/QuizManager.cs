using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//@Anna in der QuizUIController kannst du die ganzen Methoden schreiben, die die UI verändern und diese dann dann hier (mit quizUI) aufrufen
public class QuizManager : MonoBehaviour {

    [SerializeField] private QuizUIController quizUIController;
    [SerializeField] private GameManager gameManager;

    List<Color> cNames;
    List<ColorPreset> cList;

    //private Image colorToMixImg;

    //private GameObject quizModeUI;

    // Start is called before the first frame update
    void Start() {
        //colorToMixImg = GameObject.Find("ColorToMixImage").GetComponent<Image>();
    }

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
        quizUIController.SetColorToMixImage(GetRandomColor());
    }

    public Color GetRandomColor() {
        int rnd = Random.Range(0, cNames.Count);
        Color rndColor = cNames[rnd];
        cNames.Remove(rndColor);

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
        Color currentColor = quizUIController.GetColorToMixImageColor();

        if (currentColor == resultColor) {
            result = true;
        } else {
            result = false;
        }

        quizUIController.ShowMixResult(result);

    }
}