using System.Collections.Generic;
using System.Linq;
using Rimaethon._Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rimaethon._Scripts.UI
{
    public class UIManager : MonoBehaviour
    {
        private List<UIPage> _pages;

        private int _currentPage;
        private int _defaultPage;
        private const int PausePageIndex = 1;
        bool _isUIPagesInitialized;


        private List<UIPage> _staticUIPages;
        private List<UIPage> _dynamicUIPages;

        [HideInInspector] public EventSystem eventSystem;
        private bool _isPaused;
        private List<UIElement> _uiElements;
        
        private void Awake()
        {
            EventManager.Instance.AddHandler(GameEvents.OnTogglePause, TogglePause);
        }
        
        private void Start()
        {

            InitializeFirstPage();
            UpdateUI();
        }
  

        public void InitializeUIPages(List<UIPage> staticPages, List<UIPage> dynamicPages)
        {
            if (_isUIPagesInitialized) return;

            _staticUIPages = staticPages;
            _dynamicUIPages = dynamicPages;

            _isUIPagesInitialized = true;
        }
    



        public void TogglePause()
        {
            if (_isPaused)
            {
                GoToPage(_defaultPage);
                Time.timeScale = 1;
                _isPaused = false;
                Debug.Log("Unpaused");
            }
            else
            {
                GoToPage(PausePageIndex);
                Time.timeScale = 0;
                _isPaused = true;
                Debug.Log("Paused");
            }
        }


        public void UpdateUI()
        {
           // foreach (var uiElement in _uIelements) uiElement.UpdateUI();
        }


        private void InitializeFirstPage()
        {
            GoToPage(_defaultPage);
        }


       


        public void GoToPage(int pageIndex)
        {
            if (pageIndex < _pages.Count && _pages[pageIndex] != null)
            {
                SetActiveAllPages(false);
                _pages[pageIndex].gameObject.SetActive(true);
                _pages[pageIndex].SetSelectedUIToDefault();
            }
        }


        public void GoToPageByName(string pageName)
        {
            var page = _pages.Find(item => item.name == pageName);
            var pageIndex = _pages.IndexOf(page);
            GoToPage(pageIndex);
        }


        private void SetActiveAllPages(bool activated)
        {
            if (_pages == null) return;
            foreach (var page in _pages.Where(page => page != null)) page.gameObject.SetActive(activated);
        }
        public void GıveTestDebug()
        {
            Debug.Log("Test");
        }
    }
}