using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //enemy health
    float m_enemyCurrentHP;

    //rat attack every 1 second -- https://answers.unity.com/questions/17131/execute-code-every-x-seconds-with-update.html
    public float attackPeriod;
    private float time;

    //max HP default
    float m_enemyMaxHP;

    //scene manager
    SceneManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

        time = 0.0f;
        m_enemyCurrentHP = 100.0f;

        //rat attack every 1 seconds
        attackPeriod = 1.0f;

        m_enemyMaxHP = 100.0f;

    }

    // Update is called once per frame
    void Update()
    {

        //code for attack every 1 second
        time += Time.deltaTime;

        if (time >= attackPeriod)
        {
            time = 0.0f;

            sm.DealDamageToPlayer(10.0f);
        }

    }

    //getter and setter for enemyhp
    public float EnemyHP
    {
        get
        {
            return this.m_enemyCurrentHP;
        }
        set
        {
            this.m_enemyCurrentHP = value;
        }
    }

    //getter and setter for enemyhp
    public float EnemyMaxHP
    {
        get
        {
            return this.m_enemyMaxHP;
        }
        set
        {
            this.m_enemyMaxHP = value;
        }
    }
}
