using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private static bool _initialized = false;
    private static AudioSource audioSource;
    private static Dictionary<AudioClipName, AudioClip> audioClips = new Dictionary<AudioClipName, AudioClip>();

    public static bool Initialized
    {
        get { return _initialized; }
    }
    public static void Initialize(AudioSource source)
    {
        _initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.BreakBlock, Resources.Load<AudioClip>("AudioClips/breakblock"));
        audioClips.Add(AudioClipName.ButtonClick, Resources.Load<AudioClip>("AudioClips/click"));
        audioClips.Add(AudioClipName.GameOver, Resources.Load<AudioClip>("AudioClips/gameover"));
        audioClips.Add(AudioClipName.PaddleHit, Resources.Load<AudioClip>("AudioClips/paddlehit"));
        audioClips.Add(AudioClipName.ThemeSong, Resources.Load<AudioClip>("AudioClips/themesong"));
        audioSource.clip = audioClips[AudioClipName.ThemeSong];
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    public static void Play(AudioClipName audioClipName)
    {
        audioSource.PlayOneShot(audioClips[audioClipName]);
    }
}
