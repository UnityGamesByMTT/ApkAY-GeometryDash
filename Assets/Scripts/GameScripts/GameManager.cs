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
    [SerializeField] private Button ExitButton;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private TMP_Text DiamondCount;

    [Header("BG")]
    [SerializeField] private GameObject BG;
    [SerializeField] private float parallaxEffect = 0.5f; 

    [Header("Player")]
    [SerializeField] private GameObject player;

    private int diamondCount = 20;

    private void Awake()
    {
        if(PlayerPrefs.GetInt("Diamonds")== 0) PlayerPrefs.SetInt("Diamonds", diamondCount); ;
        int x = PlayerPrefs.GetInt("Diamonds") - 1;
        StartCoroutine(SquishText(DiamondCount, x+1));
        if (ExitButton) ExitButton.onClick.RemoveAllListeners();
        if (ExitButton) ExitButton.onClick.AddListener(()=> { SceneManager.LoadScene("MainScene"); });
        Time.timeScale = 0;
        if (StartButton) StartButton.onClick.RemoveAllListeners();
        if (StartButton) StartButton.onClick.AddListener(()=> { Time.timeScale = 1; StartPanel.SetActive(false); StartCoroutine(SquishText(DiamondCount, x)); });

    }

    public IEnumerator SquishText(TMP_Text textMesh,int Diamond, float squishDuration =0.1f, float squishAmount =0.7f)
    {
        PlayerPrefs.SetInt("Diamonds", Diamond);
        textMesh.text = Diamond.ToString();
        Vector3 originalScale = textMesh.transform.localScale;
        Vector3 squishedScale = new Vector3(originalScale.x + squishAmount, originalScale.y - squishAmount, originalScale.z);

        // Squish
        float elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            textMesh.transform.localScale = Vector3.Lerp(originalScale, squishedScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Expand back
        elapsedTime = 0f;
        while (elapsedTime < squishDuration)
        {
            textMesh.transform.localScale = Vector3.Lerp(squishedScale, originalScale, elapsedTime / squishDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textMesh.transform.localScale = originalScale;
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
        SceneManager.LoadScene("GameScene");
    }
}
