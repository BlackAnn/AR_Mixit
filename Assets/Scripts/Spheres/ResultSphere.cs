using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sphere showing the mixed result color
/// </summary>
public class ResultSphere : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _initialScale;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _initialScale = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// set result color and set sphere to active
    /// </summary>
    /// <param name="color">result color</param>
    public void ShowSphere(Color color)
    {
        transform.gameObject.SetActive(true);
        SetMaterialColor(color);
    }

    /// <summary>
    /// set material color of the sphere
    /// </summary>
    /// <param name="color">new color value for the material</param>
    private void SetMaterialColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().SetPropertyBlock(props);
    }

    /// <summary>
    /// Set position
    /// </summary>
    /// <param name="position">new position</param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    /// <summary>
    /// resets the sphere to its initial state
    /// </summary>
    public void Reset()
    {
        if (gameObject.active)
        {
            transform.gameObject.SetActive(false);
            transform.localScale = _initialScale;
        }
    }



}
