using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour
{    
    public delegate void OnPlayerHealthChangedDelegate();
    public OnPlayerHealthChangedDelegate onPlayerHealthChangedCallback;

    [Tooltip("VFX prefab generating after destruction")]
    public GameObject hitEffect;
    public GameObject destructionFX;

    public int playerHealth = 3; //initialHalth of the ship
    public int shipHealth = 3; //shouldb especified by ship TODO 
    public int bonusHealth = 3; //bonus health which can be got during game
    public int maxPlayerHealth; //sum of initial health and bonus heatlh



    #region Sigleton
    public static MyPlayer instance;
    public static MyPlayer Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MyPlayer>();
            return instance;
        }
    }
    #endregion


    private void Awake()
    {
        SetPlayerHealth(shipHealth);
    }

    private void Start()
    {
        maxPlayerHealth = playerHealth + bonusHealth;
    }
    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        playerHealth -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure

        ClampHealth();

        if (playerHealth <= 0)
            Destruction();
        else
            Instantiate(hitEffect, transform.position, Quaternion.identity, transform);

    }

    //'Player's' destruction procedure
    void Destruction()
    {
        Instantiate(destructionFX, transform.position, Quaternion.identity); //generating destruction visual effect and destroying the 'Player' object
        Destroy(gameObject);
    }

    //method to change palyerHealth accordingly to a ship type
    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
    }

    //Method to Adjust your health value
    void ClampHealth()
    {
        playerHealth = Mathf.Clamp(playerHealth, 0, shipHealth);

        if (onPlayerHealthChangedCallback != null)
            onPlayerHealthChangedCallback.Invoke();
    }

    //Method to heal

    public void Heal(int health)
    {
        this.playerHealth += health;
        ClampHealth();
    }
    //Method to increase total Health
    public void AddHealth()
    {
        if (shipHealth < maxPlayerHealth)
        {
            shipHealth += 1;
            playerHealth = shipHealth;

            if (onPlayerHealthChangedCallback != null)
                onPlayerHealthChangedCallback.Invoke();
        }
    }
}
