using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: divide Sphere Animation & color logic (?)
public class ColorSphere : MonoBehaviour
{
    public ColorNames colorId;
    private Color color;
    private float movementSpeed;
    private Animator animator;
    private bool isMixing;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMixing = false;
        targetPosition = new Vector3();
        movementSpeed = 0.9f;
        color = ColorPreset.GetColorById((int)colorId);
        SetColor(color);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMixing)
        {
            //Move Sphere towards center of parent object
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            if(transform.localScale.Equals(new Vector3(0, 0, 0)))
            {
                transform.gameObject.SetActive(false);
                isMixing = !isMixing;
            }
        }
    }

    public Vector3 GetPosition()
	{
        return transform.position;
	}


    public void ActivateMixing(Vector3 position)
    {
        
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

    public void SetColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().material.SetColor("_Color", color);
        //GetComponent<Renderer>().material.color = color;

    }

 



}
