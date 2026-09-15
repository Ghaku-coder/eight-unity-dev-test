using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField] private string menuScene;
    [SerializeField] private string gameScene;

    [SerializeField] private const string SCORE_KEY = "PlayerScore";

    public void Play()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    // SceneChange.cs
    public void Reset()
    {
        PlayerPrefs.SetInt(SCORE_KEY, 0);
        PlayerPrefs.Save();

        // Nếu GameManager đã tồn tại (persist qua DontDestroyOnLoad) thì đồng bộ luôn
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetScore(); // đã tự SaveScore() bên trong
        }
    }
    }