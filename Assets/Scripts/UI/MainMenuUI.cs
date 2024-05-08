using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using VContainer;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;

    [SerializeField] Button _continueButton;
    [SerializeField] Button _newGameButton;
    [SerializeField] Button _quitButton;

    private ApplicationDataSaver _applicationDataSaver;
    private ApplicationData _applicationData;

    [Inject]
    public void Construct(ApplicationDataSaver appDataSaver, ApplicationData appData)
    {
        _applicationDataSaver = appDataSaver;
        _applicationData = appData;
        _inputField.text = _applicationData.PlayerName;
    }

    private void Start()
    {
        _inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

        _continueButton.onClick.AddListener(() => { Continue(); });
        _newGameButton.onClick.AddListener(() => { NewGame(); });
        _quitButton.onClick.AddListener(() => { Quit(); });
    }

    private void ValueChangeCheck()
    {
        _applicationData.PlayerName = _inputField.text;
    }

    private void Continue()
    {
        // load last player played game...
        if (!_applicationData.NewGame)
        {
            SceneManager.LoadScene(4);
        }
    }

    private void NewGame()
    {
        _applicationData.NewGame = true;
        _applicationData.MapData = new MapData(null, null, 0);
        SceneManager.LoadScene(2);
    }
    private void Quit()
    {
        Application.Quit();
    }
}
