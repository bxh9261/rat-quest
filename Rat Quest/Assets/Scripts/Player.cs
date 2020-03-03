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

    SceneManager sm;

    private float time;
    public float blockingPeriod;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        time = 0.0f;
        blockingPeriod = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

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
        if (!m_block && sm.EnemyHP > 0)
        {
            m_health -= a_damage;

            Debug.Log("took damage, blocking is " + m_block);

            // If player is dead...
            if (m_health <= 0)
            {
                Debug.Log("die");
                sm.youDied.transform.position = new Vector3(-4.41f, -3.18f, 0.0f);
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
