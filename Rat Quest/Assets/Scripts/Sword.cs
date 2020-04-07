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
    

    // Start is called before the first frame update
    void Start()
    {
        sceneM = GameObject.Find("SceneManager").GetComponent<SceneManager>();
        scoreM = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /// <summary>
    /// When sword is clicked
    /// [DB]
    /// </summary>
    private void OnMouseDown()
    {
        // Deal damage to enemy
        sceneM.DealDamageToEnemy(m_damage);
        scoreM.AddScore("swordHit");
    }
}
