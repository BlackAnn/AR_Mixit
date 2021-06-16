using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphere : MonoBehaviour
{
    public ColorNames colorId;
    private Material material;
    private Color color;
    private float movementSpeed;
    private Animator animator;
    private bool isMixing;
    private Vector3 targetPosition;
    private GameObject parent;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMixing = false;
        targetPosition = new Vector3();
        movementSpeed = 0.9f;
        //parent = transform.parent.gameObject.transform.parent.gameObject;
        material = ColorPreset.GetMaterialById((int)colorId);
        color = material.GetColor("_Color");
        SetMaterial(material);
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

    public Color GetColorValue()
    {
        //return colorInfo.GetColor();
        return Color.green;
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

    public void SetColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().SetPropertyBlock(props);
    }

    public void SetMaterial(Material material)
    {
        GetComponent<MeshRenderer>().material = material;
    }






}
