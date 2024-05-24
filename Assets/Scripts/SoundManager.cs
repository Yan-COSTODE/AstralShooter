using System;
using System.Collections.Generic;
using UnityEngine;

public enum ESound
{
	MAIN_MENU,
	BOSS_FIGHT,
	BACKGROUND,
	PLAYER_HEALTH,
	PLAYER_SHIELD,
	PLAYER_SHIELD_GATE,
	PLAYER_SHIELD_REGEN,
	PLAYER_DIE,
	ASTEROID_EXPLODE,
	PICKUP,
	INVICIBILTY
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

	public int Play(ESound _sound, Vector3 _position, float _duration = -1.0f)
	{
		AudioClip _clip = Search(_sound);

		if (!_clip)
			return -1;

		GameObject _gO = new GameObject("Audio Source");
		_gO.transform.SetParent(transform);
		_gO.transform.position = _position;
		AudioSource _audioSource = _gO.AddComponent<AudioSource>();
		_audioSource.clip = _clip;
		_audioSource.Play();
		audioSources.Add(_audioSource);
		
		if (_duration != -1.0f)
			Destroy(_gO, _clip.length);
		else
			Destroy(_gO, _clip.length > _duration ? _duration : _clip.length);
		
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
    #endregion
}

[Serializable]
public struct SoundElem
{
	[SerializeField] private ESound sound;
	[SerializeField] private AudioClip audio;

	public ESound Sound => sound;
	public AudioClip Audio => audio;
}