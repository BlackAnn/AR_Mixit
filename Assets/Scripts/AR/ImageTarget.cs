using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTarget : MonoBehaviour
{
    //public GameObject sphere;
    public ColorNames colorId;


    public ColorSphere InstanciateSphere()
    {
        //Instantiate new Sphere if no sphere exists yet
        if (transform.Find("ColorSphere(Clone)") == null)
        {
           return ColorSphere.Create(transform, colorId);
        }
        return null;

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
