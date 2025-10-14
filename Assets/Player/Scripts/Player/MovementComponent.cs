using System;
using UnityEngine;

namespace Player.Scripts.Player
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private float jumpHeight = 3;
        
        private readonly float _gravity = Physics.gravity.y;
        private bool _bIsJumpRequested;
        private float _moveInput;
        private bool _bIsGrounded;
        
        private Rigidbody2D _rb;
        private GroundCheck _groundCheck;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        
        void OnEnable()
        {
            if (_groundCheck != null)
                _groundCheck.OnGroundedChanged += HandleGroundedChanged;
        }

        void OnDisable()
        {
            if (_groundCheck != null)
                _groundCheck.OnGroundedChanged -= HandleGroundedChanged;
        }
        
        void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _groundCheck = GetComponentInChildren<GroundCheck>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _animator = GetComponentInChildren<Animator>();
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
            _animator.SetFloat("Move", Math.Abs(_moveInput));
        }
        
        private void HandleJumpInput()
        {
            if (Input.GetButtonDown("Jump") && _bIsGrounded)
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
            if (_bIsJumpRequested && _bIsGrounded)
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
                _spriteRenderer.flipX = false;
            }
            else if (_moveInput < 0)
            {
                _spriteRenderer.flipX = true;
            }
        }
        
        private void HandleGroundedChanged(bool isGrounded)
        {
            _bIsGrounded = isGrounded;
            _animator.SetBool("IsGrounded", isGrounded);
        }
    }
}
