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

    [Header("SettingPage")]
    [SerializeField] private GameObject SettingsPage;
    [SerializeField] private Button SettingsExitBtn;

    [Header("HomePage")]
    [SerializeField] private GameObject ShopPage;
    [SerializeField] private Button ShopExitBtn;

    [Header("LevelPage")]
    [SerializeField] private Button LEv1;
    [SerializeField] private Button Lev2;
    [SerializeField] private Button Lev3;
    [SerializeField] private Button Lev4;
    [SerializeField] private Button Lev5;
    [SerializeField] private Button Lev6;


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
    }
}
