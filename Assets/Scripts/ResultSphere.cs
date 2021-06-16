using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: before showing, set Color to the mixed color
public class ResultSphere : MonoBehaviour
{
    //private Color _color;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowSphere(Color color, Vector3 position)
    {
        //Debug.Log("ShowResultSphere: " + position);
        transform.gameObject.SetActive(true);
        SetColor(color);
        transform.position = position;
        animator.SetTrigger("ShowResultSphere");
    }

    public void SetColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().SetPropertyBlock(props);
    }

  
}
