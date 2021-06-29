using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the sphere interaction (moving together & apart), the sphere mixing and the display of the result sphere
/// </summary>
public class MixingController : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ResultSphere _resultSphere;
    [SerializeField] private ParentObject _parentObject;
    [SerializeField] private AnimatorSynchronizer _animSynchronizer;

    private MixingState _state;
    private List<ImageTarget> _imageTargets = new List<ImageTarget>();
    private List<ColorSphere> _colorSpheres = new List<ColorSphere>();
    private List<Vector3> _previousImageTargetPos = new List<Vector3>();
    private Color _resultColor = Color.black;
    private float _sphereMovementSpeed = 0.5f;

    /// <summary>
    /// States that will be used to manage the sphere mixing.
    /// </summary>
    public enum MixingState
    {
        Idle,
        MovingTogether,
        MovingApart,
        Mixing,
        ShowingResultSphere
    }

    void Start()
    {
        _state = MixingState.Idle;
    }

    //Updates the spheres' position and movement according to its state
    void Update()
    {
        if (_state != MixingState.Idle)
        {
            if (_imageTargets.Count == 1)
            {
                _state = MixingState.MovingApart;
            }
            else if (_imageTargets.Count == 0)
            {
                _state = MixingState.Idle;
            }

            //if (TargetPositionHasChanged())

            if (_state == MixingState.MovingApart)
            {
                for (int i = 0; i < _colorSpheres.Count; i++)
                {
                    Vector3 targetPosition = _imageTargets[i].GetPosition();
                    _colorSpheres[i].SetTargetPosition(targetPosition);
                }
            }
            else if (_state == MixingState.ShowingResultSphere)
            {
                Vector3 resultPosition = CalculateMidpointPosition(_imageTargets[0].GetPosition(), _imageTargets[1].GetPosition());
                _resultSphere.SetPosition(resultPosition);
            }
            else if (_state == MixingState.MovingTogether || _state == MixingState.Mixing)
            {
                if (_imageTargets.Count == 2 && _colorSpheres.Count == 2)
                {
                    Vector3 resultPosition = CalculateMidpointPosition(_imageTargets[0].GetPosition(), _imageTargets[1].GetPosition());
                    foreach (ColorSphere sphere in _colorSpheres)
                    {
                        sphere.SetTargetPosition(resultPosition);
                    }
                    _parentObject.SetPosition(resultPosition);


                    if (_state == MixingState.Mixing)
                    {
                        _resultSphere.SetPosition(resultPosition);
                        if (_colorSpheres[0].GetSize().Equals(new Vector3(0, 0, 0)))
                        {
                            _state = MixingState.ShowingResultSphere;
                            Debug.Log("state = " + _state);
                            MixingHasFinished();
                        }
                    }
                    _previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();
                }
            }
        }
    }

    /// <summary>
    /// Updates the image target list and the sphere list, based on a given ImageTarget list. Only image targets which have a sphere child will be saved.
    /// </summary>
    /// <param name="imageTargets">list on which the new update is based</param>
    public void UpdateLists(List<ImageTarget> imageTargets)
    {
        _imageTargets = new List<ImageTarget>();
        _colorSpheres = new List<ColorSphere>();
        foreach (ImageTarget target in imageTargets)
        {
            ColorSphere child = target.GetChildSphere();
            if (child != null)
            {
                _imageTargets.Add(target);
                _colorSpheres.Add(child);
            }
        }
    }

    /// <summary>
    /// Activate sphere movement and make them move towards the midpoint between their target images.
    /// </summary>
    public void MoveSpheresTogether()
    {
        if (_state == MixingState.Idle || _state == MixingState.MovingApart)
        {
            if (_imageTargets.Count == 2 && _colorSpheres.Count == 2)
            {
                _state = MixingState.MovingTogether;
                _previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();
                Vector3 resultPosition = CalculateMidpointPosition(_imageTargets[0].GetPosition(), _imageTargets[1].GetPosition());

                foreach (ColorSphere sphere in _colorSpheres)
                {
                    sphere.Move(resultPosition, _sphereMovementSpeed);
                }
            }
        }
    }

    /// <summary>
    /// Makes spheres move apart towards their original position (on top of the image targets)
    /// </summary>
    public void MoveSpheresApart()
    {
        if (_state == MixingState.MovingTogether)
        {
            _state = MixingState.MovingApart;
            _previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();
        }
    }

    /// <summary>
    /// Generate Result color of the two current spheres and activate mixing animation.
    /// </summary>
    public void StartMixing()
    {
        //Mix only if there are two targets detected
        if (_state == MixingState.MovingTogether && _imageTargets.Count == 2)
        {
            _gameManager.MixingHasStarted();
            _state = MixingState.Mixing;
            _resultColor = GenerateMixedColor(_colorSpheres[0], _colorSpheres[1]);

            foreach (ColorSphere sphere in _colorSpheres)
            {
                sphere.SetParent(_parentObject.gameObject);
                sphere.IsMixing();
            }
            _animSynchronizer.ActivateMixingAnimation();
            _resultSphere.ShowSphere(_resultColor);
        }
    }

    /// <summary>
    /// notify game manager when the mixing has finished
    /// </summary>
    public void MixingHasFinished()
    {
        _gameManager.MixingHasFinished(_resultColor);
    }

    /// <summary>
    /// Hide the result sphere and set the mixing state to idle.
    /// </summary>
    public void HideResultSphere()
    {
        _resultSphere.Reset();
        _state = MixingState.Idle;
    }

    /// <summary>
    /// Destroy all the child spheres in the parent object
    /// </summary>
    public void DestroyAllSpheresInParent()
    {
        foreach (Transform child in _parentObject.transform)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// Calculate the midpoint position between two vectors.
    /// </summary>
    /// <param name="position1">vector 1</param>
    /// <param name="position2"> vector 2</param>
    /// <returns></returns>
    private Vector3 CalculateMidpointPosition(Vector3 position1, Vector3 position2)
    {
        return (position1 + position2) / 2;
    }

    /// <summary>
    /// Check if the target position has changed
    /// </summary>
    /// <returns>true, if the target position has changed</returns>
    private bool TargetPositionHasChanged()
    {
        if (_previousImageTargetPos.Count != _imageTargets.Count)
        {
            return true;
        }
        for (int i = 0; i < _previousImageTargetPos.Count; i++)
        {
            if (!_previousImageTargetPos[i].Equals(_imageTargets[i].GetPosition()))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Generate the mixed color of two spheres
    /// </summary>
    /// <param name="sphere1"></param>
    /// <param name="sphere2"></param>
    /// <returns></returns>
    private Color GenerateMixedColor(ColorSphere sphere1, ColorSphere sphere2)
    {
        int colorIndex = (int)MixedColors.Instance.GetMixedColor(sphere1.GetColorId(), sphere2.GetColorId());
        return ColorPreset.GetColorById(colorIndex);
    }

}
