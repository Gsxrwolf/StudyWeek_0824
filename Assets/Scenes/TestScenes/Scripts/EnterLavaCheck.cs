using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterLavaCheck : MonoBehaviour
{
    public bool IsActive = true;

    [Header("Settings")]
    public LayerMask LavaLayerMask;
    public Vector3 LavaCheckPosition;
    public Vector3 LavaCheckSize;

    public AudioClip SizzleSound;

    [field: SerializeField]
    public bool IsEntered { get; private set; }

    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsActive == true)
            CheckForDestroy();
        if (IsEntered == true)
        {
            AudioManager.Instance.PlayEffect(SizzleSound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void CheckForDestroy()
    {
        IsEntered = Physics.OverlapBox(transform.position + LavaCheckPosition, LavaCheckSize / 2, Quaternion.identity, LavaLayerMask).Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        this.transform = GetComponent<Transform>();

        Gizmos.DrawCube(transform.position + LavaCheckPosition, LavaCheckSize);
    }
}
