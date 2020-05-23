namespace WinterTrap.Debug
{
    using System.Collections;
    using UnityEngine;
    using View;

    public class DebugUI : BaseBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private DebugUIHint _hint;
#pragma warning restore 0649

        private IDebugUIHintTransmitter _transmitter;
        private object _hintOwner;
        private Coroutine _hintRoutine;

        protected override void DoAwake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected override void DoStart()
        {
            _transmitter = GameObject.FindObjectOfType<DebugPlayer>(); // XXX

            if (_transmitter != null)
            {
                _transmitter.onShowHint += OnShowHint;
                _transmitter.onHideHint += OnHideHint;
            }

            Cursor.lockState = CursorLockMode.Locked;
        }

        protected override void DoDestroy()
        {
            if (_transmitter != null)
            {
                _transmitter.onShowHint -= OnShowHint;
                _transmitter.onHideHint -= OnHideHint;
            }
        }

        private void OnHideHint(object owner)
        {
            if (_hintOwner == owner)
            {
                _hint.Hide();
                _hintOwner = null;
            }
        }

        private void OnShowHint(object owner, string localizedMessage, float timeseconds)
        {
            if (_hintRoutine != null)
            {
                StopCoroutine(_hintRoutine);
                _hintRoutine = null;
            }

            _hintOwner = owner;
            _hint.Show(localizedMessage);

            if (timeseconds > 0)
                _hintRoutine = StartCoroutine(WaitHintTime(timeseconds));
        }

        private IEnumerator WaitHintTime(float timeSeconds)
        {
            yield return new WaitForSeconds(timeSeconds);
            _hintRoutine = null;
            if (_hintOwner != null)
                OnHideHint(_hintOwner);
        }
    }
}