using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool IsActive = true;

    [Header("Settings")]
    public LayerMask GroundedLayerMask;
    public Vector3 GroundCheckPosition;
    public Vector3 GroundCheckSize;

    [field: SerializeField]
    public bool IsGrounded { get; private set; }

    private new Transform transform;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (IsActive == true)
            CheckForGrounded();
    }

    private void CheckForGrounded()
    {
        IsGrounded = Physics.OverlapBox(transform.position + GroundCheckPosition, GroundCheckSize / 2, Quaternion.identity, GroundedLayerMask).Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        this.transform = GetComponent<Transform>();

        Gizmos.DrawCube(transform.position + GroundCheckPosition, GroundCheckSize);
    }
}
