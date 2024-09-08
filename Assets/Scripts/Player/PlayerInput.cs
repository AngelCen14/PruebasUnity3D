using UnityEngine;

namespace Player {
    public class PlayerInput : MonoBehaviour {
        public Vector2 MovementInput { get; private set; }

        private void Update() {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            MovementInput = new Vector2(x, y).normalized;
        }
    }
}