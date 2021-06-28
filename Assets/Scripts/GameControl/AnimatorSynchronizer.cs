using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSynchronizer : MonoBehaviour
{

    private bool _activateMixing = false;
    private List <Animator> _sphereAnimators = new List<Animator>();
    //[SerializeField]private Animator _parentAnimator;
    [SerializeField] private Animator _resultAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_activateMixing)
        {
            Debug.Log("ANIMATE MIXING-----------");
           // _parentAnimator.Play("Spheres_Rotating", 0, 0);

            foreach (Animator anim in _sphereAnimators)
            {
                anim.Play("Sphere_Mix", 0, 0);
            }

            _resultAnimator.Play("ResultSphere_Appear", 0, 0.5f);
            
            _activateMixing = false;
        }
    }

    public void SubscribeSphereAnimator(Animator animator)
    {
        if (!_sphereAnimators.Contains(animator))
        {
            _sphereAnimators.Add(animator);
        }
       
    }

    public void UnsubscribeSphereAnimator(Animator animator)
    {
        if (_sphereAnimators.Contains(animator))
        {
            _sphereAnimators.Remove(animator);
        }
    }

    public void ActivateMixingAnimation()
    {
        _activateMixing = true;
    }
}
