using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mathematics.Week6
{
    public class PlaneController : MonoBehaviour
    {
        [Header("Movement Properties")]
        [SerializeField] private float velocitySpeed = 5f;
        [SerializeField] private float tiltAngle = 35f;

        private Vector2 moveInput;
        private Rigidbody _myRB;

        private void Start()
        {
            _myRB = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f) * velocitySpeed;
            _myRB.linearVelocity = move;

            if (moveInput != Vector2.zero)
            {
                float tiltX = -moveInput.y * tiltAngle;
                float tiltZ = -moveInput.x * tiltAngle;
                _myRB.rotation = Quaternion.Euler(tiltX, 0f, tiltZ);
            }
            else
            {
                _myRB.rotation = Quaternion.identity;
            }
        }

        public void TranslateVertical(InputAction.CallbackContext context)
        {
            moveInput.y = context.ReadValue<float>();
        }

        public void TranslateHorizontal(InputAction.CallbackContext context)
        {
            moveInput.x = context.ReadValue<float>();
        }
    }
}
