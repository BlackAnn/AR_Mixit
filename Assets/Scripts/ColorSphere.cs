using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphere : MonoBehaviour
{

    public Color color;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetPosition()
	{
        return transform.position;
	}

    public void PositionToString()
	{
        Debug.Log(GetPosition().x);
	}

    public void ActivateMixing()
    {
        //Activate MixingAnimation (to left or right)
    }

    /*Set Material Color
     *MaterialPropertyBlock props = new MaterialPropertyBlock();
            props.SetColor("_Color", _lemmingManager.GetColor(newColor));
            _renderer.SetPropertyBlock(props);*/

    
}
