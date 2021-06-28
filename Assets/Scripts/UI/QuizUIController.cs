using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour {
    public Image colorToMixImg;
    private Image colorToMixImgSmall;

    // Start is called before the first frame update
    void Start() {
        colorToMixImg = GameObject.Find("ColorToMixImage").GetComponent<Image>();
        colorToMixImgSmall = GameObject.Find("ColorToMixImageSmall").GetComponent<Image>();
    }

    public void SetUpQuizUI() {

    }

    public void SetColorToMixImage(Color color) {
        colorToMixImg.color = color;
        colorToMixImgSmall.color = colorToMixImg.color;
    }
}
