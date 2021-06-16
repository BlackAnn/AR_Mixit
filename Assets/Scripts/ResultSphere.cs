using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: before showing, set Color to the mixed color
public class ResultSphere : MonoBehaviour
{
    private Color _color;
    private Animator animator;
  


    // Start is called before the first frame update
    void Start()
    {
        //set default color
        _color = Color.black;
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetColor(Color color)
    {
        _color = color;
    }



    public void ShowSphere(Vector3 position)
    {
        //Debug.Log("ShowResultSphere: " + position);
        transform.gameObject.SetActive(true);
        transform.position = position;
        animator.SetTrigger("ShowResultSphere");
    }


}
