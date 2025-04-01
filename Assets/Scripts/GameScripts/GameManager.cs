using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Sprite[] ProgressBarSprites;
    [SerializeField] private Image ProgressBar;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button LevCompButton;
    [SerializeField] private Button ExitButton;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private TMP_Text DiamondCount;
    [Space]
    [Header(" UI Panels")]
    [SerializeField] private GameObject WinPanel;

    [Header("BG")]
    [SerializeField] private GameObject BG;
    [SerializeField] private float parallaxEffect = 0.5f; 

    [Header("Player")]
    [SerializeField] private GameObject player;

    private int diamondCount = 20;
    private bool LevComp = false;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("Diamonds")== 0) PlayerPrefs.SetInt("Diamonds", diamondCount); ;
        int x = PlayerPrefs.GetInt("Diamonds") - 1;
        StartCoroutine(SquishText(DiamondCount, x+1));
        if (ExitButton) ExitButton.onClick.RemoveAllListeners();
        if (ExitButton) ExitButton.onClick.AddListener(()=> { SceneManager.LoadScene("MainScene"); });
        Time.timeScale = 0;
        if (StartButton) StartButton.onClick.RemoveAllListeners();
        if (StartButton) StartButton.onClick.AddListener(()=> {
            
           
                Time.timeScale = 1;
                StartPanel.SetActive(false);
                StartCoroutine(SquishText(DiamondCount, x));
            

        });
        if (LevCompButton) LevCompButton.onClick.RemoveAllListeners();
        if (LevCompButton) LevCompButton.onClick.AddListener(() => {
            
                SceneManager.LoadScene("MainScene");
           

        });
    }

    public IEnumerator SquishText(TMP_Text textMesh, int diamond, float squishDuration = 0.1f, float squishAmount = 0.7f)
    {
        PlayerPrefs.SetInt("Diamonds", diamond);
        textMesh.text = diamond.ToString();

        Vector3 startScale = Vector3.one;
        Vector3 squishedScale = new Vector3(1 + squishAmount, 1 - squishAmount, 1);

        // Squish
        float elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            textMesh.transform.localScale = Vector3.Lerp(startScale, squishedScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Expand back to (1,1,1)
        elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            textMesh.transform.localScale = Vector3.Lerp(squishedScale, startScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.transform.localScale = startScale;
    }

    internal void LevelComplete()
    {
        WinPanel.SetActive(true);
        LevComp = true;
        Time.timeScale = 0;
    }

    private void Update()
    {
        bGEffect();
    }
    void bGEffect()
    {
        float targetX = player.transform.position.x * parallaxEffect;
        BG.transform.position = new Vector3(targetX,0, 0);
    }

    internal IEnumerator  GameOver()
    {
        yield return new WaitForSeconds(0.9F);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
