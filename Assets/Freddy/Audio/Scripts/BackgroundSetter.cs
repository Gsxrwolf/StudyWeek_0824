using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSetter : AudioSetter
{
    // The background music clip.
    public AudioClip Clip;

    // THe music volume.
    public float Volume = 1;
    
    // Will be performed once the setter is loaded.
    protected override void Execute()
    {
        // Play background music ...
        AudioManager.Instance.PlayBackgroundMusic(Clip, Volume, true);
    }
}
