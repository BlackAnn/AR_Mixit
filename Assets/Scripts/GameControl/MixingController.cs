using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MixingController : MonoBehaviour
{
    [SerializeField] private  GameManager gameManager;
    [SerializeField] private ResultSphere resultSphere;
    [SerializeField] private ParentObject parentObject;
    [SerializeField] private AnimatorSynchronizer animSynchronizer;

    private MixingState state;
    private List<ImageTarget> _imageTargets = new List<ImageTarget>();
    private List<ColorSphere> _colorSpheres = new List<ColorSphere>();
    private List<Vector3> previousImageTargetPos = new List<Vector3>();
    private Color _resultColor = Color.black;
    private float _sphereMovementSpeed = 0.5f;

    public enum MixingState
    {
        Idle,
        MovingTogether,
        MovingApart,
        Mixing,
  
    }

    // Start is called before the first frame update
    void Start()
    {
        state = MixingState.Idle;
    }

    // Update is called once per frame
    // TO DO: check if position is default position => state = Idle
    // !! adjust sphere position (to initial position)
    void Update()
    {
        //if image targets have moved
        if (state != MixingState.Idle)
        {
            if (_imageTargets.Count == 1)
            {
                state = MixingState.MovingApart;
            } else if(_imageTargets.Count == 0)
            {
                state = MixingState.Idle;
            }

            //if (TargetPositionHasChanged())

                if(state == MixingState.MovingApart)
                {
                    for (int i = 0; i < _colorSpheres.Count; i++)
                    {
                        Vector3 targetPosition = _imageTargets[i].GetPosition();
                        //targetPosition.y += 0.03f;
                        _colorSpheres[i].SetTargetPosition(targetPosition);
                    }
                }
                else if(_imageTargets.Count == 2 && _colorSpheres.Count == 2)
                {
                    Vector3 resultPosition = CalculateMidpointPosition(_imageTargets[0].GetPosition(), _imageTargets[1].GetPosition());
                    foreach (ColorSphere sphere in _colorSpheres)
                    {
                        sphere.SetTargetPosition(resultPosition);
                    }
                    parentObject.SetPosition(resultPosition);


                    if (state == MixingState.Mixing)
                    {
                        resultSphere.SetPosition(resultPosition);
                        if(_colorSpheres[0].GetSize().Equals(new Vector3(0, 0, 0)))
                        {
                        MixingHasFinished();
                        }
                    }
                    previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();

                }  
        }
    }

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

    public void MoveSpheresTogether()
    {
        if (state == MixingState.Idle || state == MixingState.MovingApart)
        {
            if (_imageTargets.Count == 2 && _colorSpheres.Count == 2)
            {
                state = MixingState.MovingTogether;
                previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();
                Vector3 resultPosition = CalculateMidpointPosition(_imageTargets[0].GetPosition(), _imageTargets[1].GetPosition());

                foreach (ColorSphere sphere in _colorSpheres)
                {
                    //sphere.SetParent(parentObject.gameObject);
                    sphere.Move(resultPosition, _sphereMovementSpeed);
                }
            }
        }
    }

    public void MoveSpheresApart()
    {
        if (state == MixingState.MovingTogether)
        {
            state = MixingState.MovingApart;
            previousImageTargetPos = _imageTargets.Select(x => x.GetPosition()).ToList();
        }
    }

    public void StartMixing()
    {
        //Mix only if there are two targets detected
        if (state == MixingState.MovingTogether && _imageTargets.Count == 2)
        {
            gameManager.MixingHasStarted();
            state = MixingState.Mixing;
            _resultColor = GenerateMixedColor(_colorSpheres[0], _colorSpheres[1]);
           
            foreach (ColorSphere sphere in _colorSpheres)
            {
                sphere.SetParent(parentObject.gameObject);
                sphere.IsMixing();
            }
            animSynchronizer.ActivateMixingAnimation();
            resultSphere.ShowSphere(_resultColor);
        }
    }

    public void MixingHasFinished()
    {
        gameManager.MixingHasFinished(_resultColor);
    }

    public void HideResultSphere()
    {
        resultSphere.Reset();
    }

    public void DestroyAllSpheresInParent()
    {
        foreach (Transform child in parentObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
     

    private Vector3 CalculateMidpointPosition(Vector3 position1, Vector3 position2)
    {
        return (position1 + position2) / 2;
    }

    private bool TargetPositionHasChanged()
    {
        if (previousImageTargetPos.Count != _imageTargets.Count)
        {
            return true;
        }

        for(int i = 0; i<previousImageTargetPos.Count; i++)
        {
            if (!previousImageTargetPos[i].Equals(_imageTargets[i].GetPosition()))
            {
                return true;
            }
        }
        return false;
    }

    private Color GenerateMixedColor(ColorSphere sphere1, ColorSphere sphere2)
    {
        int colorIndex = (int)MixedColors.Instance.GetMixedColor(sphere1.GetColorId(), sphere2.GetColorId());
        return ColorPreset.GetColorById(colorIndex);
    }

}
