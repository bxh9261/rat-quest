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

    SceneManager sm;
    

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SceneManager").GetComponent<SceneManager>();

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
        sm.DealDamageToEnemy(m_damage);
    }
}
