using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the game logic and user interaction.
/// </summary>
public class GameManager : MonoBehaviour
{  
    [SerializeField] private QuizUIController _quizModeUI;
    [SerializeField] private QuizManager _quizManager;
    [SerializeField] private MixingController _mixingController;
    [SerializeField] private InteractionControl _touchInteraction;
    [SerializeField] private TextMeshProUGUI _helpText;
    [SerializeField] private GameObject _helpPanel;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TextMeshProUGUI _resultName;

    private List<ImageTarget> _detectedImageTargets = new List<ImageTarget>();
    private GameState _state;
    private GameMode _mode;
    private Color _resultColor = Color.black;

    private string _helpText_NoCards = "Lege 2 Farbkarten nebeneinander und scanne sie.";
    private string _helpText_OneCard = "Lege noch eine Farbkarte hinzu.";
    private string _helpText_TwoCards = "Reibe ueber die beiden Kugeln um sie zu mischen.";
    private string _helpText_Result = "Klicke auf die Kugel um den Farbnamen anzuzeigen und auszublenden.";

    /// <summary>
    /// Possible game modes of the app.
    /// </summary>
    public enum GameMode
    {
        Learn, Quiz
    }

    /// <summary>
    /// States that are needed for the game manager to manage the game logic and user interaction.
    /// </summary>
    public enum GameState
    {
        Idle,  //showing no spheres
        UserInteraction,  //showing mixing spheres
        Mixing, //mixing animation
        ShowingResultSphere,  //showing result sphere
    }

    // Start is called before the first frame update
    void Start()
    {
        GameModeController.previousWindow = GameModeController.gameMode;

        //make adjustments depending on game mode
        if (GameModeController.gameMode.Equals("Learn"))
        {
            _mode = GameMode.Learn;
            ActivateUserInteraction();
            _quizModeUI.Hide();
        }
        else if (GameModeController.gameMode.Equals("Quiz"))
        {
            _mode = GameMode.Quiz;
            _state = GameState.Idle;
            _helpPanel.SetActive(false);
            _resultPanel.SetActive(false);
            _quizManager.SetupGame();
            
            

        }
      
    }

    /// <summary>
    /// Switches State to User Interaction. At this state the user is able to interact with the two mixing spheres and start the mixing.
    /// </summary>
    public void ActivateUserInteraction()
    {
        _state = GameState.UserInteraction;
        _resultPanel.SetActive(false);
        _touchInteraction.Activate();
        _mixingController.DestroyAllSpheresInParent();

        //Instantiate Spheres of target that have previously been detected
        foreach (ImageTarget target in _detectedImageTargets)
        {
            target.InstanciateSphere();
        }

        ChangeHelpText();
        _helpPanel.SetActive(true);
    }

    /// <summary>
    /// Switch to the option menu.
    /// </summary>
    public void GoToOptionMenu()
    {
        GameModeController.menuMode = "Options";
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Subscribe a new image target to the detectedImageTargets list. A child sphere will be instantiated if the game is in User Interaction state.The user info text will be adjusted accordingly.
    /// </summary>
    /// <param name="marker">new image target that will be added to the list</param>
    public void SubscribeTarget(ImageTarget marker)
    {
        if (!_detectedImageTargets.Contains(marker))
        {
            _detectedImageTargets.Add(marker);
        }
        if (_state == GameState.UserInteraction)
        {
            marker.InstanciateSphere();
            ChangeHelpText();
        }
        _mixingController.UpdateLists(_detectedImageTargets);
    }

    /// <summary>
    /// Removes an image target from the detectedImageTargets list. If a child sphere is present, the child will also be destroyed. The user info text will be adjusted accordingly.
    /// </summary>
    /// <param name="marker">the image target that will be removed from the list</param>
    public void UnsubscribeTarget(ImageTarget marker)
    {
        if (_detectedImageTargets.Contains(marker))
        {
            ColorSphere childSphere = marker.GetChildSphere();
            if (childSphere != null)
            {
                Destroy(childSphere.gameObject);
            }
            _detectedImageTargets.Remove(marker);
            _mixingController.UpdateLists(_detectedImageTargets);

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

    /// <summary>
    /// Changes the user help text according to how many image targets are currently detected.
    /// </summary>
    private void ChangeHelpText()
    {
        if (_detectedImageTargets.Count == 0)
        {
            _helpText.text = _helpText_NoCards;
        }
        else if (_detectedImageTargets.Count == 1)
        {
            _helpText.text = _helpText_OneCard;
        }
        else
        {
            _helpText.text = _helpText_TwoCards;
        }
    }

   

    /// <summary>
    /// Resets the game logic to its initial state depending on the game mode.
    /// </summary>
    public void Reset()
    {
        _mixingController.HideResultSphere();
        if (_mode == GameMode.Learn)
        {
            ActivateUserInteraction();
        }
        else if (_mode == GameMode.Quiz)
        {
            _state = GameState.Idle;
            _helpPanel.SetActive(false);
        }
    }

    /// <summary>
    /// Switches the game state to Mixing and deactivates the user interaction.
    /// </summary>
    public void MixingHasStarted()
    {
        _state = GameState.Mixing;
        _touchInteraction.Deactivate();
        _helpPanel.SetActive(false);
    }

    /// <summary>
    /// Switches the game state to ShowingResultSphere and adjust the user interaction accordingly, depending on the game mode.
    /// </summary>
    /// <param name="resultColor">The color that has resulted from the mixing that has just finished</param>
    public void MixingHasFinished(Color resultColor)
    {
        _state = GameState.ShowingResultSphere;
        _resultColor = resultColor;
        if (_mode == GameMode.Quiz)
        {
            _quizManager.EvaluateResult(resultColor);
        }
        else
        {
            _helpText.text = _helpText_Result;
            _helpPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Toggles the display of the result sphere name
    /// </summary>
    public void ToggleResultName()
    {
        if (_mode == GameMode.Learn && _state == GameState.ShowingResultSphere)
        {
            if (_resultPanel.active)
            {
                _resultPanel.SetActive(false);
            }
            else
            {
                _resultName.color = _resultColor;
                _resultName.text = ColorPreset.GetDisplayNameByColor(_resultColor);
                _resultPanel.SetActive(true);
            }
        }
    }

}