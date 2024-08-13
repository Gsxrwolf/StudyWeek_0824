using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFlip : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private Vector3 _rotationGoal;
    [SerializeField] private float _goalScale = 1f;
    [SerializeField] private float _speed = 0.5f;
    private float _current, _target;
    private bool _isGrounded;

    private void Start()
    {
        var myValue = Mathf.Lerp(245.5f, 40, 30);
    }
    private void Update()
    {
        if (_isGrounded) _target = _target == 0 ? 1 : 0;

        _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(_rotationGoal), _curve.Evaluate(_current));
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * _goalScale, _curve.Evaluate(_current));
    }
}
