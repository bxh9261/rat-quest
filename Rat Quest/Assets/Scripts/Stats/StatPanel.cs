using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code used from Kryzarel's character stats tutorial
//Link: https://www.youtube.com/watch?v=SH25f3cXBVc&list=PLm7W8dbdflojT-OqfBJvqK6L9LRwKmymz

public class StatPanel : MonoBehaviour
{
    //Array for the stats to show
    [SerializeField]
    StatDisplay[] statDisplay;

    //Array for the stats names
    [SerializeField]
    string[] statNames;

    //Array for the actual stats
    private CharacterStat[] stats;

    private void OnValidate()
    {
        statDisplay = GetComponentsInChildren<StatDisplay>();
        UpdateStatNames();
    }

    //Read in the stat variables and set them
    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        //SAFE KEEPING

        //If we have more stats then we have displays
        if(stats.Length > statDisplay.Length)
        {
            //Lets us know!
            Debug.LogError("Not enough stat displays! Add more to the Stats Panel!");
            return;
        }

        for (int i = 0; i < statDisplay.Length; i++)
        {
            //Set the displays to being active if there is a stat,
            //And as long as it doesn't surpass the amount of stats we have
            statDisplay[i].gameObject.SetActive(i < stats.Length);

            if(i < stats.Length)
            {
                statDisplay[i].Stat = stats[i];
            }
        }
    }

    public CharacterStat[] GetStats()
    {
        return stats;
    }

    //Update the values
    public void UpdateStatValues()
    {
        //Loop through the stats
        for (int i = 0; i < stats.Length; i++)
        {
            //Update the text values in them
            statDisplay[i].UpdateStatValue();
        }
    }


    //Update the names
    public void UpdateStatNames()
    {
        //Loop through the stats
        for (int i = 0; i < statNames.Length; i++)
        {
            //Update the text names in them
            statDisplay[i].Name = statNames[i];
        }
    }
}
