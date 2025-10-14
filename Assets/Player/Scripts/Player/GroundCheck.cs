using System;
using UnityEngine;

namespace Player.Scripts.Player
{
    public class GroundCheck : MonoBehaviour
    {
        public event Action<bool> OnGroundedChanged;
        private bool BisGrounded { get; set; }

        private LayerMask _groundLayer;
        private readonly float _groundCheckDistance = 1.1f;

        void Awake()
        {
            _groundLayer = LayerMask.GetMask("Ground");
        }
        
        void Update()
        {
            Debug.DrawRay(transform.position, Vector2.down * _groundCheckDistance, 
                BisGrounded ? Color.green : Color.red);
        }
        
        private void FixedUpdate()
        {
            bool wasGrounded = BisGrounded;
            BisGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, _groundLayer);

            if (BisGrounded != wasGrounded)
            {
                OnGroundedChanged?.Invoke(BisGrounded);
            }
        }
    }
}
