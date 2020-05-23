namespace WinterTrap.View
{
    using System;
    using System.Collections;
    using UnityEngine;

    public abstract class BaseBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            DoAwake();
            StartCoroutine(WaitFirstFrame());
        }

        private void Start()
        {
            DoStart();
        }

        private void Update()
        {
            DoUpdate(Time.deltaTime);
        }

        private void LateUpdate()
        {
            DoLateUpdate(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            DoFixedUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            DoDestroy();
        }

        protected virtual void DoAwake()
        {
        }

        protected virtual void DoStart()
        {
        }

        protected virtual void DoFirstFrame()
        {
        }

        protected virtual void DoUpdate(float deltaTime)
        {
        }

        protected virtual void DoLateUpdate(float deltaTime)
        {
        }

        protected virtual void DoFixedUpdate(float deltaTime)
        {
        }

        protected virtual void DoDestroy()
        {
        }

        private IEnumerator WaitFirstFrame()
        {
            yield return null;
            DoFirstFrame();
        }
    }
}