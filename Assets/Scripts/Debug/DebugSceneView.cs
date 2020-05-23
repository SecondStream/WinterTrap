namespace WinterTrap.Debug
{
    using System.Collections.Generic;
    using UnityEngine;
    using View;

    public class DebugSceneView : BaseBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private GameObject _player;
#pragma warning restore 0649

        private List<IView> _views = new List<IView>();

        protected override void DoAwake()
        {
            GetComponentsInChildren(true, _views);

            // Тут все очень криво, даже для дебага.
            var player = GameObject.FindObjectOfType<DebugPlayer>();
            if (player == null)
            {
                var point = GameObject.Find("point_Player");
                Instantiate(_player, point.transform.position, point.transform.rotation);
            }
            else if (!string.IsNullOrEmpty(player.fromTransition))
            {
                var point = _views.Find(x => x.name == "point_" + player.fromTransition);
                if (point != null)
                    player.SetTransformTo(point);
            }
        }

        protected override void DoDestroy()
        {
            _views.Clear();
            _views = null;
        }
    }
}