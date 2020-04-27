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
    [SerializeField]
    GameObject loot;

    Inventory droppedItems;

    [SerializeField]
    ConsumableItem[] consumablePool;

    [SerializeField]
    EquippableItem[] normalEquipPool;

    [SerializeField]
    EquippableItem[] rareEquipPool;

    [Space]

    //sword and shield (on UI)
    Sword m_sword;
    Shield m_shield;

    //emeny and player (instantiated on start, player is invisible)
    public PEnemy m_enemy;
    public Player m_player;

    //health bars
    public Slider playerHealthbar;
    public Slider enemyHealthbar;

    private IEnumerator coroutine;
    public bool respawning = false;

    public GameObject youDied;

    public int money;
    public GameObject moneyUI;

    //hallway animation
    public GameObject hallway;

    //Score Manager
    ScoreManager scoreM;

    AudioSource footsteps;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
        moneyUI = GameObject.Find("Money UI");

        footsteps = GetComponent<AudioSource>();

        hallway = GameObject.Find("Hallway");

        youDied = GameObject.Find("you died lmao");

        m_player = Instantiate(m_player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        m_sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        m_shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
        m_enemy = Instantiate(m_enemy, new Vector3(-4.33f, 1.5f, -1.0f), Quaternion.identity);
        m_enemy.transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);

        scoreM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        /*
        for (int i = 0; i < items.Count; i++)
        {
            Instantiate(items[i], new Vector3(1.3f + (1.7f * i), 2.7f, -0.5f), Quaternion.identity);
        }
        */
        droppedItems = loot.GetComponent<Inventory>();
        loot.SetActive(false);
        droppedItems.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_enemy != null)
        {
            //enemy respawner, here since enemy script will be deleted on enemy die
            if (m_enemy.EnemyHP <= 0 && !respawning)
            {
                SpawnItem();
                loot.SetActive(true);
                respawning = true;
                m_enemy.gameObject.active = false;
                coroutine = WaitAndRespawn(5.0f);
                StartCoroutine(coroutine);
                Debug.Log("this should run ONCE");
                money += (int)m_enemy.EnemyMaxHP;
                scoreM.AddScore("ratKill");
                footsteps.Play();
            }
            enemyHealthbar.value = m_enemy.EnemyHP / m_enemy.EnemyMaxHP;
            moneyUI.GetComponent<Text>().text = "Money: " + money.ToString();

            hallway.GetComponent<Animator>().enabled = !m_enemy.gameObject.active;
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
            footsteps.Pause();
            loot.SetActive(false);
            droppedItems.Clear();
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

    public void restoreHealth(float val)
    {
        m_player.restoreHealth(val);
        playerHealthbar.value = m_player.getHealth() / 100.0f;
    }

    private void SpawnItem()
    {
        //Get 3 chances of having an item spawn
        for(int i = 0; i < 2; i++)
        {
            //Gives a 50% chance of spawning a item
            if (Random.Range(0.0f, 100.0f) <= 50.0f)
            {
                float rng = Random.Range(0.0f, 100.0f);

                //50% of a consumable item
                if (rng < 1000.0f)
                {
                    droppedItems.AddItem(consumablePool[Random.Range(0, consumablePool.Length)]);
                }
                //30% of a equippable item
                else if (rng < 80.0f)
                {
                    droppedItems.AddItem(normalEquipPool[Random.Range(0, normalEquipPool.Length)]);
                }
                //20% of a rare item
                else
                {
                    droppedItems.AddItem(rareEquipPool[Random.Range(0, rareEquipPool.Length)]);
                }
            }
        }
    }
}
