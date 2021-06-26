using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour {
    public GameObject colorToMixImg;

    public GameObject learnModeUI;
    public GameObject quizModeUI;

    public ColorNames[] randomColors;

    List<ColorPreset> cList;
    public int quizStep;

    public void GoToMainMenu() {
        GameModeController.menuMode = "Options";
        SceneManager.LoadScene(0);
    }

    // Start is called before the first frame update
    void Start() {
        if (GameModeController.gameMode.Equals("Learn")) {
            learnModeUI.SetActive(true);
            quizModeUI.SetActive(false);
            GameObject helpTxT = GameObject.Find("LearnModeHelpText");
            helpTxT.SetActive(true);

            StartCoroutine(RemoveAfterSeconds(5, helpTxT));

        } else if (GameModeController.gameMode.Equals("Quiz")) {
            quizStep = 0;
            learnModeUI.SetActive(false);
            quizModeUI.SetActive(true);
            GameObject helpTxT = GameObject.Find("QuizModeHelpText");

            CreateColorArray();
            colorToMixImg = GameObject.Find("ColorToMixImage");
            colorToMixImg.GetComponent<Image>().color = cList[(int)randomColors[0]].GetColor();

            StartCoroutine(RemoveAfterSeconds(5, helpTxT));
        }
    }

    void Update() {
        GameObject.Find("ColorToMixImageSmall").GetComponent<Image>().color = GameObject.Find("ColorToMixImage").GetComponent<Image>().color;
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

    public void playGame() {
        //GameObject.Find("QuizModePromptText").gameObject.SetActive(true);
    }

    public bool continueGame() {
        if (quizStep <= 19) {
            quizStep++;
            colorToMixImg.GetComponent<Image>().color = cList[(int)randomColors[quizStep]].GetColor();
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

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj) {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
