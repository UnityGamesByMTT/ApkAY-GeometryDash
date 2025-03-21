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


    private void Start()
    {
        if (PlayBtn) PlayBtn.onClick.RemoveAllListeners();
        if (PlayBtn) PlayBtn.onClick.AddListener(() => {
            SceneManager.LoadScene("GameScene");
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
    }
}
