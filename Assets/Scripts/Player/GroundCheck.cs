using UnityEngine;

namespace Player
{
    public class GroundCheck : MonoBehaviour
    {
    
        public bool bIsGrounded;
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                bIsGrounded = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                bIsGrounded = false;
            }
        }
    }
}
