using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ScoreEntry
{
	public ulong score;
	public string playerName;

	public ScoreEntry(ulong _score, string _playerName)
	{
		score = _score;
		playerName = _playerName;
	}
}

[Serializable]
public class ScoreList
{
	public List<ScoreEntry> scores;

	public ScoreList(List<ScoreEntry> _scores)
	{
		scores = _scores;
	}
}

public class Scoreboard : SingletonTemplate<Scoreboard>
{
	#region Fields & Properties
	#region Fields
	private const string HighScoresKey = "HighScores";
	[SerializeField] private List<ScoreEntry> highScores = new();
	[SerializeField] private int maxScores = 5;
	#endregion
	
	#region Properties
	public List<ScoreEntry> HighScores => highScores;
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    LoadScores();
    }

    public void AddScore(ulong _score, string _playerName)
    {
	    ScoreEntry _newEntry = new ScoreEntry(_score, _playerName);
	    highScores.Add(_newEntry);
	    highScores = highScores.OrderByDescending(_entry => _entry.score).ToList();

	    if (highScores.Count > maxScores)
		    highScores = highScores.Take(maxScores).ToList();
	    
	    SaveScores();
    }

    private void LoadScores()
    {
	    if (!PlayerPrefs.HasKey(HighScoresKey))
		    return;
	    
	    string _json = PlayerPrefs.GetString(HighScoresKey);
	    ScoreList _scoreList = JsonUtility.FromJson<ScoreList>(_json);
	    highScores = _scoreList.scores;
    }
    
    private void SaveScores()
    {
	    string _json = JsonUtility.ToJson(new ScoreList(highScores));
	    PlayerPrefs.SetString(HighScoresKey, _json);
	    PlayerPrefs.Save();
    }
    #endregion
}
