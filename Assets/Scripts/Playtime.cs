using UnityEngine;

public class Playtime : MonoBehaviour
{
    [SerializeField]
    private GameObject LowOnTime;
    public static Playtime instance;
    internal float playTime = 300f;
    private float timeRemaining;
    private bool isPlaying = false;
    
    private void Awake()
    {
        
        if (instance == null )
        {
            instance = this;
        }
    }
    void Start()
    {
        playTime = PlayerPrefs.GetFloat("PlayTime");
        timeRemaining = playTime;
    }

    void Update()
    {
        if (!isPlaying) return;

        timeRemaining -= Time.deltaTime;
      
        GameManager.instance.DiamondCount.text = timeRemaining.ToString("F2");
        if (timeRemaining <= 0)
        {
            EndGame();
        }
    }

    internal void StartTimer()
    {
        Time.timeScale = 1f;
        isPlaying = true;
    }
    internal void SavetheTime()
    {
        PlayerPrefs.SetFloat("PlayTime", timeRemaining);
    }
    internal void EndGame()
    {
        Debug.Log("Time's up!");
        SavetheTime();
        LowOnTime.SetActive(true);
        Time.timeScale = 0;
        enabled = false;
    }
}
