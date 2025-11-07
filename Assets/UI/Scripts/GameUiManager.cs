using Managers.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Scripts
{
    public class GameUiManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private ScoreManager scoreManager;

        private void Awake()
        {
            pauseButton.onClick.AddListener(OnPauseClicked);
            resumeButton.onClick.AddListener(OnResumeClicked);
            retryButton.onClick.AddListener(OnRetryClicked);
            exitButton.onClick.AddListener(OnExitClicked);

            pauseMenu.SetActive(false);
        }

        private void OnPauseClicked()
        {
            PauseManager.PauseGame();
            pauseMenu.SetActive(true);
            titleText.text = $"Score: {scoreManager.Kills}";
        }

        private void OnResumeClicked()
        {
            PauseManager.ResumeGame();
            pauseMenu.SetActive(false);
        }

        private void OnRetryClicked()
        {
            PauseManager.ResumeGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnExitClicked()
        {
            PauseManager.ResumeGame();
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}
