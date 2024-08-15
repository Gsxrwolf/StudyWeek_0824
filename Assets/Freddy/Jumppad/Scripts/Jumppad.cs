using System.Collections.Generic;
using UnityEngine;

public sealed class Jumppad : MonoBehaviour
{
    [SerializeField] private float _force;

    [SerializeField] private List<AudioClip> _sounds = new List<AudioClip>();

    /// <summary>
    /// Called once the player collides with the pads trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // Get rigidbody from trigger obj.
        Rigidbody rb = other.GetComponent<Rigidbody>();

        // Make sure rigidbody is valid.
        if(rb != null)
        {
            // Play sound
            AudioManager.Instance.PlayEffect(_sounds[Random.Range(0, _sounds.Count)]);

            // Remove objetcs vertical velocity before impact.
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            // Apply force to object.
            rb.AddForce(Vector3.up * _force, ForceMode.Impulse);
        }
    }
}
