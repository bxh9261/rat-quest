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
        float maxHP = sceneManager.GetComponent<SceneManager>().m_enemy.EnemyMaxHP;
        float currentHP = sceneManager.GetComponent<SceneManager>().m_enemy.EnemyHP;

        string enemyHPString = currentHP + "/" + maxHP;

        enemyHPText.text = enemyHPString;

        //Currently all enemies are just rats so no name changes
    }

    void UpdatePlayer()
    {
        float maxHP = 100; //Currently no variable in player storing maxHP, only current
        float currentHP = sceneManager.GetComponent<SceneManager>().m_player.getHealth();

        string playerHPString = currentHP + "/" + maxHP;

        playerHPText.text = playerHPString;
    }

}
