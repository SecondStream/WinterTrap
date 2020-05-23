namespace WinterTrap.View
{
    using UnityEngine;

    public interface IView
    {
        string name { get; set; }
        Vector3 position { get; set; }
        Quaternion rotation { get; set; }
        void SetTransformTo(IView view);
    }
}