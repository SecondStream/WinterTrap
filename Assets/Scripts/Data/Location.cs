namespace WinterTrap.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Location
    {
        [SerializeField]
        private string _uid;
        [SerializeField]
        private string _localizationKey;

        public string uid => _uid;

        public string localizedName => Localization.Get(_localizationKey);
    }
}