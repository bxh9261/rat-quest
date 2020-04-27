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
    public PEnemy[] m_enemies;
    public Player m_player;

    //keeps track of which enemy is currently alive (0 or 1 right now)
    int enemyNum;

    //health bars
    public Slider playerHealthbar;
    public Slider enemyHealthbar;

    //"Special Attack!"
    public GameObject specialUI;

    private IEnumerator coroutine;
    public bool respawning = false;

    public GameObject youDied;

    //how many you've defeated so far
    public int defeated;

    //hallway animation
    public GameObject hallway;

    //Score Manager
    ScoreManager scoreM;

    //respawn time decreases as time goes on
    float respawnTime;

    AudioSource[] aud;

    // Start is called before the first frame update
    void Start()
    {
        enemyNum = 0;
        respawnTime = 5.0f;
        defeated = 0;

        aud = GetComponents<AudioSource>();

        hallway = GameObject.Find("Hallway");
        specialUI = GameObject.Find("Money UI");
        youDied = GameObject.Find("you died lmao");

        m_player = Instantiate(m_player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        m_sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<Sword>();
        m_shield = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();

        //create enemies (currently only 2)
        for(int i = 0; i < m_enemies.Length; i++)
        {
            m_enemies[i] = Instantiate(m_enemies[i], new Vector3(-4.33f, 1.5f, -1.0f), Quaternion.identity);
            m_enemies[i].transform.localScale = new Vector3(0.3f, 0.3f, 1.0f);
            if(i != 0)
            {
                m_enemies[i].gameObject.active = false;
            }
        }
        

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
        if(m_enemies[enemyNum] != null)
        {
            //enemy respawner, here since enemy script will be deleted on enemy die
            if (m_enemies[enemyNum].EnemyHP <= 0 && !respawning)
            {
                SpawnItem();
                loot.SetActive(true);
                respawning = true;
                for(int i = 0; i < m_enemies.Length; i++)
                {
                    m_enemies[i].gameObject.active = false;
                }
                coroutine = WaitAndRespawn(respawnTime);
                StartCoroutine(coroutine);
                defeated += 1;
                scoreM.AddScore("ratKill");
                aud[0].Play();
            }
            enemyHealthbar.value = m_enemies[enemyNum].EnemyHP / m_enemies[enemyNum].EnemyMaxHP;
            if (m_enemies[enemyNum].EnemyHP < 0)
            {
                m_enemies[enemyNum].EnemyHP = 0;
            }

            

            hallway.GetComponent<Animator>().enabled = respawning;
        }

        //after defeating 5 enemies, game gets tougher
        if(defeated == 5)
        {
            for(int i = 0; i < m_enemies.Length; i++){
                m_enemies[i].EnemyMaxHP = m_enemies[i].EnemyMaxHP+50;
            }
            aud[2].pitch *= 1.05946f;
            defeated = 0;
            //gradually make enemies come faster but don't go under 1 second
            if(respawnTime > 1.0f)
            {
                respawnTime -= 0.5f;
            }
        }

    }

    // say "special attack!" on UI for 1 second
    public IEnumerator SpecialUI()
    {
        specialUI.GetComponent<Text>().text = "Special Attack!";
        yield return new WaitForSeconds(1.0f);
        specialUI.GetComponent<Text>().text = "";

    }

    // respawn after being dead 5 seconds
    private IEnumerator WaitAndRespawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        enemyNum = (int)Mathf.Floor(Random.Range(0, m_enemies.Length));
        m_enemies[enemyNum].gameObject.active = true;
        m_enemies[enemyNum].EnemyHP = m_enemies[enemyNum].EnemyMaxHP;
       
        respawning = false;
        aud[0].Pause();
        loot.SetActive(false);
        droppedItems.Clear();
    }

    public float getCurrentEnemyHealth()
    {
        return m_enemies[enemyNum].EnemyHP;
    }

    public bool DealDamageToPlayer(float dam)
    {
        m_player.TakeDamage(dam);
        playerHealthbar.value = m_player.getHealth() / 100.0f;
        aud[3].Play();
        return true;
    }

    public bool DealDamageToEnemy(float dam)
    {
        m_enemies[enemyNum].EnemyHP -= dam;
        return true;
    }

    public void restoreHealth(float val)
    {
        m_player.restoreHealth(val);
        playerHealthbar.value = m_player.getHealth() / 100.0f;
        aud[1].Play();
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

    public PEnemy ReturnCurrentEnemy()
    {
        return m_enemies[enemyNum];
    }
}
