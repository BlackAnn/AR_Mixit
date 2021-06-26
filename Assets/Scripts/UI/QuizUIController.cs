using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUIController : MonoBehaviour
{
    private Image colorToMixImg;
    private Image colorToMixImgSmall;

    // Start is called before the first frame update
    void Start()
    {
        colorToMixImg = transform.Find("ColorToMixImage").GetComponent<Image>();
        //Debug.Log("Color To Mix Image = " + colorToMixImg);
        colorToMixImgSmall = transform.Find("ColorToMixImageSmall").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColorToMixImage(Color color)
    {
        Debug.Log("Color To Mix Image = " + transform.Find("ColorToMixImage"));
        transform.Find("ColorToMixImage").GetComponent<Image>().color = color;
        //colorToMixImg.color = color;
    }

    public void SetColorToMixImageSmall(Color color)
    {
        colorToMixImgSmall.color = color;
    }
}
