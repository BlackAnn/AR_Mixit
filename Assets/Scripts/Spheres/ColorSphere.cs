using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: divide Sphere Animation & color logic (?)
public class ColorSphere : MonoBehaviour
{
    private ColorNames _colorId;

    private bool _isMixing;
    private Vector3 _targetPosition;
    private float _movementSpeed;
    private Animator _animator;
    

    //Instantiates a sphere
    public static ColorSphere Create(Transform parent, ColorNames colorId)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/ColorSphere"), parent, false) as GameObject;
        ColorSphere colorSphere = newObject.GetComponent<ColorSphere>();
        colorSphere.UpdateColor(colorId);
        return colorSphere;
    }


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _isMixing = false;
        _targetPosition = new Vector3();
        _movementSpeed = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_isMixing)
        {
            //Move Sphere towards center of parent object
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
            if(transform.localScale.Equals(new Vector3(0, 0, 0)))
            {
                transform.gameObject.SetActive(false);
                _isMixing = !_isMixing;
                Destroy(gameObject);
            }
        }
    }

    public Vector3 GetPosition()
	{
        return transform.position;
	}

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetParent(GameObject parent)
    {
        transform.parent = parent.transform;
    }

    public ColorNames GetColorId()
    {
        return _colorId;
    }

    //sets color_Id and updates sphere Color
    public void UpdateColor(ColorNames colorId)
    {
        _colorId = colorId;
        Color color = ColorPreset.GetColorById((int)colorId);
        SetMaterialColor(color);
    }

    //updates sphere color
    private void SetMaterialColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().material.SetColor("_Color", color);
        //GetComponent<Renderer>().material.color = color;
    }

  

    public void ActivateMixing(Vector3 position, GameObject parent)
    {
        _animator.SetTrigger("MixSpheres");
        _targetPosition = position;
        SetParent(parent);

        //IsMixing activates the translation of sphere towards the center
        //ToggleIsMixing is slightly delayed, in order to coordinate better with the rest of the "MixSpheres" animation
        Invoke("ToggleIsMixing", 0.5f);
    }

    private void ToggleIsMixing()
    {
        _isMixing = !_isMixing;
        Debug.Log("Inside ToggleMixing)");
    }



}
