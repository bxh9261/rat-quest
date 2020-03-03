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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Purpose: Call this method when player takes damage
    /// Params: a_damage = incoming damage
    /// Programmer(s): [DB]
    /// </summary>
    void TakeDamage(float a_damage)
    {
        m_health -= a_damage;

        // If player is dead...
        if (m_health <= 0)
        {
            
        }
    }
}
