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
    float m_damage = 10.0f;
    float m_enemyMaxHP = 100f;

    SceneManager sm;

    public Slider enemyHealthbar;
    

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        //m_player = GameObject.FindGameObjectWithTag("player").GetComponent<Player>();
        sm.EnemyHP = m_enemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthbar.value = sm.EnemyHP / m_enemyMaxHP;
    }

    /// <summary>
    /// Purpose: Deal sword's damage to the enemy.
    /// Params: (NOT IMPLEMENTED YET) a_enemy = current enemy
    /// Programmer(s): [DB]
    /// </summary>
    void DealDamage()
    {
        Debug.Log(m_damage + " damage dealt to enemy.");
        sm.EnemyHP -= m_damage;
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
