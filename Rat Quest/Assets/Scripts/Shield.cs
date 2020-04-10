/*
    Programmer: Durrell Bedassie [DB]
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Player m_player;
    float m_blockRating = 10.0f;
    AudioSource block;
    SceneManager sceneM;

    // Start is called before the first frame update
    void Start()
    {
        //m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        block = GetComponent<AudioSource>();
        sceneM = GameObject.Find("SceneManager").GetComponent<SceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Purpose: When shield is clicked
    /// Programmer(s): [DB]
    /// </summary>
    private void OnMouseDown()
    {
        if (!sceneM.respawning)
        {
            block.Play();
        }
        
        // Block incoming damage
        BlockDamage();
    }

    /// <summary>
    /// Purpose: Call this method when blocking incoming damage.
    /// Programmer(s): [DB]
    /// </summary>
    void BlockDamage()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_player.block();
        //Debug.Log(m_blockRating + " damage blocked!");
    }
}
