/*
    Programmer: Durrell Bedassie [DB] 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class Player : MonoBehaviour
{
    float m_health = 100.0f;
    bool m_block = false;
    public bool blockDebuff = false;    // is debuff active?
    public float debuffTimer;   // how long does debuff last
    float debuffDT; // delta time for debuff

    SceneManager sm;
    InventoryManager invM;

    private float time;
    public float blockingPeriod;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        invM = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<InventoryManager>();
        debuffDT = 0.0f;
        time = 0.0f;
        blockingPeriod = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //blocking period is the amount of seconds the player won't take damage. by default it is 1, but adding a shield item will increase this time based on its DEF stat
        if(invM.Defense.Value >= 1)
        {
            blockingPeriod = invM.Defense.Value;
        }
        else
        {
            blockingPeriod = 1.0f;
        }

        // Timer for blocking debuff from Enemy2
        if (blockDebuff)
        {
            debuffDT += Time.deltaTime;

            if (debuffDT >= debuffTimer)
            {
                blockDebuff = false;
            }
        }

        if (m_block)
        {
            time += 0.016f;
        }
        else
        {
            time = 0.0f;
        }
        

        if (time >= blockingPeriod)
        {
            time = 0.0f;
            m_block = false;
            Debug.Log("blocking period ended");
        }
    }

    /// <summary>
    /// Purpose: Call this method when player takes damage
    /// Params: a_damage = incoming damage
    /// Programmer(s): [DB]
    /// </summary>
    public void TakeDamage(float a_damage)
    {
        //if a player has more vitality they take less damage from a hit
        a_damage -= Mathf.Floor(invM.Vitality.Value);
        if(a_damage < 1)
        {
            a_damage = 1.0f;
        }

        // If the blocking debuff is active, the player takes 1/2 damage when blocking.
        if (blockDebuff)
        {
            if (m_block)
            {
                m_health -= (a_damage / 2);
            }
        }

        if (!m_block && sm.getCurrentEnemyHealth() > 0)
        {
            m_health -= a_damage;

        }
        // If player is dead...
        if (m_health <= 0)
        {
            Debug.Log("die");
            UnitySceneManager.LoadScene("Main Menu");
        }


    }

    public float getHealth()
    {
        return m_health;
    }

    public void block()
    {
        m_block = true;
    }

    public void restoreHealth(float val)
    {
        m_health += val;
        if (m_health > 100) m_health = 100.0f;
    }
}
