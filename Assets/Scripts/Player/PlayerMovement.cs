using UnityEngine;

namespace Player {
    [SelectionBase]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent (typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour {
        #region Componentes
        private PlayerInput _playerInput;
        private CharacterController _characterController;
        private Transform _cameraTransform;
        #endregion

        #region Movimiento
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 5.0f;
        private Vector3 _movementDir;
        private Vector3 _rotationDir;
        #endregion
        
        #region Gravedad
        [Header("Gravity")]
        [SerializeField] private float mass = 1.0f;
        [SerializeField] private float fallSpeed = 1.0f;
        private float _verticalVelocity;
        private const float GRAVITY = -9.81f;
        #endregion

        #region Eventos Unity
        private void Awake() {
            _playerInput = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();
            _cameraTransform = Camera.main.gameObject.transform;
        }

        private void Update() {
            HandleMovement();
            HandleRotation();
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _rotationDir);
        }
        #endregion

        #region Funciones Privadas
        private void HandleMovement() {
            // Mover al personaje en relacion a la camara
            _movementDir = _cameraTransform.forward * _playerInput.MovementInput.y;
            _movementDir += _cameraTransform.right * _playerInput.MovementInput.x;
            _movementDir.y = CalculateVerticalForce();
            _movementDir.Normalize();
            if (IsFalling()) {
                // Si el jugador esta callendo se ignora el input 
                _movementDir.x = 0;
                _movementDir.z = 0;
                _characterController.Move(_movementDir * (fallSpeed * mass * Time.deltaTime));
            } else {
                // Mover al jugador a la direccion del input
                _characterController.Move(_movementDir * (walkSpeed * Time.deltaTime));
            }
        }

        private void HandleRotation() {
            _rotationDir = _movementDir;
            _rotationDir.y = 0; // Ignorar la gravedad en el vector de rotacion, para que el personaje no mire abajo
            if (_rotationDir == Vector3.zero) return;
            Quaternion targetRotation = Quaternion.LookRotation(_rotationDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private float CalculateVerticalForce() {
            if (_characterController.isGrounded) {
                // Se pone -1 y no 0 para asegurar que el jugador toca el suelo y no esta callendo constantemente
                _verticalVelocity = -1;
            } else {
                _verticalVelocity += GRAVITY * Time.deltaTime;
            }

            return _verticalVelocity;
        }
        #endregion

        #region Funciones Publicas
        public bool IsMoving() {
            return _movementDir.x != 0 || _movementDir.z != 0;
        }

        public bool IsFalling() {
            return !_characterController.isGrounded;
        }
        #endregion
    }
}