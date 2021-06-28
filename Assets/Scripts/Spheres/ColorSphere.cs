using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TO DO: divide Sphere mixng & other Logic (?)
//TO DO: Bewegung dynamischer machen (anfangs langsam, dann schneller
//TO DO: anstatt state, bloss bool isMixing verwenden

public class ColorSphere : MonoBehaviour
{
    public enum SphereState
    {
        Idle,
        IsMoving,
        IsMixing
    }

    private MixingController _mixingController;
    private ColorNames _colorId;
    private Vector3 _targetPosition;
    private float _movementSpeed;
    private AnimatorSynchronizer _animSynchronizer;
    private SphereState _state;
    
    
    //Instantiates a sphere
    public static ColorSphere Create(Transform parent, ColorNames colorId, MixingController mixingController, AnimatorSynchronizer animSync)
    {
        GameObject newObject = Instantiate(Resources.Load("Prefabs/ColorSphere"), parent, false) as GameObject;
        ColorSphere colorSphere = newObject.GetComponent<ColorSphere>();
        colorSphere.UpdateColor(colorId);
        colorSphere.SetMixingController(mixingController);
        colorSphere.SetAnimationSynchronizer(animSync);
        animSync.SubscribeSphereAnimator(colorSphere.GetComponent<Animator>());
        return colorSphere;
    }

    void OnDestroy()
    {
        _animSynchronizer.UnsubscribeSphereAnimator(gameObject.GetComponent<Animator>());
    }


    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        _state = SphereState.Idle;
        _targetPosition = new Vector3();
        _movementSpeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(_state == SphereState.IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);
           
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(_state == SphereState.IsMoving && collision.gameObject.tag == "ColorSphere")
        {
            _mixingController.StartMixing();
        }
    }

    protected void SetMixingController(MixingController mixingController)
    {
        _mixingController = mixingController;
    }

    protected void SetAnimationSynchronizer(AnimatorSynchronizer animSynch)
    {
        _animSynchronizer = animSynch;
    }

    public Vector3 GetPosition()
	{
        return transform.position;
	}

    public Vector3 GetSize()
    {
        return transform.localScale;
    }



    public ColorNames GetColorId()
    {
        return _colorId;
    }

    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void SetParent(GameObject parent)
    {
        transform.parent = parent.transform;
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
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


    public void IsMixing()
    {
        _state = SphereState.IsMixing;
    }


    public void Move(Vector3 targetPosition, float speed)
    {
        _targetPosition = targetPosition;
        _movementSpeed = speed;
        _state = SphereState.IsMoving;
    }

}
