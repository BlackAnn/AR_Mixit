using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mixable color sphere
/// </summary>
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
        
    /// <summary>
    /// Instantiates a sphere
    /// </summary>
    /// <param name="parent">image target which will be the parent of the sphere</param>
    /// <param name="colorId">colorId indicating the color of the sphere</param>
    /// <param name="mixingController">mixing controller object responsible for managing the mixing of the sphere</param>
    /// <param name="animSync">animator synchronizer responsible for synchronizing the sphere animation</param>
    /// <returns></returns>
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


    void Start()
    {
        _state = SphereState.Idle;
        _targetPosition = new Vector3();
        _movementSpeed = 0.2f;
    }

    void Update()
    {
        //if sphere is in isMoving state, adapt its position
        if(_state == SphereState.IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _movementSpeed * Time.deltaTime);  
        }
    }

    //when sphere collides with another sphere, start mixing
    void OnCollisionEnter(Collision collision)
    {
        if(_state == SphereState.IsMoving && collision.gameObject.tag == "ColorSphere")
        {
            _mixingController.StartMixing();
        }
    }

    /// <summary>
    /// Sets mixing controller
    /// </summary>
    /// <param name="mixingController"></param>
    protected void SetMixingController(MixingController mixingController)
    {
        _mixingController = mixingController;
    }

    /// <summary>
    /// sets animation synchronizer
    /// </summary>
    /// <param name="animSynch"></param>
    protected void SetAnimationSynchronizer(AnimatorSynchronizer animSynch)
    {
        _animSynchronizer = animSynch;
    }

    /// <summary>
    /// returns sphere position
    /// </summary>
    /// <returns>position as Vector3</returns>
    public Vector3 GetPosition()
	{
        return transform.position;
	}

    /// <summary>
    /// returns sphere localScale
    /// </summary>
    /// <returns>localScale as Vector3</returns>
    public Vector3 GetSize()
    {
        return transform.localScale;
    }

    /// <summary>
    /// returns sphere's color id
    /// </summary>
    /// <returns>color id as ColorNames enum</returns>
    public ColorNames GetColorId()
    {
        return _colorId;
    }

    /// <summary>
    /// sets sphere position
    /// </summary>
    /// <param name="newPosition">new sphere position</param>
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    /// <summary>
    /// sets sphere parent
    /// </summary>
    /// <param name="parent">new parent gameobject</param>
    public void SetParent(GameObject parent)
    {
        transform.parent = parent.transform;
    }

    /// <summary>
    /// sets the target position, which the sphere will be moving towards
    /// </summary>
    /// <param name="targetPosition">new target position</param>
    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
    }

    
    /// <summary>
    /// sets color_Id and updates sphere material Color
    /// </summary>
    /// <param name="colorId">new color id</param>
    public void UpdateColor(ColorNames colorId)
    {
        _colorId = colorId;
        Color color = ColorPreset.GetColorById((int)colorId);
        SetMaterialColor(color);
    }

    /// <summary>
    /// updates material color
    /// </summary>
    /// <param name="color">color which the material will be set to</param>
    private void SetMaterialColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    /// <summary>
    /// Set state to isMixing
    /// </summary>
    public void IsMixing()
    {
        _state = SphereState.IsMixing;
    }

    /// <summary>
    /// Start moving sphere
    /// </summary>
    /// <param name="targetPosition">position that the sphere is moving towards</param>
    /// <param name="speed">speed at which the sphere is moving</param>
    public void Move(Vector3 targetPosition, float speed)
    {
        _targetPosition = targetPosition;
        _movementSpeed = speed;
        _state = SphereState.IsMoving;
    }

}
