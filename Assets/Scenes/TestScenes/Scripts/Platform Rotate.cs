using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    #region Rotate   
    [SerializeField] private Vector3 _rotation;
    void Update()
    {
        transform.Rotate(_rotation * Time.deltaTime);
    }
    #endregion
}
