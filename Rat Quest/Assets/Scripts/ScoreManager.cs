using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    int score;
    int highscore;

    public Text scoreText;
    public Text highscoreText;

    //Ammount of points per action/kill
    [SerializeField][Range(0,500)] int ppRatKill = 100;
    [SerializeField][Range(0,500)] int ppItemBought = 100;
    [SerializeField][Range(0,100)] int ppSwordHit = 5;
    [SerializeField][Range(0,250)] int ppDeleteItem = 250;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Highscore();
        ScoreTexts();
    }

    void Highscore()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
        }
    }

    void ScoreTexts()
    {
        string scoreString = "Score: " + score;
        scoreText.text = scoreString;

        string highscoreString = "Highscore: " + highscore;
        highscoreText.text = highscoreString;
    }

    public void AddScore(int increase)
    {
        score += increase;
    }

    public void AddScore(string action) 
    {
        switch (action)
        {
            case "swordHit":
                AddScore(ppSwordHit);
                break;

            case "ratKill":
                AddScore(ppRatKill);
                break;

            case "itemBought":
                AddScore(ppItemBought);
                break;

            case "itemDeleted":
                AddScore(ppDeleteItem);
                break;

            default:
                break;
        }
    }
}
