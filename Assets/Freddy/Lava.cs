using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections.Generic;

public sealed class Lava : MonoBehaviour
{
    [SerializeField] private AudioClip _sizzle;
    [SerializeField] private List<AudioClip> _damage = new List<AudioClip>();

    /// <summary>
    /// Performed once a game object hits the lava.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.Instance.PlayEffect(_sizzle);
        AudioManager.Instance.PlayEffect(_damage[Random.Range(0, _damage.Count)]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
