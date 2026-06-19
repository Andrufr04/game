using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    // Новая настройка для длины луча
    public float groundCheckDistance = 0.2f;

    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;
        bool jumpPressed = false;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveVertical = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveVertical = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveHorizontal = 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveHorizontal = -1f;

            if (Keyboard.current.spaceKey.isPressed) jumpPressed = true;
        }

        Vector3 movement = (transform.right * moveHorizontal) + (transform.forward * moveVertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);

        // ИСПОЛЬЗУЕМ НОВУЮ ПЕРЕМЕННУЮ ДЛЯ ЛУЧА
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);

        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        bool isMoving = movement.magnitude > 0.1f;
        anim.SetBool("isWalking", isMoving);
        anim.SetBool("isGrounded", isGrounded);
    }
}