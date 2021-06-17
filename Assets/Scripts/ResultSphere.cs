using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSphere : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ShowSphere(Color color, Vector3 position)
    {
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
