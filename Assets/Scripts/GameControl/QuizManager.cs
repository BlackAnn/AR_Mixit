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
    private ColorNames[] randomColors;

    List<ColorNames> cNames;
    List<ColorPreset> cList;
    private int quizStep;

    private GameObject quizModeUI;


    // Start is called before the first frame update
    void Start() {
    }

        //GameObject.Find("ColorToMixImageSmall").GetComponent<Image>().color = GameObject.Find("ColorToMixImage").GetComponent<Image>().color;
        /* if (GameModeController.gameMode.Equals("Quiz")) {
          if (MIXEVENT?) {
          CheckColor();
           resultTxt.setActive();
           GameObject.Find("QuizNextButton").setActive();
           GameObject.Find("ColorToMixImageSmall").setActive(false);
            if (continueGame) {
            GameObject.Find("QuizModePromptText").setActive();
            GameObject.Find("ColorToMixImage").setActive();
          }
          }
         */

    /*public bool CheckColor() {
       GameObject resultTxt = GameObject.Find("QuizModeResultText");
         if (QuizUIController.colorToMixImg.GetComponent<Image>().color == current mixed color )
             resultTxt.GetComponent<TextMeshProUGUI>().text = "Richtig!";
             return true;
         else {
             resultTxt.GetComponent<TextMeshProUGUI>().text = "Falsch";
             return false;
         }
     }*/

    //setup for quiz-mode
    public void SetupGame() {
        quizStep = 0;
        CreateColorArray();
        quizUIController.SetColorToMixImage(cList[(int)randomColors[0]].GetColor());
    }

    //Methode, die die Nutzer-Interaktion fuer das Spiel beginnen laesst
    public void StartGame() {
        gameManager.ActivateUserInteraction();
    }

    public void PlayGame() {
        //GameObject.Find("QuizModePromptText").gameObject.SetActive(true);
    }

    public bool ContinueGame() {
        if (quizStep <= 19) {
            quizStep++;
            quizUIController.SetColorToMixImage(cList[(int)randomColors[quizStep]].GetColor());
            return true;
        } else {
            return false;
        }
    }

    public void ShowColorInfo() {

    }

    public void CreateColorArray() {
        cNames = new List<ColorNames>();
        cList = new List<ColorPreset>();
        cList = ColorPreset.GetValues();

        for (int j = 0; j < cList.Count; j++) {
            if (cList[(int)j].GetMixable()) {
                cNames.Add(cList[j].GetID());
            }
        }
    }

    //DUMMY_METHODE: Methode, die aufgerufen wird, wenn fertig gemischt wurde
    public void EvaluateResult(Color resultColor) {
        Debug.Log("EVALUATE RESULT, color = " + resultColor);
    }
}