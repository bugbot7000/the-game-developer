using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.DreamOS
{
    public class GameHubLibraryItem : MonoBehaviour
    {
        [SerializeField] private Image iconObject;
        [SerializeField] private Image bannerObject;
        [SerializeField] private TextMeshProUGUI gameTitle;
        public ButtonManager playButton;
        [HideInInspector] public int gameIndex;

        public void SetGameName(string text)
        {
            gameTitle.SetText(text);
        }

        public void SetIcon(Sprite icon)
        {
            iconObject.sprite = icon;
        }

        public void SetBanner(Sprite banner)
        {
            bannerObject.sprite = banner;
        }
    }
}