using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private TMP_Text text;
	[SerializeField] private float fLifeTime = 5.0f;
	private Coroutine destroy;
	private float damage;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	public void Set(float _damage, Color _color, Vector3 _position)
	{
		transform.position = _position;
		damage += _damage;
		text.text = damage.ToString("F0");
		text.color = _color;
		ResetLifetime(fLifeTime);
	}

	private void ResetLifetime(float _time)
	{
		if (destroy != null)
			StopCoroutine(destroy);

		destroy = StartCoroutine(DestroyAfter(_time));
	}
	
	private IEnumerator DestroyAfter(float _time)
	{
		yield return new WaitForSeconds(_time);
		Destroy(gameObject);
	}
    #endregion
}
