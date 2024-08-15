using UnityEngine;


/// <summary>
/// Audio Manager as Singleton instance.
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    /// <summary>
    /// The audio source that manages all music sounds.
    /// </summary>
    [SerializeField] private AudioSource _musicSource;

    /// <summary>
    /// The audio source that manages all effects sounds.
    /// </summary>
    [SerializeField] private AudioSource _effectsSource = null;

    // Start or change background music.
    public void PlayBackgroundMusic(AudioClip clip, float volume = 1.0f, bool loop = true)
    {
        if(_musicSource != null)
        {
            _musicSource.clip = clip;
            _musicSource.volume = volume;
            _musicSource.loop = loop;
            _musicSource.Play();
        }
    }

    // Play sound effect.
    public void PlayEffect(AudioClip clip, float volume = 1.0f)
    {
        if(_effectsSource != null)
        {
            _effectsSource.PlayOneShot(clip, volume);
        }
    }
}
