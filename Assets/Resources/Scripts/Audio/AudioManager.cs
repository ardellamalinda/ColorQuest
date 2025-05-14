using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager Instance;
	public Sound[] musicSounds, sfxSounds;
	public AudioSource musicSource, sfxSource;

	private Dictionary<string, Sound> musicDictionary = new Dictionary<string, Sound>();
	private Dictionary<string, Sound> sfxDictionary = new Dictionary<string, Sound>();

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
			Setup();
		}
		else
		{
			if (Instance != this)
			{
				Destroy(gameObject);
			}
		}
	}

	private void Setup()
	{
		for (int i = 0; i < musicSounds.Length; i++)
		{
			musicDictionary[musicSounds[i].name] = musicSounds[i];
		}
		for (int i = 0; i < sfxSounds.Length; i++)
		{
			sfxDictionary[sfxSounds[i].name] = sfxSounds[i];
		}
	}
	public void StopMusic()
	{
		musicSource.Stop();
	}

	public void PlayMusic(SoundName name)
	{
		Sound s = Array.Find(musicSounds, sound => sound.soundName == name);
		if (s != null)
		{
			musicSource.clip = s.clip;
			musicSource.Play();
		}
	}
	public void PlayMusic(string name)
	{
		Sound s = musicDictionary[name];
		if (s != null)
		{
			musicSource.clip = s.clip;
			
			musicSource.Play();
		}
	}
	public void PlaySfx(string name)
	{
		Sound s = sfxDictionary[name];
		if (s != null)
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}
	public void PlaySfx(AudioClip clip)
	{
		sfxSource.PlayOneShot(clip);
		
	}
	public void PlaySfx(SoundName name)
	{

		Sound s = Array.Find(sfxSounds, sound => sound.soundName == name);
		if (s != null)
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}
	public void PlaySfx(Sound sound)
	{
		if (sound != null)
		{
			sfxSource.PlayOneShot(sound.clip);
		}
	}
	public void PlaySfx(Sound[] randomSound)
	{

		if (randomSound != null)
		{
			sfxSource.PlayOneShot(randomSound[UnityEngine.Random.Range(0, randomSound.Length)].clip);
		}
	}
	public void PlaySfx(SoundName[] randomName)
	{
		SoundName name = randomName[UnityEngine.Random.Range(0, randomName.Length)];
		Sound s = Array.Find(sfxSounds, sound => sound.soundName == name);
		if (s != null)
		{
			sfxSource.PlayOneShot(s.clip);
		}
	}
	public void SetSfxVolume(float volume)
	{
		sfxSource.volume = volume;
	}
	public void SetMusicVolume(float volume)
	{
		musicSource.volume = volume;
	}
}

