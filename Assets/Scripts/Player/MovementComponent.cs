using System;
using UnityEngine;

namespace Player
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpHeight = 3;
        [SerializeField] private GroundCheck groundCheck;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private readonly float _gravity = Physics.gravity.y;
        private bool _bIsJumpRequested;
        private float _moveInput;
        
        private Rigidbody2D _rb;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            HandleMoveInput();
            HandleJumpInput();
            
            ApplyFlip();
        }

        private void FixedUpdate()
        {
            ApplyMove();
            ApplyJump();
        }

        private void HandleMoveInput()
        {
            _moveInput = Input.GetAxisRaw("Horizontal");
        }
        
        private void HandleJumpInput()
        {
            if (Input.GetButtonDown("Jump") && groundCheck.bIsGrounded)
            {
                _bIsJumpRequested = true;
            }
        }

        private void ApplyMove()
        {
            _rb.velocity = new Vector2(_moveInput * speed, _rb.velocity.y);
        }
        
        private void ApplyJump()
        {
            if (_bIsJumpRequested && groundCheck.bIsGrounded)
            { 
                float jumpVelocity = (float) Math.Sqrt(jumpHeight * -2f * _gravity);
                _rb.velocity = new Vector2(_moveInput * speed, jumpVelocity);
                _bIsJumpRequested = false; 
            }
        }
        
        private void ApplyFlip()
        { 
            if (_moveInput > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (_moveInput < 0)
            {
                spriteRenderer.flipX = true;
            }
        }
    }
}
