using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioSetter : MonoBehaviour
{
    /// <summary>
    /// Executed once the instance is loaded.
    /// </summary>
    void Awake()
    {
        // Perform logic ...
        Execute();
    }

    /// <summary>
    /// In here, define what the setter should do when loaded.
    /// </summary>
    protected abstract void Execute();
}
