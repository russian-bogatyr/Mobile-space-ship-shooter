using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyGuns
{
    public GameObject rightGun, leftGun, centralGun;
    [HideInInspector] public ParticleSystem leftGunVFX, rightGunVFX, centralGunVFX;
}
public class MyPlayerShooting : MonoBehaviour
{

    [Tooltip("shooting frequency. the higher the more frequent")]
    public float fireRate;

    [Tooltip("projectile prefab")]
    public GameObject projectileObject;

    //time for a new shot
    [HideInInspector] public float nextFire;


    [Tooltip("current weapon power")]
    [Range(1, 4)]       //change it if you wish
    public int weaponPower = 1;

    public Guns guns;
    bool shootingIsActive = true;
    [HideInInspector] public int maxweaponPower = 4;
    public static MyPlayerShooting instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        //receiving shooting visual effects components
        guns.leftGunVFX = guns.leftGun.GetComponent<ParticleSystem>();
        guns.rightGunVFX = guns.rightGun.GetComponent<ParticleSystem>();
        guns.centralGunVFX = guns.centralGun.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (shootingIsActive)
        {
            if (Time.time > nextFire)
            {
                MakeAShot();
                nextFire = Time.time + 1 / fireRate;
            }
        }
    }

    //method for a shot
    void MakeAShot()
    {
        switch (weaponPower) // according to weapon power 'pooling' the defined anount of projectiles, on the defined position, in the defined rotation
        {
            case 1:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position);
                guns.centralGunVFX.Play();
                break;
            case 2:
                CreateLazerShot(projectileObject, guns.rightGun.transform.position);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position);
                guns.rightGunVFX.Play();
                break;
            case 3:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position);
                guns.rightGunVFX.Play();
                break;
            case 4:
                CreateLazerShot(projectileObject, guns.centralGun.transform.position);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position);
                guns.leftGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position);
                guns.rightGunVFX.Play();
                CreateLazerShot(projectileObject, guns.leftGun.transform.position);
                CreateLazerShot(projectileObject, guns.rightGun.transform.position);
                break;
        }
    }

    void CreateLazerShot(GameObject lazer, Vector3 pos) //translating 'pooled' lazer shot to the defined position in the defined rotation
    {
        Instantiate(lazer, pos, projectileObject.transform.rotation);
    }
}
