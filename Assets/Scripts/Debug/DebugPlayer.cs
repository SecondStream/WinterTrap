namespace WinterTrap.Debug
{
    using UnityEngine;

    public class DebugPlayer : MonoBehaviour
    {
        [SerializeField]
        private CharacterController _controller;

        [SerializeField]
        private float _speed = 2f;

        [SerializeField]
        private float _runMultiply = 2f;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private float _cameraMaxAngle = 80;

        private float _currentCameraAngle = 0;

        private void Update()
        {
            var mx = Input.GetAxis("Mouse X");
            var my = Input.GetAxis("Mouse Y");
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            if (!mx.Equals(0))
            {
                mx *= DebugSettings.MOUSE_SENSITIVITY;
                transform.Rotate(Vector3.up, mx);
            }

            if (!my.Equals(0))
            {
                my *= DebugSettings.MOUSE_SENSITIVITY;
                var oldAngle = _currentCameraAngle;
                _currentCameraAngle = Mathf.Clamp(_currentCameraAngle + my,
                    -_cameraMaxAngle, _cameraMaxAngle);
                var offsetRotation = _currentCameraAngle - oldAngle;

                if (!offsetRotation.Equals(0))
                    _camera.transform.Rotate(Vector3.right, -offsetRotation);
            }

            var move = Vector3.zero;
            if (!v.Equals(0))
                move += transform.forward * (v * _speed);

            if (!h.Equals(0))
                move += transform.right * (h * _speed);

            if (Input.GetKey(KeyCode.LeftShift))
                move *= _runMultiply;

            move += Physics.gravity;

            _controller.Move(move * Time.deltaTime);
        }
    }
}
