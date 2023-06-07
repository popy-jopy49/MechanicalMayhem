﻿using UnityEngine;
using System.Collections.Generic;
using SWAssets;
using System;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager> {

    [SerializeField] private List<AudioClip> audioClips;
    private AudioSource source;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
		source.loop = false;
		source.playOnAwake = false;
	}

	public bool PlaySound(int index)
    {
		try
		{
			source.PlayOneShot(audioClips[index]);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
    }

    public bool PlaySound(string name)
	{
		foreach (AudioClip clip in audioClips)
		{
			if (clip.name == name)
			{
				source.PlayOneShot(clip);
				return true;
			}
		}

		return false;
	}

	[Obsolete("Please put all audio clips in the sound manager array and call it by its name or index")]
	public bool PlaySound(AudioClip clipToPlay)
	{
		source.PlayOneShot(clipToPlay);
		return true;
	}
    
}
