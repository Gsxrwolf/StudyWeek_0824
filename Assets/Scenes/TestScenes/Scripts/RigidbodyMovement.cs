using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(GroundChecker))]
public class RigidbodyMovement : MonoBehaviour
{
    [Header("Settings")]
    public float Speed;
    public float MaxSpeed;
    public float JumpPower;
    public float JumpSpeedModifier = 1;
    public float FallSpeedModifier = 1;

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
            rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
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
}
