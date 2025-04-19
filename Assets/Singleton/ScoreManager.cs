using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    private int score = 0;
    private int highScore = 0;
    [SerializeField] private TMP_Text scoreText;

    private void Awake()
    {
        instance = this;
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("HighScore");

        Debug.Log(PlayerPrefs.GetInt("HighScore"));
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreUI();

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }
    private void UpdateScoreUI()
    {
        scoreText.text =  score.ToString();
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
