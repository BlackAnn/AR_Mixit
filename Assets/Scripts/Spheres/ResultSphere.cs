﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSphere : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _initialScale;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _initialScale = new Vector3(0, 0, 0);
    }

    public void ShowSphere(Color color, Vector3 position)
    {
        transform.gameObject.SetActive(true);
        SetMaterialColor(color);
        transform.position = position;
        _animator.SetTrigger("ShowResultSphere");
    }

    private void SetMaterialColor(Color color)
    {
        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetColor("_Color", color);
        GetComponent<Renderer>().SetPropertyBlock(props);
    }

    //resets the sphere to its initial state
    public void Reset()
    {
        transform.gameObject.SetActive(false);
        transform.localScale = _initialScale;

    }



}
