using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private GameObject asteroids;
	[SerializeField] private IntRange number;
	[SerializeField] private FloatRange delay;
	[SerializeField] private Transform topLeft;
	[SerializeField] private Transform botRight;
	private float fDelay;
	private float fSpawnDelay;
	#endregion
	
	#region Properties
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		fSpawnDelay = delay.Value;
	}

	private void Update()
    {
	    SpawnAsteroids();
    }

	private void OnDrawGizmosSelected()
	{
		Vector3 _center = (topLeft.position + botRight.position) / 2;
		Vector3 _topRight = topLeft.position;
		_topRight.x = botRight.position.x;
		Vector3 _botLeft = botRight.position;
		_botLeft.x = topLeft.position.x;
		Gizmos.DrawCube(_center, (_center - botRight.position) * 2);
		Gizmos.DrawLine(topLeft.position, _topRight);
		Gizmos.DrawLine(_topRight, botRight.position);
		Gizmos.DrawLine(botRight.position, _botLeft);
		Gizmos.DrawLine(_botLeft, topLeft.position);
	}

	private void SpawnAsteroids()
    {
	    fDelay += Time.deltaTime;

	    if (fDelay < fSpawnDelay)
		    return;

	    fDelay = 0.0f;
	    fSpawnDelay = delay.Value;
	    int _number = number.Value;
	    
	    for (int _i = 0; _i < _number; _i++)
		    Instantiate(asteroids, SpawnPosition(), SpawnRotation());
    }

    private Vector3 SpawnPosition()
    {
	    Vector3 _position;
	    _position.x = Random.Range(topLeft.position.x, botRight.position.x);
	    _position.y = Random.Range(botRight.position.y, topLeft.position.y);
	    _position.z = 0.0f;
	    return _position;
    }

    private Quaternion SpawnRotation()
    {
	    Vector3 _euler;
	    _euler.x = 0.0f;
	    _euler.y = 0.0f;
	    _euler.z = Random.Range(0.0f, 360.0f);

	    return Quaternion.Euler(_euler);
    }
    #endregion
}
