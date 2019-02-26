﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {

    //-----VARIABLES-----  

    //Characters name
    public string characterName;

    //Characters health
    public int maxHealth;
    private int health;

    //Characters mana
    public int maxMana;
    private int mana;

    //Characters action points
    public int maxActionPoints;
    private int actionPoints;

    //Characters experience
    private int currentLevel;
    private int currentExperience;
    private int nextLevelExperienceCap;

    //-----METHODS-----

    //Setup Method
    public void Initialise () {
        health = maxHealth;
        mana = maxMana;
        actionPoints = maxActionPoints;
    }

    //Return the current amount of the specified resource
    public int GetResourceOfType (ResourceType type) {
        switch (type) {
            case ResourceType.HEALTH:
                return health;
            case ResourceType.MANA:
                return mana;
            case ResourceType.ACTIONPOINTS:
                return actionPoints;
            case ResourceType.EXPERIENCE:
                return currentExperience;
            default:
                Debug.LogError("Attempting to get a resource that doesn't exist");
                return 0;
        }
    }
    
    //Reduce the specified resource by the specified amount in the cost
    public void ApplyChangeToData (StatChange change) {
        switch (change.Resource) {
            case ResourceType.HEALTH:
                health = (int) Mathf.Clamp(health + change.Amount, 0, maxHealth);
                if (health <= 0) {
                    Die();
                }
                break;
            case ResourceType.MANA:
                mana = (int) Mathf.Clamp(mana + change.Amount, 0, maxMana);                
                break;
            case ResourceType.ACTIONPOINTS:
                actionPoints = (int) Mathf.Clamp(actionPoints + change.Amount, 0, maxActionPoints);                
                break;
            case ResourceType.EXPERIENCE:

                break;
            default:
                Debug.LogError("Attempting to change a resource that doesn't exist");
                break;
        }

        //Update the UI to reflect the changes
        UIManager.instance.UpdateHeroStatusBar();
    }

    //Destroy the character object when their health reaches zero
    public void Die () {
        Debug.Log("Oh shit im dead");
        Destroy (gameObject, 2f);
    } 

    public int LevelToExperience (int level) {
        return Mathf.RoundToInt(Mathf.Pow(level, 1.5f) * 100);
    }

}

public enum ResourceType {HEALTH, MANA, ACTIONPOINTS, EXPERIENCE};

public struct StatChange {
    public ResourceType Resource;
    public int Amount;

    public StatChange (ResourceType _resource, int _amount) {
        Resource = _resource;
        Amount = _amount;
    }
}
