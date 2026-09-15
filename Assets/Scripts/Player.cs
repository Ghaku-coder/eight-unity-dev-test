using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Player : MonoBehaviour
{
    [Header("Controller")]
    public FixedJoystick joystick;

    [Header("Di chuyen")]
    private float speed = 10f;
    private float jumpForce = 5f;

    [Header("Camera")]
    public Transform cameraPlayer;

    [Header("trang thai hien tai")]
    public bool isAttacking { get; private set; } = false;

    // //hoặc (để vẫn hiện trong Inspector)
    // [SerializeField] private bool isAttacking = false; public bool IsAttacking => isAttacking;

    private bool isRunning = false;
    private bool isClimbing = false;

    public GroundChecker groundChecker;
    public Animator animator;
    private Rigidbody rb;
    public bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        isGrounded = groundChecker.IsGrounded;

        PlayerRunning();
    }

    private void PlayerRunning()
    {
        Vector3 camForward = cameraPlayer.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraPlayer.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 moveDir = camForward * joystick.Vertical + camRight * joystick.Horizontal;

        rb.linearVelocity = new Vector3(moveDir.x * speed, rb.linearVelocity.y, moveDir.z * speed);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            isRunning = true;
            animator.SetBool("isRunning", true);

            Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            if (flatVelocity.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(flatVelocity);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            isRunning = false;
        }
    }

    public void PlayerJumping()
    {
        if (isClimbing || !isGrounded) return; // không cho nhảy khi đang leo

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
    }

    private void CheckGround()
    {
        if (isClimbing) return; // đang leo thì bỏ qua check ground như cũ

        if (!isGrounded && !isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false); //checkground nhanh quá nên tắt luôn animation jumping
        }
    }

    public void PlayerAttacking(bool value)
    {
        isAttacking = value;
    }
}