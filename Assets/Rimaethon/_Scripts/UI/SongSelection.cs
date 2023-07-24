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


        void Start()
        {
            LoadSongs();
        }

        private void LoadSongs()
        {
            AudioClip[] audioClips = Resources.LoadAll<AudioClip>(songsDirectory);

            foreach (AudioClip audioClip in audioClips)
            {
                GameObject buttonGO = Instantiate(buttonTemplate, buttonParent);
                buttonGO.SetActive(true);
                Debug.Log("Button instantiated");
                buttonGO.name = audioClip.name + "Button";

                TMP_Text buttonText = buttonGO.GetComponentInChildren<TMP_Text>();
                buttonText.text = audioClip.name;

                Button button = buttonGO.GetComponent<Button>();
                
                button.onClick.AddListener(delegate
                {
                    PlayerPrefs.SetString("SelectedSong", audioClip.name);
                });
            }

        }
    }
}