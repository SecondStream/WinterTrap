namespace WinterTrap.Debug
{
    using TMPro;
    using UnityEngine;
    using View;

    public class DebugUIHint : BaseBehaviour
    {
#pragma warning disable 0649
        [SerializeField]
        private TMP_Text _text;
#pragma warning restore 0649

        public void Show(string localizedMessage)
        {
            _text.text = localizedMessage;

            if (!gameObject.activeSelf)
                gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}