using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    //UI Control
    [SerializeField] private QuizUIController quizModeUI;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private MixingController mixingController;
    [SerializeField] private InteractionControl touchInteraction;
    [SerializeField] private TextMeshProUGUI helpText;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI resultName;



    private List<ImageTarget> detectedImageTargets = new List<ImageTarget>();
    private GameState _state;
    private GameMode _mode;
    private Color _resultColor = Color.black;

    private string helpText_NoCards = "Lege 2 Farbkarten nebeneinander und scanne sie.";
    private string helpText_OneCard = "Lege noch eine Farbkarte hinzu.";
    private string helpText_TwoCards = "Reibe ueber die beiden Kugeln um sie zu mischen.";
    private string helpText_Result = "Klicke auf die Kugel um den Farbnamen anzuzeigen und auszublenden.";


    public enum GameMode
    {
        Learn, Quiz
    }

    // Start is called before the first frame update
    void Start()
    {
        //zum Testen!!
        //GameModeController.gameMode = "Quiz";
        //_state = GameState.Idle;

        GameModeController.previousWindow = GameModeController.gameMode;


        if (GameModeController.gameMode.Equals("Learn"))
        {
            _mode = GameMode.Learn;
            ActivateUserInteraction();
            quizModeUI.Hide();
        }
        else if (GameModeController.gameMode.Equals("Quiz"))
        {
            _mode = GameMode.Quiz;
            _state = GameState.Idle;
            helpPanel.SetActive(false);
            resultPanel.SetActive(false);

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
        resultPanel.SetActive(false);
        touchInteraction.Activate();

        mixingController.DestroyAllSpheresInParent();

        foreach (ImageTarget target in detectedImageTargets)
        {
            target.InstanciateSphere();
        }

        Debug.Log("Activate User Interaction");
        ChangeHelpText();
        helpPanel.SetActive(true);
    }


    public void GoToMainMenu()
    {
        Debug.Log("GotToMainMenu");
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
            ChangeHelpText();
        }
        mixingController.UpdateLists(detectedImageTargets);

    }

    public void UnsubscribeTarget(ImageTarget marker)
    {
        if (detectedImageTargets.Contains(marker))
        {
            ColorSphere childSphere = marker.GetChildSphere();
            if (childSphere != null)
            {
                Destroy(childSphere.gameObject);
            }
            detectedImageTargets.Remove(marker);
            mixingController.UpdateLists(detectedImageTargets);

        }

        if (_state == GameState.UserInteraction)
        {
            ChangeHelpText();
        }
        else if (_state == GameState.Mixing || _state == GameState.ShowingResultSphere)
        {
            Reset();
        }

    }

    private void ChangeHelpText()
    {
        Debug.Log("Change Text: " + detectedImageTargets.Count);
        Debug.Log("HelpText is active: " + helpText.gameObject.active);
        Debug.Log("HelpText : " + helpText.text);

        if (detectedImageTargets.Count == 0)
        {
            helpText.text = helpText_NoCards;
        }
        else if (detectedImageTargets.Count == 1)
        {
            helpText.text = helpText_OneCard;
        }
        else
        {
            helpText.text = helpText_TwoCards;
        }
    }

    /*private void DestroyAllActiveSpheres()
    {
        foreach(ImageTarget target in detectedImageTargets)
        {
            ColorSphere childSphere = target.GetChildSphere();
            if (childSphere != null)
            {
                Destroy(childSphere.gameObject);
            }
        }
    }*/

    //resets the gamelogic to its initial state (resultSphere disappears and new spheres will be displayed if image target is detected)
    public void Reset()
    {
        mixingController.HideResultSphere();
        if (_mode == GameMode.Learn)
        {
            ActivateUserInteraction();
        }
        else if (_mode == GameMode.Quiz)
        {
            _state = GameState.Idle;
            helpPanel.SetActive(false);
        }
    }

    public void MixingHasStarted()
    {
        _state = GameState.Mixing;
        touchInteraction.Deactivate();
        helpPanel.SetActive(false);
    }

    public void MixingHasFinished(Color resultColor)
    {
        _state = GameState.ShowingResultSphere;
        _resultColor = resultColor;
        if (_mode == GameMode.Quiz)
        {
            quizManager.EvaluateResult(resultColor);
        }
        else
        {
            helpText.text = helpText_Result;
            helpPanel.SetActive(true);
        }
    }

    public void ToggleResultName()
    {
        if (_mode == GameMode.Learn && _state == GameState.ShowingResultSphere)
        {
            if (resultPanel.active)
            {
                resultPanel.SetActive(false);
            }
            else
            {
                resultName.color = _resultColor;
                resultName.text = ColorPreset.GetDisplayNameByColor(_resultColor);
                resultPanel.SetActive(true);
            }
        }
    }




}