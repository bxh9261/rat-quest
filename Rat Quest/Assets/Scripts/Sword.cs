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

    SceneManager sceneM;
    ScoreManager scoreM;
    InventoryManager invM;

    AudioSource[] hitnkill;

    // Start is called before the first frame update
    void Start()
    {
        invM = GameObject.FindGameObjectsWithTag("Inventory")[0].GetComponent<InventoryManager>();
        hitnkill = GetComponents<AudioSource>();
        sceneM = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        scoreM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //the sword damage to the enemy is equal to its strength plus 10 (since if you have no sword you do 10 by default)
        m_damage = Mathf.Floor(invM.Strength.Value) + 10;
    }


    /// <summary>
    /// When sword is clicked
    /// [DB]
    /// </summary>
    private void OnMouseDown()
    {
        // Deal damage to enemy
        sceneM.DealDamageToEnemy(m_damage);
        if (!sceneM.respawning && sceneM.getCurrentEnemyHealth() > 0)
        {
            hitnkill[0].Play();
            scoreM.AddScore("swordHit");
        }
        else if (!sceneM.respawning)
        {
            hitnkill[1].Play();
        }
    }
}
