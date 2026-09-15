using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager uIManager;
    public List<Gem> gems = new List<Gem>();
    public Player player;
    [SerializeField] private int score = 0; public int Score => score;  
    // //hoặc (nếu ko cần hiện ở trong inspector)
    // public int Score { get; private set; } = 0;

    public TextMeshProUGUI ShowScore;
    public Camera mainCamera;
    public RectTransform scoreIconTarget;

    private const string SCORE_KEY = "PlayerScore";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // đã có 1 instance rồi, huỷ cái mới
            return;
        }
        Instance = this;
        ShowScore.text = string.Empty;

        LoadScore();
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterGems(Gem g) => gems.Add(g);
    public void UnRegisterGems(Gem g) => gems.Remove(g);

    public void AddScore(int value)
    {
        score += value;
        SaveScore(); 
    }

    public void ShowPlayerScore()
    {
        ShowScore.text = $"Score: {score}";
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    private void LoadScore()
    {
        score = PlayerPrefs.GetInt(SCORE_KEY, 0);
    }

    // Gọi khi bấm nút Reset
    public void ResetScore()
    {
        score = 0;
        SaveScore(); 
    }
}