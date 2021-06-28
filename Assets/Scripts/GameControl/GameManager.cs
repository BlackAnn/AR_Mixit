using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    //UI Control
    [SerializeField] private UIController learnModeUI;
    [SerializeField] private UIController quizModeUI;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private MixingController mixingController;
    [SerializeField] private InteractionControl touchInteraction;

    private List<ImageTarget> detectedImageTargets = new List<ImageTarget>();
    private GameState _state;
    private GameMode _mode;


    public enum GameMode
    {
        Learn, Quiz
    }



    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.Idle;

        if (GameModeController.gameMode.Equals("Learn"))
        {
            _mode = GameMode.Learn;
            ActivateUserInteraction();
            learnModeUI.Show();
            quizModeUI.Hide();
            
        }
        else if (GameModeController.gameMode.Equals("Quiz"))
        {
            _mode = GameMode.Quiz;
            _state = GameState.Idle;
            learnModeUI.Hide();
            quizModeUI.Show();
            quizManager.SetupGame();
         
        }
        else // Delete?
        {
            ActivateUserInteraction();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActivateUserInteraction()
    {
        _state = GameState.UserInteraction;
        touchInteraction.Activate();

        mixingController.DestroyAllSpheresInParent();

        foreach (ImageTarget target in detectedImageTargets)
        {
            target.InstanciateSphere();
        }
    }

 
    public void GoToMainMenu()
    {
        GameModeController.menuMode = "Options";
        SceneManager.LoadScene(0);
    }


    public void SubscribeTarget(ImageTarget marker)
    {

        //add marker to list
        if (!detectedImageTargets.Contains(marker))
        {
            detectedImageTargets.Add(marker);
        }
        //only instanciate spheres if game is in the right state
        if (_state == GameState.UserInteraction)
        {
            marker.InstanciateSphere();
        }
        mixingController.UpdateLists(detectedImageTargets);

    }

    public void UnsubscribeTarget(ImageTarget marker)
    {
        if (detectedImageTargets.Contains(marker))
        {
            ColorSphere childSphere = marker.GetChildSphere();
            if(childSphere != null)
            {
                Destroy(childSphere.gameObject);
            }
            detectedImageTargets.Remove(marker);
            mixingController.UpdateLists(detectedImageTargets);
           
             //add different states
        }

        
        if(_state == GameState.Mixing || _state == GameState.ShowingResultSphere)
        {
            Reset();
        }


    }

    //For Learn Mode
    //TO DO: adjust for Quiz Modus
    //resets the gamelogic to its initial state (resultSphere disappears and new spheres will be displayed if image target is detected)
    public void Reset()
    {
        mixingController.HideResultSphere();
        ActivateUserInteraction();
    }

    public void MixingHasStarted()
    {
        _state = GameState.Mixing;
        touchInteraction.Deactivate();
    }

    public void MixingHasFinished(Color resultColor)
    {
        _state = GameState.ShowingResultSphere;
        if(_mode == GameMode.Quiz)
        {
            quizManager.EvaluateResult(resultColor);
        } else
        {
            //show reset button ?
        }
    }

   



}
