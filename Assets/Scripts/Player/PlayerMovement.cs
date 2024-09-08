using UnityEngine;

namespace Player {
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent (typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {
        // Componentes
        private PlayerInput playerInput;
        private CharacterController characterController;
        private GameObject cameraObject;

        [Header("Movement")]
        [SerializeField] private float walkSpeed;
        [SerializeField] private float rotationSpeed;
        private Vector3 movementDir;

        private void Awake() {
            playerInput = GetComponent<PlayerInput>();
            characterController = GetComponent<CharacterController>();
            cameraObject = Camera.main.gameObject;
        }

        private void Update() {
            handleMovement();
            handleRotation();
        }

        private void handleMovement() {
            movementDir = cameraObject.transform.forward * playerInput.MovementInput.y;
            movementDir += cameraObject.transform.right * playerInput.MovementInput.x;
            movementDir.y = 0;
            movementDir.Normalize();
            characterController.Move(movementDir * (walkSpeed * Time.deltaTime));
        }

        private void handleRotation() {
            if (movementDir != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(movementDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        public bool isMoving() {
            return Mathf.Abs(movementDir.magnitude) > 0;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, movementDir);
        }
    }
}