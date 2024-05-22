using System;
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
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		player = UIManager.Instance.Player;
	}

	private void FixedUpdate()
	{
		if (!player)
			return;
		
		UpdateHealthBar(player.Health, player.Shield);
	}

	private void UpdateHealthBar(Stat _health, Stat _shield)
	{
		if (!healthBar || !shieldBar || !healthText)
			return;
		
		healthBar.fillAmount = Mathf.Clamp01(_health.Current / _health.Max);
		shieldBar.fillAmount = Mathf.Clamp01(_shield.Current / _shield.Max);
		healthText.text = $"{_health.Current:F0}<color=#42A5DA>+{_shield.Current:F0}</color>";
	}
    #endregion
}
