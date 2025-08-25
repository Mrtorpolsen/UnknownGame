using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public TMP_Text northCurrencyText;
    public TMP_Text southCurrencyText;

    public Transform north;
    public Transform south;

    public Dictionary<Team, int> currency;


    [Header("References")]
    [SerializeField] public Canvas gameUI;

    [Header("Attributes")]
    [SerializeField] float currencyTimer = 0f;
    [SerializeField] float currencyInterval = 1f;
    [SerializeField] int incomePerTick = 20;
    [SerializeField] public bool isGameOver = false;
    [SerializeField] public bool isGameRunning = false;
    [SerializeField] Team winningTeam;

    private void Awake()
    {
        main = this;

        currency = new Dictionary<Team, int>()
        {
            { Team.North, 300 },
            { Team.South, 300 },
        };

        UpdateCurrencyText();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        { 
            currencyTimer += Time.deltaTime;

            if(currencyTimer >= currencyInterval)
            {
                currencyTimer = 0f;

                AddCurrency(Team.North, incomePerTick);
                AddCurrency(Team.South, incomePerTick);
            }
        }
    }

    public void AddCurrency(Team team, int amount)
    {
        currency[team] += amount;
        UpdateCurrencyText();
    }
    public void SubtractCurrency(Team team, int amount)
    {
        currency[team] -= amount;
        UpdateCurrencyText();
    }
    private void UpdateCurrencyText()
    {
        northCurrencyText.text = currency[Team.North].ToString();
        southCurrencyText.text = currency[Team.South].ToString();
    }
    public void SetGameOver(bool gameOver, Team team)
    {
        isGameOver = gameOver;
        winningTeam = team == Team.North ? Team.North : Team.South;
        gameUI.gameObject.SetActive(isGameOver);
        isGameRunning = false;
    }
}
