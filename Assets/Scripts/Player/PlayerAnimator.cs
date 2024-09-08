using UnityEngine;

namespace Player {
    public class PlayerAnimator : MonoBehaviour {
        private Animator animator;
        private PlayerMovement playerMovement;

        // Parametros animator
        private int isMoving = Animator.StringToHash("isMoving");

        private void Awake() {
            animator = GetComponent<Animator>();
            playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        }

        void Update() {
            animator.SetBool(isMoving, playerMovement.isMoving());
        }
    }
}

