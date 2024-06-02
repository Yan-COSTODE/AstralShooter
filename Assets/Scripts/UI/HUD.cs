using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	private Player player;
	[SerializeField] private Image healthBar;
	[SerializeField] private Image shieldBar;
	[SerializeField] private TMP_Text healthText;
	[SerializeField] private TMP_Text playerText;
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private List<GameObject> charge = new();
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void FixedUpdate()
	{
		if (!player)
			return;
		
		UpdateHealthBar(player.Health, player.Shield);
		UpdateScore(player.Score);
	}
	
	public void Init()
	{
		SetNumberofCharge(0);
		GetPlayer();
	}

	
	private void GetPlayer()
	{
		player = Player.Instance;
		
		if (player)
			playerText.text = player.Username;
	}
	
	private void UpdateScore(ulong _score)
	{
		if (!scoreText)
			return;

		scoreText.text = _score.ToString();
	}
	
	private void UpdateHealthBar(Stat _health, Stat _shield)
	{
		if (!healthBar || !shieldBar || !healthText)
			return;
		
		healthBar.fillAmount = Mathf.Clamp01(_health.Percent);
		shieldBar.fillAmount = Mathf.Clamp01(_shield.Percent);
		healthText.text = $"{_health.Current:F0}<color=#42A5DA>+{_shield.Current:F0}</color>";
	}

	public void SetNumberofCharge(int _charge)
	{
		for (int _i = 0; _i < charge.Count; _i++)
			charge[_i].SetActive(_i < _charge);
	}
    #endregion
}
