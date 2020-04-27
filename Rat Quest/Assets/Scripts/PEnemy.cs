using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Programmer: Durrell Bedassie
    Date: 03/03/2020
    Usage: Parent class for Enemy types
*/

public class PEnemy : MonoBehaviour
{
    #region Variables
    //rat attack every 1 second -- https://answers.unity.com/questions/17131/execute-code-every-x-seconds-with-update.html

    protected float enemyCurrentHP;   // current HP
    protected float enemyMaxHP;  // max HP
    protected float basicAttackDmg; // basic attack damage
    protected float specialAttackDmg;   // special attack damage
    protected float basicTime;  // timer to track basic attack
    protected float specialTime; // timer to track special attack
    protected float bAtkTimer; // time enemy takes to basic attack
    protected float sAtkTimer; // time enemy takes to special attack



    //scene manager
    protected SceneManager sm;
    #endregion
    

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        basicTime = 0.0f;
        specialTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        basicTime += Time.deltaTime;
        specialTime += Time.deltaTime;
        
        BasicAttack();

        /*
            - I didn't put SpecialAttack() in the default Update() just in case some enemies don't have special attacks.
                - Durrell
        */
    }

    //getter and setter for enemyhp
    public float EnemyHP
    {
        get
        {
            return this.enemyCurrentHP;
        }
        set
        {
            this.enemyCurrentHP = value;
        }
    }

    //getter and setter for enemymaxhp
    public float EnemyMaxHP
    {
        get
        {
            return this.enemyMaxHP;
        }
        set
        {
            this.enemyMaxHP = value;
        }
    }

    public virtual void BasicAttack()
    {
        if (basicTime >= bAtkTimer)
        {
            sm.DealDamageToPlayer(basicAttackDmg);
            basicTime = 0.0f;

            Debug.Log("Enemy basic attacks for: " + basicAttackDmg);
        }
    }

    public virtual void SpecialAttack()
    {
        if (specialTime >= sAtkTimer)
        {
            sm.DealDamageToPlayer(specialAttackDmg);
            specialTime = 0.0f;

            Debug.Log("Enemy special attacks for: " + specialAttackDmg);
            StartCoroutine(sm.SpecialUI());
        }
    }


}
