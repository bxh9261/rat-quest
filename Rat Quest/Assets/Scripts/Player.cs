/*
    Programmer: Durrell Bedassie [DB] 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float m_health = 100.0f;
    bool m_block = false;
    public bool blockDebuff = false;    // is debuff active?
    public float debuffTimer;   // how long does debuff last
    float debuffDT; // delta time for debuff

    SceneManager sm;

    private float time;
    public float blockingPeriod;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        debuffDT = 0.0f;
        time = 0.0f;
        blockingPeriod = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
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

            Debug.Log("took damage, blocking is " + m_block);

            // If player is dead...
            if (m_health <= 0)
            {
                Debug.Log("die");
            }
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
}
