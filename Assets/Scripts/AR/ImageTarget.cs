using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTarget : MonoBehaviour
{
    //public GameObject sphere;
    [SerializeField] private ColorNames colorId;
    [SerializeField]private GameManager gameManager;
    [SerializeField] private AnimatorSynchronizer animSynchronizer;


    public ColorSphere InstanciateSphere()
    {
        //Instantiate new Sphere if no sphere exists yet
        if (transform.Find("ColorSphere(Clone)") == null)
        {
           return ColorSphere.Create(transform, colorId, gameManager, animSynchronizer);
        }
        return null;

    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

}
