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
    public Enemy m_enemy;
    public Player m_player;

    //health bars
    public Slider playerHealthbar;
    public Slider enemyHealthbar;

    //list of items
    public List<GameObject> items;

    private IEnumerator coroutine;
    bool respawning = false;

    public GameObject youDied;

    // Start is called before the first frame update
    void Start()
    {
        youDied = GameObject.Find("you died lmao");

        m_player = Instantiate(m_player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        m_sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        m_shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        m_enemy = Instantiate(m_enemy, new Vector3(-4.33f, 1.5f, -1.0f), Quaternion.identity);
        m_enemy.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);

        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], new Vector3(1.3f + (1.7f * i), 2.7f, -0.5f), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(m_enemy != null)
        {
            //enemy respawner, here since enemy script will be deleted on enemy die
            if (m_enemy.EnemyHP <= 0 && !respawning)
            {
                respawning = true;
                m_enemy.gameObject.active = false;
                coroutine = WaitAndRespawn(5.0f);
                StartCoroutine(coroutine);
                Debug.Log("this should run ONCE");
            }
            enemyHealthbar.value = m_enemy.EnemyHP / m_enemy.EnemyMaxHP;
        }

    }



    // respawn after being dead 5 seconds
    private IEnumerator WaitAndRespawn(float waitTime)
    {

            yield return new WaitForSeconds(waitTime);
            m_enemy.gameObject.active = true;
            m_enemy.EnemyHP = 100.0f;
            respawning = false;
            Debug.Log("this should run ONCE, five seconds after the first one");

    }

    public float getCurrentEnemyHealth()
    {
        return m_enemy.EnemyHP;
    }

    public bool DealDamageToPlayer(float dam)
    {
        m_player.TakeDamage(dam);
        playerHealthbar.value = m_player.getHealth() / 100.0f;
        return true;
    }

    public bool DealDamageToEnemy(float dam)
    {
        Debug.Log(dam + " damage dealt to enemy.");
        m_enemy.EnemyHP -= dam;
        return true;
    }

}
