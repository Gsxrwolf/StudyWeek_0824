using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Rigidbody), typeof(GroundChecker))]
public class RigidbodyMovement : MonoBehaviour
{
    [Header("Settings")]
    public float Speed;
    public float MaxSpeed;
    public float JumpPower;
    public float JumpSpeedModifier = 1;
    public float FallSpeedModifier = 1;
    public List<AudioClip> JumpSounds = new List<AudioClip>();
    public List<AudioClip> DamageSounds = new List<AudioClip>();

    private new Transform transform;
    private new Rigidbody rigidbody;
    private GroundChecker groundChecker;

    private Vector3 moveDirection;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        groundChecker = GetComponent<GroundChecker>();
    }

    private void FixedUpdate()
    {
        UpdateHorizontalMovement();
        UpdateVerticalMovement();
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void RotateHorizontal(float Rotation)
    {
        var currentRotation = rigidbody.rotation.eulerAngles;
        var targetRotation = currentRotation + new Vector3(0f, Rotation, 0f);
        rigidbody.rotation = Quaternion.Euler(targetRotation);
    }
    public void Jump()
    {
        if (groundChecker.IsGrounded == true)
        {
            // Play pop clip
            AudioClip clip = JumpSounds[Random.Range(0, JumpSounds.Count)];
            AudioManager.Instance.PlayEffect(clip);

            // Jump
            rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
        }
    }
    private void UpdateHorizontalMovement()
    {
        Vector3 currentVelocity = rigidbody.velocity;
        Vector3 targetVelocity = new Vector3(moveDirection.x, 0, moveDirection.z);
        targetVelocity *= Speed;

        targetVelocity = transform.TransformDirection(targetVelocity);

        Vector3 velocityChange = targetVelocity - currentVelocity;
        velocityChange = new Vector3(velocityChange.x, 0, velocityChange.z);
        velocityChange = Vector3.ClampMagnitude(velocityChange, MaxSpeed);

        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
    private void UpdateVerticalMovement()
    {
        if (rigidbody.velocity.y < 0)
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (FallSpeedModifier - 1) * Time.fixedDeltaTime;
        if (rigidbody.velocity.y > 0)
            rigidbody.velocity += Vector3.up * Physics.gravity.y * JumpSpeedModifier * Time.fixedDeltaTime;
    }


    #region Fall Stuff (Freddy)


    private bool _aired = false;

    private Vector3 _pos;

    public float DamageDistance = 10;

    void Update()
    {
        // Save aired state.
        bool tmp = _aired;

        // Get new aired state.
        _aired = !groundChecker.IsGrounded;


        // If in air
        if(_aired)
        {
            // If jumped.
            if(_aired != tmp)
            {
                // Set pos
                _pos = new Vector3(0, transform.position.y, 0);
            }
        }

        // If on ground
        else
        {
            // Landed
            if(_aired != tmp)
            {
                // Make sure the landing position is below start pos.
                if(_pos.y < transform.position.y)
                {
                    return;
                }

                // Get damage distance (Only compare height)
                float dist = Vector3.Distance(_pos, new Vector3(0, transform.position.y, 0));

                // Check if player died from fall damage.
                if(dist >= DamageDistance)
                {
                    // Play damage clip
                    AudioClip clip = DamageSounds[Random.Range(0, DamageSounds.Count)];
                    AudioManager.Instance.PlayEffect(clip);

                    // Load scene
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    #endregion
}
