using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour {
    [SerializeField]private Image colorToMixImg;
    [SerializeField] private Image colorToMixImgSmall;
    [SerializeField] private TextMeshProUGUI resultTxt;
    [SerializeField] private GameObject continueButton;

    // Start is called before the first frame update
    void Start() {

        //colorToMixImg = GameObject.Find("ColorToMixImage").GetComponent<Image>();
        //colorToMixImgSmall = GameObject.Find("QuizModePromptTextSmall/ColorToMixImageSmall").GetComponent<Image>();
        //resultTxt = GameObject.Find("QuizModeResultText").GetComponent<TextMeshProUGUI>();
        //continueButton = GameObject.Find("QuizNextButton");
        //GameObject.Find("QuizModePromptTextSmall").gameObject.SetActive(false);
        //resultTxt.gameObject.SetActive(false);
        //continueButton.gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void ShowMixResult(bool result) {
        
        if (result)
            resultTxt.text = "Richtig!";
        else {
            resultTxt.text = "Falsch";
        }
        resultTxt.gameObject.SetActive(true);

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
