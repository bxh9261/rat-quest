/*
    Programmer: Durrell Bedassie [DB]
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    Player m_player;
    float m_damage = 20.0f;
    float m_enemyMaxHP = 100f;
    float m_enemyCurrentHP;

    public Slider enemyHealthbar;

    // Start is called before the first frame update
    void Start()
    {
        //m_player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        m_enemyCurrentHP = m_enemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthbar.value = m_enemyCurrentHP / m_enemyMaxHP;
    }

    /// <summary>
    /// Purpose: Deal sword's damage to the enemy.
    /// Params: (NOT IMPLEMENTED YET) a_enemy = current enemy
    /// Programmer(s): [DB]
    /// </summary>
    void DealDamage()
    {
        Debug.Log(m_damage + " damage dealt to enemy.");
        m_enemyCurrentHP -= m_damage;
    }

    /// <summary>
    /// When sword is clicked
    /// [DB]
    /// </summary>
    private void OnMouseDown()
    {
        // Deal damage to enemy
        DealDamage();
    }
}
