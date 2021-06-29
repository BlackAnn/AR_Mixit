using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour {
    private Image colorToMixImg;
    private Image colorToMixImgSmall;
    private GameObject resultTxt;
    private GameObject continueButton;

    // Start is called before the first frame update
    void Start() {
        colorToMixImg = GameObject.Find("ColorToMixImage").GetComponent<Image>();
        colorToMixImgSmall = GameObject.Find("ColorToMixImageSmall").GetComponent<Image>();
        resultTxt = GameObject.Find("QuizModeResultText");
        continueButton = GameObject.Find("QuizNextButton");
    }

    public void ShowMixResult(bool result) {
        if (result)
            resultTxt.GetComponent<TextMeshProUGUI>().text = "Richtig!";
        else {
            resultTxt.GetComponent<TextMeshProUGUI>().text = "Falsch";
        }
        resultTxt.SetActive(true);
        continueButton.SetActive(true);
    }

    public void SetColorToMixImage(Color color) {
        colorToMixImg.color = color;
        colorToMixImgSmall.color = colorToMixImg.color;
    }

    public Color GetColorToMixImageColor()
    {
        return colorToMixImg.color;
    }
}
