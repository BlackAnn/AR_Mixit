using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentObject : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateMixing()
    {
        //Activate MixingAnimation (to left or right)
        animator.SetTrigger("MixSpheres");
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
