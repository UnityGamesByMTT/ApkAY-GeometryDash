using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainSceneManager : MonoBehaviour
{
    [Header("HomePage")]
    [SerializeField] private Button PlayBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button SettingsBtn;
    [SerializeField] private Button LeaderboardBtn;

    [Header("SettingPage")]
    [SerializeField] private GameObject SettingsPage;
    [SerializeField] private Button SettingsExitBtn;

    [Header("HomePage")]
    [SerializeField] private GameObject ShopPage;
    [SerializeField] private Button ShopExitBtn;

    [Header("LeaderBoard")]
    [SerializeField] private GameObject LeaderboardPage;
    [SerializeField] private Button LeaderboardExitBtn;

    [Header("LevelPage")]
    [SerializeField] private Button LEv1;
    [SerializeField] private Button Lev2;
    [SerializeField] private Button Lev3;
    [SerializeField] private Button Lev4;
    [SerializeField] private Button Lev5;
    [SerializeField] private Button Lev6;
    [SerializeField] private Button Lev7;
    [SerializeField] private Button Lev8;
    [SerializeField] private Button Lev9;
    [SerializeField] private Button Lev10;
    [SerializeField] private Button Lev11;


    private int LastPlayed;

    private void Start()
    {
        if (PlayBtn) PlayBtn.onClick.RemoveAllListeners();
        if (PlayBtn) PlayBtn.onClick.AddListener(() => {
            if (PlayerPrefs.GetInt("LastLev") == 0)
            {
                SceneManager.LoadScene("Lev1");
            }
            else
            {
                SceneManager.LoadScene("Lev"+ PlayerPrefs.GetInt("LastLev").ToString());

            }

        });

        if (ShopBtn) ShopBtn.onClick.RemoveAllListeners();
        if (ShopBtn) ShopBtn.onClick.AddListener(() => {
            ShopPage.SetActive(true);
        });

        if (SettingsBtn) SettingsBtn.onClick.RemoveAllListeners();
        if (SettingsBtn) SettingsBtn.onClick.AddListener(() => {
            SettingsPage.SetActive(true);
        });

        if (SettingsExitBtn) SettingsExitBtn.onClick.RemoveAllListeners();
        if (SettingsExitBtn) SettingsExitBtn.onClick.AddListener(() => {
            SettingsPage.SetActive(false);
        });

        if (LeaderboardBtn) LeaderboardBtn.onClick.RemoveAllListeners();
        if (LeaderboardBtn) LeaderboardBtn.onClick.AddListener(() => {
            LeaderboardPage.SetActive(true);
        });

        if (LeaderboardExitBtn) LeaderboardExitBtn.onClick.RemoveAllListeners();
        if (LeaderboardExitBtn) LeaderboardExitBtn.onClick.AddListener(() => {
            LeaderboardPage.SetActive(false);
        });

        if (ShopExitBtn) ShopExitBtn.onClick.RemoveAllListeners();
        if (ShopExitBtn) ShopExitBtn.onClick.AddListener(() => {
            ShopPage.SetActive(false);
        });

        if (LEv1) LEv1.onClick.RemoveAllListeners();
        if (LEv1) LEv1.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 1);
            SceneManager.LoadScene("Lev1");
        });

        if (Lev2) Lev2.onClick.RemoveAllListeners();
        if (Lev2) Lev2.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 2);
            SceneManager.LoadScene("Lev2");
        });
        if (Lev3) Lev3.onClick.RemoveAllListeners();
        if (Lev3) Lev3.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 3);
            SceneManager.LoadScene("Lev3");
        });
        if (Lev4) Lev4.onClick.RemoveAllListeners();
        if (Lev4) Lev4.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 4);
            SceneManager.LoadScene("Lev4");
        });
        if (Lev5) Lev5.onClick.RemoveAllListeners();
        if (Lev5) Lev5.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 5);
            SceneManager.LoadScene("Lev5");
        });

        if (Lev6) Lev6.onClick.RemoveAllListeners();
        if (Lev6) Lev6.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 6);
            SceneManager.LoadScene("Lev6");
        });

        if (Lev7) Lev7.onClick.RemoveAllListeners();
        if (Lev7) Lev7.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 7);
            SceneManager.LoadScene("Lev7");
        });

        if (Lev8) Lev8.onClick.RemoveAllListeners();
        if (Lev8) Lev8.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 8);
            SceneManager.LoadScene("Lev8");
        });

        if (Lev9) Lev9.onClick.RemoveAllListeners();
        if (Lev9) Lev9.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 9);
            SceneManager.LoadScene("Lev9");
        });

        if (Lev10) Lev10.onClick.RemoveAllListeners();
        if (Lev10) Lev10.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 10);
            SceneManager.LoadScene("Lev10");
        });

        if (Lev11) Lev11.onClick.RemoveAllListeners();
        if (Lev11) Lev11.onClick.AddListener(() => {
            PlayerPrefs.SetInt("LastLev", 11);
            SceneManager.LoadScene("Lev11");
        });


    }
}
