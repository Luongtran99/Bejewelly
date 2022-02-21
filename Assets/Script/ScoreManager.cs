﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{

    private Board board;
    public Text scoreText;
    public int score;
    public Image scoreBar;
    private GameData gameData;
    private int numberStar;
    // Use this for initialization
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        board = FindObjectOfType<Board>();
        UpdateBar();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "" + score;
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;

        for(int i = 0; i< board.scoreGoals.Length; i++)
        {
            if(score > board.scoreGoals[i] && numberStar < i + 1)
            {
                numberStar++;
            }
        }

        if (gameData != null)
        {
            int highScore = gameData.saveData.highScores[board.level];

            if (score > highScore)
            {
                gameData.saveData.highScores[board.level] = score;
            }

            int currentStar = gameData.saveData.stars[board.level];
            if(currentStar < numberStar)
            {
                gameData.saveData.stars[board.level] = numberStar;
            }

            gameData.Save();
        }
        UpdateBar();
    }


    private void UpdateBar()
    {
        if (board != null && scoreBar != null)
        {

            int length = board.scoreGoals.Length;

            scoreBar.fillAmount = (float)score / (float)board.scoreGoals[length - 1];


        }
    }
}
