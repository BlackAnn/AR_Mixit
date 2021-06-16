using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphere : MonoBehaviour
{

    public Color color;
    private float movementSpeed;
    private Animator animator;
    private bool isMixing;
    Vector3 targetPosition;
    private GameObject parent;
    private bool isCoroutineExecuting;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMixing = false;
        isCoroutineExecuting = false;
        targetPosition = new Vector3(0.0f, 0.3f, 0.0f);
        movementSpeed = 0.9f;
        parent = transform.parent.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMixing)
        {
            //Move Sphere towards center of parent object
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
        }
    }

    public Vector3 GetPosition()
	{
        return transform.position;
	}


    public void ActivateMixing(Vector3 position)
    {
        //Debug.Log("Local Position = " + transform.localPosition);
        //Debug.Log("Absolute Position = " + transform.position);
        //Debug.Log("Target Position = " + position);


        animator.SetTrigger("MixSpheres");
        targetPosition = position;

        //IsMixing activates the translation of sphere towards the center
        //ToggleIsMixing is slightly delayed, in order to coordinate better with the rest of the "MixSpheres" animation
        Invoke("ToggleIsMixing", 0.5f);
    }

    public void ToggleIsMixing()
    {
        isMixing = !isMixing;
        Debug.Log("Inside ToggleMixing)");
    }

   
    /*Set Material Color
     *MaterialPropertyBlock props = new MaterialPropertyBlock();
            props.SetColor("_Color", _lemmingManager.GetColor(newColor));
            _renderer.SetPropertyBlock(props);*/






}
