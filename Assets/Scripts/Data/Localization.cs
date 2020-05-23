namespace WinterTrap.Data
{
    using System.Collections.Generic;
    using Debug;
    using UnityEngine;

    public enum Locale
    {
        En, Ru
    };

    public class Localization
    {
        private static readonly Dictionary<Locale, Dictionary<string, string>> _strings =
            new Dictionary<Locale, Dictionary<string, string>> {
                {Locale.En, new Dictionary<string, string> {
                    // Common
                    {"Undefined", "Undefined"},
                    {"Transition", "Go to "},

                    // Locations
                    {"Trailer", "the trailer"},
                    {"AbandonedValley", "Abandoned valley"},
                }},
                {Locale.Ru, new Dictionary<string, string> {
                    // Общее
                    {"Undefined", "Undefined"},
                    {"Transition", "Перейти: "},

                    // Локации
                    {"Trailer", "вагончик"},
                    {"AbandonedValley", "Заброшенная долина"},
                }},
            };

        private static Dictionary<string, string> _cache;

        public static string Get(string key)
        {
            if (_cache == null)
                _cache = _strings[DebugSettings.LOCALE]; // TODO: Debug

            if (_cache.TryGetValue(key, out var localizedString))
            {
                return localizedString;
            }

            Debug.LogError("String for key '" + key + "' not found!");
            return _cache["undefined"];
        }

        public static void ClearCache()
        {
            _cache = null;
        }
    }
}