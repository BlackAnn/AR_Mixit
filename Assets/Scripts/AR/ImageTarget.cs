using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTarget : MonoBehaviour
{
    //public GameObject sphere;
    [SerializeField] private ColorNames colorId;
    [SerializeField]private MixingController mixingController;
    [SerializeField] private AnimatorSynchronizer animSynchronizer;


    public ColorSphere InstanciateSphere()
    {
        //Instantiate new Sphere if no sphere exists yet
        if (transform.Find("ColorSphere(Clone)") == null)
        {
           return ColorSphere.Create(transform, colorId, mixingController, animSynchronizer);
        }
        return null;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //Return child sphere or null if not child exists
    public ColorSphere GetChildSphere()
    {
        Transform child = transform.Find("ColorSphere(Clone)");
        if(child == null)
        {
            return null;
        }
        else
        {
            return child.GetComponent<ColorSphere>();
        }
    }

}
