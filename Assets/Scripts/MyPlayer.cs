using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyPlayer : MonoBehaviour
{
    [Tooltip("VFX prefab generating after destruction")]
    public GameObject hitEffect;
    public GameObject destructionFX;

    public int playerHealth = 3;




    public static MyPlayer instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //method for damage proceccing by 'Player'
    public void GetDamage(int damage)
    {
        playerHealth -= damage;           //reducing health for damage value, if health is less than 0, starting destruction procedure
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


}
