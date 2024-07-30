using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private float movX, movZ;
    public float degrees = 2;
    public float speed = 8;
    public float jumpForce = 6;
    public float fallMultiplier = 1;
    private bool isJumping;
    private Vector3 movement;
    public Animator animator;
 


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movX = Input.GetAxisRaw("Horizontal");
        movZ = Input.GetAxisRaw("Vertical");
        movement = new Vector3(movX, 0, movZ).normalized;

        // Update animator parameters
        animator.SetFloat("Speed", movement.magnitude);
        animator.SetBool("IsJumping", isJumping);
        animator.SetFloat("MovementX", movX);
        animator.SetFloat("MovementZ", movZ);

        Jump();
    }

    private void FixedUpdate()
    {
        Movement();
        JumpFall();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", isJumping);
        }
    }

    private void Movement()
    {
        if (movement.magnitude > 0)
        {
            Vector3 move = transform.position + movement * Time.deltaTime * speed;
            playerRigidbody.MovePosition(move);
        }
        if (movX > 0) // Moving right
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (movX < 0) // Moving left
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (movZ > 0) // Moving forward
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movZ < 0) // Moving backward
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            // Stop Movement
            playerRigidbody.velocity = new Vector3(0, playerRigidbody.velocity.y, 0);
            playerRigidbody.angularVelocity = Vector3.zero;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
            animator.SetBool("IsJumping", isJumping);
        }
    }

    private void JumpFall()
    {
        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }
}
