using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon._Scripts.UI
{
    public class SongSelection : MonoBehaviour
    {
        [SerializeField] private GameObject buttonTemplate;
        [SerializeField] private Transform buttonParent;
        [SerializeField] private string songsDirectory;


        private void Start()
        {
            LoadSongs();
        }

        private void LoadSongs()
        {
            var audioClips = Resources.LoadAll<AudioClip>(songsDirectory);

            foreach (var audioClip in audioClips)
            {
                var buttonGO = Instantiate(buttonTemplate, buttonParent);
                buttonGO.SetActive(true);
                Debug.Log("Button instantiated");
                buttonGO.name = audioClip.name + "Button";

                var buttonText = buttonGO.GetComponentInChildren<TMP_Text>();
                buttonText.text = audioClip.name;

                var button = buttonGO.GetComponent<Button>();

                button.onClick.AddListener(delegate { PlayerPrefs.SetString("SelectedSong", audioClip.name); });
            }
        }
    }
}