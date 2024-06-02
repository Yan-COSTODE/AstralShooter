using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private GameObject mainButton;
	[SerializeField] private string sceneName;
	[SerializeField] private GameObject playScreen;
	[SerializeField] private GameObject scoreboardScreen;
	[SerializeField] private GameObject optionScreen;
	[SerializeField] private GameObject quitScreen;
	[Header("Play")] 
	[SerializeField] private TMP_InputField nameInput;
	[Header("Scoreboard")]
	[SerializeField] private Transform highScoreSocket;
	[SerializeField] private GameObject highScoreReturn;
	[SerializeField] private UIScoreboardEntry prefab;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    ClearScoreboard();
	    BackToMenu();
    }

    private void DisableALl()
    {
	    foreach (UITweening _tween in gameObject.GetComponentsInChildren<UITweening>())
		    _tween.Reset();
	    
	    playScreen.SetActive(false);
	    scoreboardScreen.SetActive(false);
	    optionScreen.SetActive(false);
	    quitScreen.SetActive(false);
    }

    public void BackToMenu()
    {
	    DisableALl();
	    mainButton.SetActive(true);
    }

    public void GoToMenu(GameObject _object)
    {
	    DisableALl();
	    mainButton.SetActive(false);
	    _object.SetActive(true);
    }
    
    public void Play()
    {
	    string _name = nameInput.text;
	    PlayerPrefs.SetString("PlayerName", _name);
	    PlayerPrefs.Save();
	    SoundManager.Instance.Stop();
	    UIManager.Instance.SetUI(true);
	    SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
		#if UNITY_EDITOR
			    UnityEditor.EditorApplication.isPlaying = false;
		#else
		            Application.Quit();
		#endif
    }

    public void Option()
    {
    }
    
    public void ClearScoreboard()
    {
	    BackToMenu();
	    
	    for (int _i = 0; _i < highScoreSocket.childCount; _i++)
		    Destroy(highScoreSocket.GetChild(_i).gameObject);
    }
    
    public void ShowScoreboard()
    {
	    ClearScoreboard();
	    GoToMenu(scoreboardScreen);
	    List<ScoreEntry> _scores = Scoreboard.Instance.HighScores;
	    
	    foreach (ScoreEntry _score in _scores)
			Instantiate(prefab, highScoreSocket).Setup(_score);
    }
    #endregion
}
