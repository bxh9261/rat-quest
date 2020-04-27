using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : PEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        specialUI = GameObject.Find("Money UI");
        enemyMaxHP = 100.0f;
        enemyCurrentHP = enemyMaxHP;
        basicAttackDmg = 10.0f; // basic attack does 10 damage.
        specialAttackDmg = 30.0f;   // special attack does 30 damage.
        basicTime = 0.0f;   
        specialTime = 0.0f; 
        bAtkTimer = 1.0f;   // basic attacks every 1 second.
        sAtkTimer = 3.0f;   // special attacks every 3.1 seconds.
    }

    // Update is called once per frame
    void Update()
    {
        basicTime += Time.deltaTime;
        specialTime += Time.deltaTime;

        BasicAttack();
        SpecialAttack();
    }
}
