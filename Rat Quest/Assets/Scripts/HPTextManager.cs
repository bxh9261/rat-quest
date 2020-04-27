using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTextManager : MonoBehaviour
{

    GameObject sceneManager;
    Text playerHPText;
    Text enemyHPText;
    Text enemyNameText;

    bool deadEnemy;
    bool deadPlayer;

    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
        playerHPText = GameObject.Find("PlayerHP").GetComponent<Text>();
        enemyHPText = GameObject.Find("EnemyHP").GetComponent<Text>();
        enemyNameText = GameObject.Find("EnemyName").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();
        UpdateEnemy();
    }

    void UpdateEnemy()
    {
        float maxHP = sceneManager.GetComponent<SceneManager>().ReturnCurrentEnemy().EnemyMaxHP;
        float currentHP = sceneManager.GetComponent<SceneManager>().ReturnCurrentEnemy().EnemyHP;

        string enemyHPString = currentHP + "/" + maxHP;

        if (deadEnemy == true && currentHP > 0)
        {
            deadEnemy = false;
            enemyHPText.text = enemyHPString;
        }

        if (deadEnemy == false)
        {
            enemyHPText.text = enemyHPString;

            if (currentHP == 0)
            {
                deadEnemy = true;
            }
        }

        //Currently all enemies are just rats so no name changes
    }

    void UpdatePlayer()
    {
        float maxHP = 100; //Currently no variable in player storing maxHP, only current
        float currentHP = sceneManager.GetComponent<SceneManager>().m_player.getHealth();

        string playerHPString = currentHP + "/" + maxHP;

        if (deadPlayer == false)
        {
            playerHPText.text = playerHPString;

            if (currentHP <= 0)
            {
                playerHPText.text = "0/100";
                deadPlayer = true;
            }
        }
    }

}
