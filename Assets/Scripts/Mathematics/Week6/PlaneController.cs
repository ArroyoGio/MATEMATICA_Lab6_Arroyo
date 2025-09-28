using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mathematics.Week6
{
    public class PlaneController : MonoBehaviour
    {
        [Header("Controls Properties")]
        [SerializeField] private float pitchPlane;
        [SerializeField] private float pitchGain = 1f;
        [SerializeField] private MinMax pitchTreshHold;
        [SerializeField] private float rollPlane;
        [SerializeField] private float rollhGain = 1f;
        [SerializeField] private MinMax rollTreshHold;
        [SerializeField] private float yawPlane;
        [SerializeField] private float yawGain = 1f;
        [SerializeField] private MinMax yawTreshHold;

        [Header("Rotation Data")]
        [SerializeField] private Quaternion qx = Quaternion.identity;
        [SerializeField] private Quaternion qy = Quaternion.identity;
        [SerializeField] private Quaternion qz = Quaternion.identity;
        [SerializeField] private Quaternion r = Quaternion.identity;

        private float anguloSen;
        private float anguloCos;

        protected float _pitchDirection = 0f;
        protected float _rollDirection = 0f;
        protected float _yawDirection = 0f;

        private void FixedUpdate()
        {
            pitchPlane += _pitchDirection * pitchGain;
            pitchPlane = Mathf.Clamp(pitchPlane, pitchTreshHold.MinValue, pitchTreshHold.MaxValue);

            rollPlane += _rollDirection * rollhGain;
            rollPlane = Mathf.Clamp(rollPlane, rollTreshHold.MinValue, rollTreshHold.MaxValue);

            yawPlane += _yawDirection * yawGain;
            yawPlane = Mathf.Clamp(yawPlane, yawTreshHold.MinValue, yawTreshHold.MaxValue);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * rollPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * rollPlane * 0.5f);
            qz.Set(0, 0, anguloSen, anguloCos);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * pitchPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * pitchPlane * 0.5f);
            qx.Set(anguloSen, 0, 0, anguloCos);

            anguloSen = Mathf.Sin(Mathf.Deg2Rad * yawPlane * 0.5f);
            anguloCos = Mathf.Cos(Mathf.Deg2Rad * yawPlane * 0.5f);
            qy.Set(0, anguloSen, 0, anguloCos);

            r = qy * qx * qz;
            transform.rotation = r;

            UpdatePosition();
        }

        public void RotatePitch(InputAction.CallbackContext context)
        {
            _pitchDirection = context.ReadValue<float>();
        }

        public void RotateRoll(InputAction.CallbackContext context)
        {
            _rollDirection = context.ReadValue<float>();
        }

        public void RotateYaw(InputAction.CallbackContext context)
        {
            _yawDirection = context.ReadValue<float>();
        }

        private float _verticalDirection = 0f;
        private float _horizontalDirection = 0f;
        [SerializeField] private float velocitySpeed = 5f;

        private Rigidbody _myRB;

        private void Start()
        {
            _myRB = GetComponent<Rigidbody>();
        }

        public void TranslateVertical(InputAction.CallbackContext context)
        {
            _verticalDirection = context.ReadValue<float>();
        }

        public void TranslateHorizontal(InputAction.CallbackContext context)
        {
            _horizontalDirection = context.ReadValue<float>();
        }

        private void UpdatePosition()
        {
            Vector3 move = transform.forward * velocitySpeed;
            move += transform.right * _horizontalDirection * velocitySpeed;
            move += transform.up * _verticalDirection * velocitySpeed;
            _myRB.velocity = move;
        }
    }

    [System.Serializable]
    public struct MinMax
    {
        public float MinValue;
        public float MaxValue;
    }
}
