using Rimaethon._Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Scripts.UI
{
    public class UIPage : MonoBehaviour
    {
        [Tooltip("The default UI to have selected when opening this page")]
        public GameObject defaultSelected;


        public void SetSelectedUIToDefault()
        {
            // if (GameManager.Instance == null || GameManager.Instance.UIManager == null ||
            //     defaultSelected == null) return;
            // GameManager.Instance.UIManager.eventSystem.SetSelectedGameObject(null);
            // GameManager.Instance.UIManager.eventSystem.SetSelectedGameObject(defaultSelected);
        }
    }
}