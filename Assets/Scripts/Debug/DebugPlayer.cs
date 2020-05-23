namespace WinterTrap.Debug
{
    using Action;
    using UnityEngine;
    using View;

    public class DebugPlayer : View, IPlayerView, IDebugUIHintTransmitter
    {
#pragma warning disable 0649
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
#pragma warning restore 0649

        private float _currentCameraAngle;

        public string fromTransition { get; set; }

        private IPlayerAction _waitAction;

        public event UIShowHint onShowHint;
        public event UIHideHint onHideHint;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected override void DoUpdate(float deltaTime)
        {
            // Есть баг, что состояние клавиш и осей в состоянии false/0 после смены сцены.
            // UPD: Пофикшено аддитивной загрузкой.
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
            IPlayerAction waitAction = null;
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward,
                out RaycastHit hit, 1.5f, _rayCastMask))
            {
                var action = hit.collider.GetComponent<IPlayerAction>();
                if (action != null)
                {
                    if (Input.GetKeyUp(KeyCode.E))
                    {
                        var transition = action as Transition;
                        if (transition != null)
                            fromTransition = transition.name;

                        action.DoIt(this);
                    }
                    else
                    {
                        waitAction = action;
                    }
                }
            }

            if (waitAction != null)
                AddWaitPressButton(waitAction);
            else
                RemoveWaitPressButton();
        }

        private void AddWaitPressButton(IPlayerAction action)
        {
            if (_waitAction != action)
                onShowHint?.Invoke(this, action.localizedMessage, 2f);

            _waitAction = action;
        }

        private void RemoveWaitPressButton()
        {
            if (_waitAction != null)
                onHideHint?.Invoke(this);
            _waitAction = null;
        }

        protected override void DoDestroy()
        {
            onShowHint = null;
            onHideHint = null;
        }
    }
}
