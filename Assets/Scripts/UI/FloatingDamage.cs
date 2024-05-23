using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private TMP_Text text;
	[SerializeField] private float fLifeTime = 5.0f;
	[SerializeField] private float fFadeTimer = 0.5f;
	private float damage = 0.0f;
	private float fCurrentLifeTime = 0.0f;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void Update()
	{
		SetFade();
		
		if (fCurrentLifeTime >= fLifeTime)
			Destroy(gameObject);
	}

	public void Set(float _damage, Color _color, Vector3 _position)
	{
		transform.position = _position;
		damage += _damage;
		text.text = damage.ToString("F1");
		text.color = _color;
		fCurrentLifeTime = 0.0f;
	}

	private void SetFade()
	{
		fCurrentLifeTime += Time.deltaTime;
		Color _color = text.color;
		
		if (fCurrentLifeTime <= fFadeTimer)
			_color.a = fCurrentLifeTime / fFadeTimer;
		else if (fCurrentLifeTime >= (fLifeTime - fFadeTimer))
			_color.a = (fLifeTime - fCurrentLifeTime) / fFadeTimer;
		else
			_color.a = 1;
		
		text.color = _color;
	}
    #endregion
}
