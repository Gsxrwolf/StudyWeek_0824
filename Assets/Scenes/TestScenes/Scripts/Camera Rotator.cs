using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [Header("Settings")]
    public Transform CameraTransform;
    public float LookSensitivity = 1;
    public Angles AngleSettings;

    private float cameraRotationY;

    private void Start()
    {
        cameraRotationY = 0;
        cameraRotationY = CameraTransform.rotation.eulerAngles.y;
    }
    public void Update()
    {
    }
    public void Rotate(float rotation)
    {
        cameraRotationY += -rotation * LookSensitivity;
        cameraRotationY = Mathf.Clamp(cameraRotationY, AngleSettings.Min, AngleSettings.Max);
        CameraTransform.eulerAngles = new Vector3(cameraRotationY, CameraTransform.eulerAngles.y, CameraTransform.eulerAngles.z);
    }

    [System.Serializable]
    public class Angles
    {
        public float Min = -75;
        public float Max = 75;
    }
}
