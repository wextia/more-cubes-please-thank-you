﻿using UnityEngine;
using System.Collections;

public class UpgradesUI : MonoBehaviour
{
    [SerializeField]
    int upgradesShown = 2;
    [SerializeField]
    int spaceBetweenUpgrades = 400;
    [SerializeField]
    GameObject moreLifeUpgrade;
    [SerializeField]
    GameObject missileRateUpgrade;
    [SerializeField]
    GameObject shootThroughUpgrade;
    [SerializeField]
    GameObject moreBulletsUpgrade;
    [SerializeField]
    GameObject multishotUpgrade;

    GameObject[] upgradesRoulette;
    GameObject[] instancedUpgrades;

    static UpgradesUI upgradesUI;

    public static UpgradesUI instance
    {
        get
        {
            if (!upgradesUI)
            {
                upgradesUI = FindObjectOfType<UpgradesUI>();
                if (!upgradesUI)
                {
                    Debug.LogError("There needs to be one active UpgradesUI script on a GameObject in your scene.");
                }
                else
                {
                    upgradesUI.Init();
                }
            }

            return upgradesUI;
        }
    }

    void Init()
    {
        upgradesRoulette = new GameObject[]{
            moreLifeUpgrade,
            missileRateUpgrade,
            shootThroughUpgrade,
            moreBulletsUpgrade,
            multishotUpgrade
    };
        instancedUpgrades = new GameObject[upgradesShown];

    }

    static public void SetActive(bool value)
    {
        instance.gameObject.SetActive(value);
    }

    static public void ShowUpgrades()
    {
        int[] chosenOnes = new int[instance.upgradesShown];
        for (int i = 0; i < instance.upgradesShown; i++)
        {
            if (instance.instancedUpgrades[i] != null)
                Destroy(instance.instancedUpgrades[i]);
            int pick = Random.Range(0, instance.upgradesRoulette.Length);
            bool alreadyPicked = false;
            for (int j = 0; j < i; j++)
            {
                if (pick == chosenOnes[j])
                    alreadyPicked = true;
            }
            if (alreadyPicked)
                --i;
            else
            {
                chosenOnes[i] = pick;
                instance.instancedUpgrades[i] = Instantiate(instance.upgradesRoulette[pick],
                    new Vector3(
                        (i * (float)instance.spaceBetweenUpgrades) - ((((float)instance.upgradesShown - 1)) * ((float)instance.spaceBetweenUpgrades / 2)),
                        0, 0),
                    Quaternion.identity) as GameObject;
                instance.instancedUpgrades[i].transform.SetParent(instance.transform, false);
            }
        }
    }
}
