using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private GameObject mainButton;
	[SerializeField] private string sceneName;
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
    }

    public void Play()
    {
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
    
    public void ClearScoreboard()
    {
	    for (int _i = 0; _i < highScoreSocket.childCount; _i++)
		    Destroy(highScoreSocket.GetChild(_i).gameObject);
	    
	    highScoreSocket.gameObject.SetActive(false);
	    highScoreReturn.SetActive(false);
	    mainButton.SetActive(true);
    }
    
    public void ShowScoreboard()
    {
	    ClearScoreboard();
	    List<ScoreEntry> _scores = Scoreboard.Instance.HighScores;
	    
	    foreach (ScoreEntry _score in _scores)
			Instantiate(prefab, highScoreSocket).Setup(_score);
	    
	    highScoreSocket.gameObject.SetActive(true);
	    highScoreReturn.SetActive(true);
	    mainButton.SetActive(false);
    }
    #endregion
}
