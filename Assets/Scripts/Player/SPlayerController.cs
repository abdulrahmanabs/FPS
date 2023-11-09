using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class SPlayerController : MonoBehaviour
{
 private Rigidbody rb;

    [Header("Movement Properties")]
    public float walkSpeed = 8.0f;
    public float runSpeed = 12.0f;
    public float changeInSpeed = 10.0f;
    public float maxSpeed = 150.0f;
    
    [Header("Jump")]
    public float jumpForce = 500.0f;
    public float jumpCooldown = 1.0f;

    public Transform groundChecker;
    public float groundCheckerDist = 0.2f;

    private bool isGrounded = false;
    private Vector3 inputForce;
    private float prevY;
    private bool jumpBlocked = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        UpdateGroundedState();

        float vInput = Input.GetAxisRaw("Vertical");
        float hInput = Input.GetAxisRaw("Horizontal");
        inputForce = (transform.forward * vInput + transform.right * hInput).normalized * 
            (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed);

        if (isGrounded)
        {
            HandleGroundMovement();
        }
        else
        {
            HandleAirMovement();
        }
    }

    private void UpdateGroundedState()
    {
        isGrounded = Mathf.Abs(rb.velocity.y - prevY) < 0.1f &&
            Physics.OverlapSphere(groundChecker.position, groundCheckerDist).Length > 1;
        prevY = rb.velocity.y;
    }

    private void HandleGroundMovement()
    {
        if (Input.GetButton("Jump") && !jumpBlocked)
        {
            rb.AddForce(-jumpForce * rb.mass * Vector3.down);
            jumpBlocked = true;
            Invoke(nameof(UnblockJump), jumpCooldown);
        }

        rb.velocity = Vector3.Lerp(rb.velocity, inputForce, changeInSpeed * Time.fixedDeltaTime);
        ClampVelocityMagnitude(maxSpeed);
    }

    private void HandleAirMovement()
    {
        rb.velocity = ClampSqrMagnitude(rb.velocity + inputForce * Time.fixedDeltaTime, rb.velocity.sqrMagnitude);
    }

    private void ClampVelocityMagnitude(float maxMagnitude)
    {
        if (rb.velocity.sqrMagnitude > maxMagnitude * maxMagnitude)
        {
            rb.velocity = rb.velocity.normalized * maxMagnitude;
        }
    }

    private Vector3 ClampSqrMagnitude(Vector3 vector, float maxSqrMagnitude)
    {
        if (vector.sqrMagnitude > maxSqrMagnitude)
        {
            vector = vector.normalized * Mathf.Sqrt(maxSqrMagnitude);
        }
        return vector;
    }

    private void UnblockJump()
    {
        jumpBlocked = false;
    }
}
