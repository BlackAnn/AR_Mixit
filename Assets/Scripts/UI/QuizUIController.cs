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

    private Color wrongColor;
    private Color rightColor;

    private void Start()
    {
        rightColor = new Color(42 / 255f, 152 / 255f, 85 / 255f);
        wrongColor = new Color(199 / 255f, 100 / 255f, 102 / 255f);
    }
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
            resultTxt.color = rightColor;
            tippTxt.text = "Richtige Farbe:\n" + mixedColor;
        } else {
            resultTxt.text = "Leider Falsch...";
            resultTxt.color = wrongColor;
            tippTxt.text = "Gemischte Farbe:\n" + mixedColor + "\n\nRichtige Farbe:\n" + correctColor;
            tryAgainButton.SetActive(true);
        }

        tippButton.SetActive(true);
        resultPanel.gameObject.SetActive(true);
        continueButton.SetActive(true);
    }
}
