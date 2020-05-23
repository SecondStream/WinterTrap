namespace WinterTrap.Debug
{
    using Action;
    using UnityEngine;
    using View;

    public class DebugPlayer : View, IPlayerView
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

        [SerializeField]
        private LayerMask _rayCastMask;

        private float _currentCameraAngle = 0;

        public string fromTransition { get; set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected override void DoUpdate(float deltaTime)
        {
            // Есть баг, что состояние клавиш и осей в состоянии false/0 после смены сцены.
            // ну точнее это баг юнити, существующий вот уже лет семь как, вроде как, если
            // грузить сцену асинхронно - проблема уходит.
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

            _controller.Move(move * deltaTime);
        }

        protected override void DoLateUpdate(float deltaTime)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                out RaycastHit hit, 1.5f, _rayCastMask))
            {
                var actions = hit.collider.GetComponents<IPlayerAction>();
                for (int i = 0, len = actions.Length; i < len; ++i)
                {
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        var transition = actions[i] as Transition;
                        if (transition != null)
                            fromTransition = transition.name;

                        actions[i].DoIt(this);
                    }

                    // TODO: Выводить сообщение в UI.
                }
            }
        }
    }
}
