using UnityEngine;


/// <summary>
/// The base class for all singletons. Every class inheriting from this
/// class will automatically be treated as a singleton.
/// </summary>
/// <typeparam name="T">The type inheriting from this class (Locked to MonoBehavior).</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Private instance of generic singleton.
    /// </summary>
    private static T _instance;

    /// <summary>
    /// Global access to singleon instance.
    /// </summary>
    public static T Instance
    {
        get
        {
            // Check if there is an instance.
            if(_instance == null)
            {
                // Create new game with name of generic class.
                GameObject singleton = new GameObject(typeof(T).Name);

                // Add instance to new game object.
                _instance = singleton.AddComponent<T>();
            }

            // Return old or new instance ...
            return _instance;
        }
    }


    #region Unity Events

        /// <summary>
        /// Executed when instance of script is loaded.
        /// Prevents multiple singleton instances.
        /// </summary>
        void Awake()
        {
            // If there is no instance yet.
            if(_instance == null)
            {
                // This to be new instance (as generic type).
                _instance = this as T;

                // Prevent gameobject of this instance form being destroyed.
                DontDestroyOnLoad(gameObject);
            }

            // If there is an instance and its not this.
            else if(_instance != this)
            {
                // Destroy gameobject of this instance form being destroyed.
                Destroy(gameObject);
            }
        }

    #endregion


    #region Public Methods

        /// <summary>
        /// Destroy the singleton instance.
        /// </summary>
        public void DestroySingleon()
        {
            // Check if there is an instance.
            if(_instance != null)
            {
                // Delete instance.
                _instance = null;
            }
        }

    #endregion
}