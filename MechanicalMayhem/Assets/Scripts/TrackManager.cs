using System.Collections.Generic;
using System;
using UnityEngine;
using SWAssets;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TrackManager : Singleton<TrackManager>
{

	[SerializeField] private List<SceneTracks> tracks;
	private AudioSource source;
	private int buildIndex;
	private int trackIndex;

	private void Awake()
	{
		source = GetComponent<AudioSource>();
		source.loop = true;
		source.playOnAwake = true;

		SceneManager.activeSceneChanged += OnSceneLoad;
		InvokeRepeating(nameof(UpdateTrack), 0.5f, 0.5f);
	}

	public bool PlayTrack(int index)
	{
		try
		{
			source.PlayOneShot(tracks[buildIndex].sceneTracks[index].clip);
			source.loop = tracks[buildIndex].sceneTracks[index].once;
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool PlayTrack(string name)
	{
		foreach (Track track in tracks[buildIndex].sceneTracks)
		{
			if (track.clip.name == name)
			{
				source.PlayOneShot(track.clip);
				source.loop = track.once;
				return true;
			}
		}

		return false;
	}

	private void OnSceneLoad(Scene _A, Scene _B)
	{
		int buildIndex = _B.buildIndex;
		if (buildIndex == -1 || tracks.Count < buildIndex)
			return;

		this.buildIndex = buildIndex;
		trackIndex = 0;
		PlayTrack(trackIndex);
	}

	private void UpdateTrack()
	{
		if (source.isPlaying)
			return;

		PlayTrack(trackIndex++);
	}

	[Obsolete("Please put all audio clips in the sound manager array and call it by its name or index")]
	public bool PlayTrack(AudioClip clipToPlay)
	{
		source.PlayOneShot(clipToPlay);
		return true;
	}

	[Serializable]
	private class SceneTracks
	{
		public List<Track> sceneTracks;
	}

	[Serializable]
	private class Track
	{
		public AudioClip clip;
		public bool once = false;
	}

}
