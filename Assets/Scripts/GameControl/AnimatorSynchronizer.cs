using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes sure that the animators of the different gameobjects are synchronized.
/// </summary>
public class AnimatorSynchronizer : MonoBehaviour
{
    private bool _activateMixing = false;
    private List<Animator> _sphereAnimators = new List<Animator>();
    [SerializeField] private Animator _resultAnimator;

    void Update()
    {
        //once mixing is activated, all the animators are activated simultaneously
        if (_activateMixing)
        {
            foreach (Animator anim in _sphereAnimators)
            {
                anim.Play("Sphere_Mix", 0, 0);
            }
            _resultAnimator.Play("ResultSphere_Appear", 0, 0.5f);
            _activateMixing = false;
        }
    }

    /// <summary>
    /// Add a new animator to the _sphereAnimators list.
    /// </summary>
    /// <param name="animator">Animator that will be added to the list</param>
    public void SubscribeSphereAnimator(Animator animator)
    {
        if (!_sphereAnimators.Contains(animator))
        {
            _sphereAnimators.Add(animator);
        }

    }

    /// <summary>
    /// Remove an animator from the _sphereAnimators list.
    /// </summary>
    /// <param name="animator">Animator that will be removed from the list</param>
    public void UnsubscribeSphereAnimator(Animator animator)
    {
        if (_sphereAnimators.Contains(animator))
        {
            _sphereAnimators.Remove(animator);
        }
    }

    /// <summary>
    /// Activate the mixing animations
    /// </summary>
    public void ActivateMixingAnimation()
    {
        _activateMixing = true;
    }
}
