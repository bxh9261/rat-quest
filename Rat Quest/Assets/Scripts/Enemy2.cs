using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Programmer: Durrell Bedassie
    Date: 04/13/2020
    Usage: Second Enemy class that inherits from PEnemy
*/

public class Enemy2 : PEnemy
{
    bool ability;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        enemyMaxHP = 150.0f;
        enemyCurrentHP = enemyMaxHP;
        basicAttackDmg = 15.0f; // basic attack does 10 damage.
        specialAttackDmg = 15.0f; // special attack does base damage if debuff is active.
        basicTime = 0.0f;
        specialTime = 0.0f;
        bAtkTimer = 1.2f;   // basic attacks every 1.2 second.
        sAtkTimer = 4.0f;   // special attacks every 4 seconds.
    }

    // Update is called once per frame
    void Update()
    {
        basicTime += Time.deltaTime;
        specialTime += Time.deltaTime;

        BasicAttack();
        SpecialAttack();
    }

    /*
        - My idea for enemies later is a special attack that doesn't do damage, but disables the character in some way:
            - Can't block for x seconds.
            - Can't use items for x seconds.
            - Reduce x stat by z for the remainder of the encounter.
     */

    /// <summary>
    /// This enemy will reduce the amount of damage the shield can block for 10 seconds
    /// If effect is already in play, then the special attack will just be another basic attack. 
    /// </summary>
    public override void SpecialAttack()
    {   
        // Check to see if timer is up.
        if (specialTime >= sAtkTimer)
        {
            // Check to see if debuff is active
            if (GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().blockDebuff)
            {
                // If active, deal damage to player.
                base.SpecialAttack();

                Debug.Log("Blocking debuff already active, dealt " + specialAttackDmg + " instead!");
            }
            // If not active, apply the debuff
            else
            {
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().blockDebuff = true;
                GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>().debuffTimer = 10.0f;

                Debug.Log("Blocking debuff applied!");
            }

            specialTime = 0.0f;
        }
    }
}
