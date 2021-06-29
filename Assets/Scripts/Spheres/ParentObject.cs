using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// substitute parent object for color spheres. Used when spheres have to be independent of target sphere
/// </summary>
public class ParentObject : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    /// <summary>
    /// Activate Mixing Animation
    /// </summary>
    public void ActivateMixing()
    {
        animator.SetTrigger("MixSpheres");
    }

    /// <summary>
    /// Sets position
    /// </summary>
    /// <param name="position">new position</param>
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
