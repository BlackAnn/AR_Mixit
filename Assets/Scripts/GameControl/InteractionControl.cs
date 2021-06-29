using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Checks for and manages user input.
/// </summary>
public class InteractionControl : MonoBehaviour
{
    [SerializeField] private MixingController _mixingController;
    [SerializeField] private GameManager _gameManager;

    private bool _activated = false;
    private bool _isInteracting;
    private ColorNames _lastHitSphere;
    private float _timeForInteraction = 2.0f;
    private float _timer = 0.0f;
    private int _swipeCount = 0;

    void Update()
    {
        if (_activated)
        {
            //if touch is detected, check if a sphere is hit. If yes, start interaction and move spheres together
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
                    {
                        _lastHitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
                        _isInteracting = true;
                        _timer = 0.0f;
                        _swipeCount = 0;
                    }
                }
            }
            //if touch has ended, end interaction and move spheres apart
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
            {
                _isInteracting = false;
                _mixingController.MoveSpheresApart();
            }
            //During interaction, check if the spheres are getting touched alternately within a certain time frame
            if (_isInteracting)
            {
                _timer += Time.deltaTime;

                //after two swipe counts, move spheres together
                if (_swipeCount == 2)
                {
                    _mixingController.MoveSpheresTogether();
                }
                //if interaciton takes too long, move spheres apart
                if (_timer > _timeForInteraction)
                {
                    _isInteracting = false;
                    _mixingController.MoveSpheresApart();
                }
                //if interaction is ongoing, check if a sphere is hit and reset timer accordingly
                else 
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
                        {
                            ColorNames hitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
                            if (hitSphere != _lastHitSphere)
                            {
                                _timer = 0.0f;
                                _swipeCount++;
                                _lastHitSphere = hitSphere;
                            }
                        }
                    }

                }
            }
        }
        // if result sphere is touched, toggle display of result sphere name
        else
        {
            if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject.tag == "ResultSphere")
                    {
                        _gameManager.ToggleResultName();

                    }
                }
            }
        }


  // for testing of the above code in unity editor
#if UNITY_EDITOR

        if (_activated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
                    {

                        _lastHitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();
                        _isInteracting = true;
                        _timer = 0.0f;
                        _swipeCount = 0;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isInteracting = false;
                _mixingController.MoveSpheresApart();
            }
            if (_isInteracting)
            {
                _timer += Time.deltaTime;

                if (_swipeCount == 2)
                {
                    _mixingController.MoveSpheresTogether();
                }

                if (_timer > _timeForInteraction)
                {
                    _isInteracting = false;
                    _mixingController.MoveSpheresApart();
                }
                else 
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider != null && hit.collider.GetComponent<ColorSphere>() != null)
                        {

                            ColorNames hitSphere = hit.collider.GetComponent<ColorSphere>().GetColorId();

                            if (hitSphere != _lastHitSphere)
                            {
                                _timer = 0.0f;
                                _swipeCount++;
                                _lastHitSphere = hitSphere;
                            }
                        }
                    }
                } 

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null && hit.collider.gameObject.tag == "ResultSphere")
                    {
                        _gameManager.ToggleResultName();
                    }
                }
            }
        }
#endif
    }

    /// <summary>
    /// Activate user interaction with two mixing spheres
    /// </summary>
    public void Activate()
    {
        _activated = true;
    }

    /// <summary>
    /// Deactivate user interaction with two mixing spheres.
    /// </summary>
    public void Deactivate()
    {
        _activated = false;
    }



}

