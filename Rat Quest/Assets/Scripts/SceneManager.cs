using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Scene Manager
 * Prorgrammer: Brad Hanel
 * Created: 3/3/20
 */ 

public class SceneManager : MonoBehaviour
{
    //sword and shield (on UI)
    Sword m_sword;
    Shield m_shield;

    //emeny and player (instantiated on start, player is invisible)
    public GameObject m_enemy;
    public Player m_player;

    //should probably be moved to Player at some point
    public Slider playerHealthbar;

    //we currently don't have an enemy script so this is stored here
    float m_enemyCurrentHP;

    //rat attack every 1 second -- https://answers.unity.com/questions/17131/execute-code-every-x-seconds-with-update.html
    public float attackPeriod;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        m_enemyCurrentHP = 100.0f;

        m_player = Instantiate(m_player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        m_sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        m_shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        m_enemy = Instantiate(m_enemy, new Vector3(-4.33f, 1.5f, -1.0f), Quaternion.identity);
        m_enemy.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);

        time = 0.0f;

        //rat attack every 1 seconds
        attackPeriod = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //if enemy is dead, enemy die
        if (m_enemyCurrentHP <= 0) 
        {
            Destroy(m_enemy);
        }


        //code for attack every 1 second
        time += Time.deltaTime;

        if (time >= attackPeriod)
        {
            time = 0.0f;

            m_player.TakeDamage(10.0f);
            playerHealthbar.value = m_player.getHealth() / 100.0f;
        }

        
    }

    //getter and setter for enemyhp, if we eventually make an enemy script this should probably go there
    public float EnemyHP
    {
        get
        {
            return this.m_enemyCurrentHP;
        }
        set
        {
            this.m_enemyCurrentHP = value;
        }
    }

}
