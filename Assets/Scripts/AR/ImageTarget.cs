using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Image Target which can instantiate a sphere as a child
/// </summary>
public class ImageTarget : MonoBehaviour
{
    [SerializeField] private ColorNames colorId;
    [SerializeField] private MixingController mixingController;
    [SerializeField] private AnimatorSynchronizer animSynchronizer;

    /// <summary>
    /// instantiate a sphere child, if no sphere child exists yet
    /// </summary>
    /// <returns>sphere object that was instantiated</returns>
    public ColorSphere InstanciateSphere()
    {
        if (transform.Find("ColorSphere(Clone)") == null)
        {
           return ColorSphere.Create(transform, colorId, mixingController, animSynchronizer);
        }
        return null;
    }

    /// <summary>
    /// return image target position
    /// </summary>
    /// <returns>position</returns>
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    /// <summary>
    ///  Return child sphere or null if not child exists
    /// </summary>
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
