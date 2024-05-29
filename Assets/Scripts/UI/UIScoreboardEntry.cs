using TMPro;
using UnityEngine;

public class UIScoreboardEntry : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private TMP_Text score;
	[SerializeField] private TMP_Text username;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	public void Setup(ScoreEntry _entry)
	{
		score.text = _entry.score.ToString();
		username.text = _entry.playerName;
	}
    #endregion
}
