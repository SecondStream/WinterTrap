namespace WinterTrap.View
{
    using UnityEngine;

    public class View : BaseBehaviour, IView
    {
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }

        public void SetTransformTo(IView view)
        {
            position = view.position;
            rotation = view.rotation;
        }
    }
}