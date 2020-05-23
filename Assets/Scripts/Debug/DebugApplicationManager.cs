namespace WinterTrap.Debug
{
    using UnityEngine;
    using View;

    public class DebugApplicationManager : BaseBehaviour
    {
        protected override void DoUpdate(float deltaTime)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();
        }
    }
}