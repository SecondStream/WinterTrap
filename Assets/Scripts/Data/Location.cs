namespace WinterTrap.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Location
    {
#pragma warning disable 0649
        [SerializeField]
        private string _uid;
        [SerializeField]
        private string _localizationKey;
#pragma warning restore 0649

        public string uid => _uid;

        public string localizedName => Localization.Get(_localizationKey);
    }
}