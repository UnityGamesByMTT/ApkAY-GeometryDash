using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardCard : MonoBehaviour
{
    [SerializeField] private Sprite[] cardSprite;

    [Header("CardOne")]
    [SerializeField] private GameObject Card;
    [SerializeField] private Image CardSprite;

    [SerializeField] private TMP_Text NameTxt;
    [SerializeField] private TMP_Text ScoreTxt;
    [SerializeField] private TMP_Text RankTxt;

    [Header("CardTwo")]
    [SerializeField] private GameObject Card2;
    [SerializeField] private Image CardSprite2;

    [SerializeField] private TMP_Text NameTxt2;
    [SerializeField] private TMP_Text ScoreTxt2;
    [SerializeField] private TMP_Text RankTxt2;

    internal void SetDataCardOne(int rank,string name,string score)
    {
        RankTxt.text = rank.ToString();
        ScoreTxt.text = score.ToString();
        NameTxt.text = name.ToString();

        if (rank <= 3)
        {
            CardSprite.sprite = cardSprite[rank -1];
        }
    }

    internal void SetDataCardTwo(int rank = 1, string name = "", string score = "", bool isActive = false)
    {
        Card2.SetActive(isActive);
        RankTxt2.text = rank.ToString();
        ScoreTxt2.text = score.ToString();
        NameTxt2.text = name.ToString();

        if (rank <= 3)
        {
            CardSprite2.sprite = cardSprite[rank - 1];
        }
    }
}
