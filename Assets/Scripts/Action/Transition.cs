namespace WinterTrap.Action
{
    using System;
    using Common;
    using Data;
    using UnityEngine;
    using View;

    public class Transition : BaseBehaviour, IPlayerAction
    {
        [SerializeField]
        private Location _location;

        public string transitionId => name;

        public string localizedMessage => Localization.Get("transition") + _location.localizedName;

        public void DoIt(IPlayerView view, Action onComplete = null)
        {
            var manager = GameObject.FindObjectOfType<LocationManager>();
            if (manager != null)
                manager.LoadLocation(_location.uid, onComplete);
        }
    }
}