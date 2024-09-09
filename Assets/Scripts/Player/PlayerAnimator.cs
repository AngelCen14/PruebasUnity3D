using UnityEngine;

namespace Player {
    public class PlayerAnimator : MonoBehaviour {
        private Animator _animator;
        private PlayerMovement _playerMovement;

        // Parametros animator
        private readonly int _isMoving = Animator.StringToHash("isMoving");
        private readonly int _isFalling = Animator.StringToHash("isFalling");

        private void Awake() {
            _animator = GetComponent<Animator>();
            _playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
        }

        void Update() {
            // Pasar los parametros al animator
            _animator.SetBool(_isMoving, _playerMovement.IsMoving());
            _animator.SetBool(_isFalling, _playerMovement.IsFalling());
        }
    }
}

