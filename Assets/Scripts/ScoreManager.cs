using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [Header("Scoring Settings")]
    public float baseMultiplier = 0.5f;
    public float multiplierDecay = 0.5f;
    public float minMultiplier = 1f;

    [Header("Level Info")]
    public string levelName = "Level1";
    private int attemptNumber;
    private float levelMultiplier;
    private float distanceTraveled;
    private int currentScore;

    private const string HighScoreKey = "HighScore_";
    private const string AttemptKey = "Attempts_";

    public static int totalScore = 0; // For leaderboard

    void Start()
    {
        levelName = SceneManager.GetActiveScene().name;
        attemptNumber = PlayerPrefs.GetInt(AttemptKey + levelName, 1);
        levelMultiplier = Mathf.Max(minMultiplier, baseMultiplier - (attemptNumber - 1) * multiplierDecay);
        Debug.Log($"Level: {levelName}, Attempt: {attemptNumber}, Multiplier: {levelMultiplier}");
    }

    public void CalculateScore(float distance)
    {
        distanceTraveled = distance;
        currentScore = Mathf.FloorToInt(distanceTraveled * levelMultiplier);
        Debug.Log($"Current Score: {currentScore}");

        int previousHighScore = PlayerPrefs.GetInt(HighScoreKey + levelName, 0);

        if (currentScore > previousHighScore)
        {
            int diff = currentScore - previousHighScore;
            totalScore += diff;
            PlayerPrefs.SetInt("TotalScore",PlayerPrefs.GetInt("TotalScore")+diff);
            PlayerPrefs.SetInt(HighScoreKey + levelName, currentScore);
            Debug.Log($"New High Score for {levelName}: {currentScore} (Added {diff} to total)"+"********************************");
        }
        else
        {
            Debug.Log($"Score {currentScore} did not beat high score {previousHighScore}"+"---------------------");
        }

        // Increment attempt number for next time
        PlayerPrefs.SetInt(AttemptKey + levelName, attemptNumber + 1);
    }

    public int GetCurrentScore() => currentScore;
    public int GetHighScore() => PlayerPrefs.GetInt(HighScoreKey + levelName, 0);
    public int GetTotalScore() => totalScore;
}
