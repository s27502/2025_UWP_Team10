using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonDoNotDestroy<AudioManager>
{
    [SerializeField] private AudioSource _soundtrackAudioSource;
    [SerializeField] private AudioSource _SFXAudioSource;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> soundtrackClips;

    private string _currentClipName;
    private GameState _gameState;

    private Dictionary<string, Dictionary<string, AudioClip>> _audioLibrary;

    protected override void Awake()
    {
        base.Awake();
        if (Instance != this) return;

        if (ServiceLocator.Instance != null)
            ServiceLocator.Instance.Register(this);

        InitializeAudioLibrary();

        var gm = ServiceLocator.Instance.GetService<GameManager>();
        if (gm != null)
        {
            gm.OnGameStateChanged += PlayAmbient;
        }
        
        PlayAmbient(gm.GetGameState());
    }

    private void InitializeAudioLibrary()
    {
        _audioLibrary = new Dictionary<string, Dictionary<string, AudioClip>>();

        var sfx = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
        {
            if (clip != null)
                sfx[clip.name] = clip;
        }

        var music = new Dictionary<string, AudioClip>();
        foreach (var clip in soundtrackClips)
        {
            if (clip != null)
                music[clip.name] = clip;
        }

        _audioLibrary["sfx"] = sfx;
        _audioLibrary["soundtracks"] = music;
    }
    
    private void OnDestroy()
    {
        var gm = ServiceLocator.Instance?.GetService<GameManager>();
        if (gm != null)
        {
            gm.OnGameStateChanged -= PlayAmbient;
        }
    }

    public void PlayAmbient(GameState state)
    {
        switch (state)
        {
            case GameState.BUILDING:
                PlayClip("soundtracks", "BuildingMusic", _soundtrackAudioSource);
                break;
            case GameState.DEFENDING:
                PlayClip("soundtracks", "DefendingMusic", _soundtrackAudioSource);
                break;
        }
    }

    public void PlayClip(string category, string clipName, AudioSource source = null)
    {
        category = category.ToLower();

        if (_audioLibrary.TryGetValue(category, out var group))
        {
            if (group.TryGetValue(clipName, out var clip))
            {
                if (source == null)
                    source = _SFXAudioSource;

                if (source.clip == clip && source.isPlaying)
                    return;

                source.clip = clip;
                source.Play();
                _currentClipName = clipName;
                Debug.Log(clipName + " played");
            }
            else
            {
                Debug.LogWarning($"Clip '{clipName}' not found in category '{category}'.");
            }
        }
        else
        {
            Debug.LogWarning($"Audio category '{category}' not found.");
        }
    }
}
