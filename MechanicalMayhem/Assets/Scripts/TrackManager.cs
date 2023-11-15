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

	// Reference audio source and sets up functions to call not every frame
	private void Awake()
	{
		source = GetComponent<AudioSource>();
		source.loop = true;
		source.playOnAwake = true;

		SceneManager.activeSceneChanged += OnSceneLoad;
		InvokeRepeating(nameof(UpdateTrack), 0.5f, 0.5f);
	}

	// Plays a track based on an index of the tracks List
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

	// Plays track based on name in tracks List
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

	// Plays new track on scene load
	private void OnSceneLoad(Scene _A, Scene _B)
	{
		int buildIndex = _B.buildIndex;
		if (buildIndex == -1 || tracks.Count < buildIndex)
			return;

		this.buildIndex = buildIndex;
		trackIndex = 0;
		PlayTrack(trackIndex);
	}

	// Update track if the track has stopped playing or loop over tracks if the final track has been reached
	private void UpdateTrack()
	{
		if (source.isPlaying)
			return;

		if (trackIndex + 1 >= tracks.Count)
			trackIndex = 0;

		PlayTrack(trackIndex++);
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
