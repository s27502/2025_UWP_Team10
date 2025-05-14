using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _soundtrackAudioSource;
    [SerializeField] private AudioSource _SFXAudioSource;
    [SerializeField] private List<AudioClip> sfxClips;
    [SerializeField] private List<AudioClip> soundtrackClips;
    private string _currentClipName;

    private GameState _gameState;
    private Dictionary<string, Dictionary<string, AudioClip>> _audioLibrary;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (ServiceLocator.Instance != null)
                ServiceLocator.Instance.Register(this);

            InitializeAudioLibrary();
        }
    }

    private void Update()
    {
        PlayAmbient();
    }

    private void InitializeAudioLibrary()
    {
        _audioLibrary = new Dictionary<string, Dictionary<string, AudioClip>>();

        var sfx = new Dictionary<string, AudioClip>();
        foreach (var clip in sfxClips)
            sfx[clip.name] = clip;

        var music = new Dictionary<string, AudioClip>();
        foreach (var clip in soundtrackClips)
            music[clip.name] = clip;

        _audioLibrary["sfx"] = sfx;
        _audioLibrary["soundtracks"] = music;
    }

    public void PlayAmbient()
    {
        var gm = ServiceLocator.Instance.GetService<GameManager>();
        if (gm == null) return;

        _gameState = gm.GetGameState();

        switch (_gameState)
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
        }
    }
}
