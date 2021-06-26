using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//@Anna in der QuizUIController kannst du die ganzen Methoden schreiben, die die UI verändern und diese dann dann hier (mit quizUI) aufrufen
public class QuizManager : MonoBehaviour {

    [SerializeField]private QuizUIController quizUI;
    [SerializeField] private GameManager gameManager;
    public ColorNames[] randomColors;

    List<ColorPreset> cList;
    public int quizStep;

 
    // Start is called before the first frame update
    void Start() {
            quizStep = 0;
            CreateColorArray();
            
    }

    void Update() {
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
    }

    /* public bool CheckColor() {
       GameObject resultTxt = GameObject.Find("QuizModeResultText");
         if (ColorToMixImg.GetComponent<Image>().color == /*current mixed color )
             resultTxt.GetComponent<TextMeshProUGUI>().text = "Richtig!";
             return true;
         else {
             resultTxt.GetComponent<TextMeshProUGUI>().text = "Falsch";
             return false;
         }
     }*/

    //setup for quiz-mode
    public void Setup()
    {
        quizUI.SetColorToMixImage(cList[(int)randomColors[0]].GetColor());

    }

    //Methode, die die Nutzer-Interaktion fuer das Spiel beginnen laesst
    public void StartGame()
    {
        gameManager.ActivateUserInteraction();
    }

    public void playGame() {
        //GameObject.Find("QuizModePromptText").gameObject.SetActive(true);
    }

    public bool continueGame() {
        if (quizStep <= 19) {
            quizStep++;
            quizUI.SetColorToMixImage(cList[(int)randomColors[quizStep]].GetColor());
        }
        return false;
    }

    public void ShowColorInfo() {

    }

    public void CreateColorArray() {
        int i = 0;
        randomColors = new ColorNames[20];
        cList = new List<ColorPreset>();
        cList = ColorPreset.GetValues();
        Debug.Log("lenght:" + randomColors.Length);

        while (i < 20) {
            ColorNames colorName = (ColorNames)Random.Range(0, 13);
            if (cList[(int)colorName].GetMixable()) {
                randomColors[i] = colorName;
                i++;
            }
        }
        Debug.Log(randomColors);
    }

    //DUMMY_METHODE: Methode, die aufgerufen wird, wenn fertig gemischt wurde
    public void EvaluateResult(Color resultColor)
    {
        Debug.Log("EVALUATE RESULT, color = " + resultColor);
    }

    
}
