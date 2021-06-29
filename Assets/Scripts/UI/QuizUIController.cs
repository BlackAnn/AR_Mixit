using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour {
    [SerializeField] private Image colorToMixImg;
    [SerializeField] private Image colorToMixImgSmall;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject tippPanel;
    [SerializeField] private TextMeshProUGUI resultTxt;
    [SerializeField] private TextMeshProUGUI tippTxt;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject tryAgainButton;
    [SerializeField] private GameObject tippButton;

    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void SetColorToMixImage(Color color) {
        colorToMixImg.color = color;
        colorToMixImgSmall.color = colorToMixImg.color;
    }

    public Color GetColorToMixImageColor() {

        return colorToMixImg.color;
    }

    public void ShowMixResult(bool result, string correctColor,string mixedColor) {
        if (result) {
            resultTxt.text = "Richtig!";
            resultTxt.color = Color.green;
            tippTxt.text = "Richtige Farbe:\n" + mixedColor;
        } else {
            resultTxt.text = "Falsch!";
            resultTxt.color = Color.red;
            tippTxt.text = "Gemischte Farbe:\n" + mixedColor + "\n\nRichtige Farbe:\n" + correctColor;
            tryAgainButton.SetActive(true);
        }

        tippButton.SetActive(true);
        resultPanel.gameObject.SetActive(true);
        continueButton.SetActive(true);
    }
}
