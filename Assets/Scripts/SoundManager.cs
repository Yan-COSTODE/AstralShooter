using System;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
	MAIN_MENU,
	BACKGROUND,
	PLAYER_HEALTH,
	PLAYER_SHIELD,
	PLAYER_SHIELD_GATE,
	PLAYER_SHOOT,
	FIGHTER,
	BOSS,
	PLAYER_DIE,
	ENEMY_DIE,
	ASTEROID_EXPLODE,
	ASTEROID_TRAIL,
	LOOT,
	INVICIBILTY,
	UI_HOVER,
	UI_ACCEPT,
	UI_DECLINE,
	UI_DENIED,
	UI_PAUSE,
	UI_UNPAUSE
}


[Serializable]
public struct SoundElem
{
	[SerializeField] private ESound sound;
	[SerializeField] private AudioClip audio;
	[SerializeField, Range(0.0f, 1.0f)] private float fVolume;

	public ESound Sound => sound;
	public AudioClip Audio => audio;
	public float Volume => fVolume;
}

public class SoundManager : SingletonTemplate<SoundManager>
{
	#region Fields & Properties
	#region Fields
	[SerializeField] private List<SoundElem> sounds = new();
	private List<AudioSource> audioSources = new();
	#endregion
	
	#region Properties
	public AudioSource this[int _index] => audioSources[_index];
	#endregion
	#endregion
	
	#region Methods
	private void Start()
	{
		Stop();
	}

	public int Play(ESound _sound, Vector3 _position, float _duration, bool _loop, bool _ignore)
	{
		AudioClip _clip = Search(_sound);
		
		if (!_clip)
			return -1;

		Debug.Log($"Played {_sound}");
		GameObject _gO = new GameObject($"Audio Source {_sound}");
		_gO.transform.SetParent(transform);
		_gO.transform.position = _position;
		AudioSource _audioSource = _gO.AddComponent<AudioSource>();
		_audioSource.clip = _clip;
		_audioSource.ignoreListenerPause = _ignore;
		_audioSource.volume = SearchVolume(_sound);
		_audioSource.Play();

		if (_loop)
			_audioSource.loop = true;
		else
			Destroy(_gO, _clip.length > _duration ? _duration : _clip.length);
		
		audioSources.Add(_audioSource);
		return audioSources.Count - 1;
	}
	
	public void Pause()
	{
		foreach (AudioSource _audioSource in audioSources)
			_audioSource.Pause();
	}
	
	public void UnPause()
	{
		foreach (AudioSource _audioSource in audioSources)
			_audioSource.UnPause();
	}
	
	public void Stop()
	{
		foreach (AudioSource _audioSource in audioSources)
		{
			if (!_audioSource)
				continue;
			
			_audioSource.Stop();
			Destroy(_audioSource.gameObject);
		}
	}
	
    private AudioClip Search(ESound _sound)
    {
	    foreach (SoundElem _sounds in sounds)
	    {
		    if (_sounds.Sound == _sound)
			    return _sounds.Audio;
	    }

	    return null;
    }

    private float SearchVolume(ESound _sound)
    {
	    foreach (SoundElem _sounds in sounds)
	    {
		    if (_sounds.Sound == _sound)
			    return _sounds.Volume;
	    }

	    return 1.0f;
    }
    #endregion
}