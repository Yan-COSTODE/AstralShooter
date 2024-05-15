using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private float fScrollSpeed = 1.0f;
	[SerializeField] private float fTileSize = 10.0f;
	private Vector3 startPosition;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
    private void Start()
    {
	    startPosition = transform.position;
    }

    private void Update()
    {
	    float newPosition = Mathf.Repeat(Time.time * fScrollSpeed, fTileSize);
	    transform.position = startPosition + Vector3.down * newPosition;
    }
    #endregion
}
