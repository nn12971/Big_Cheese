using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : Gun
{

    [Header("Operational Info")]
    public float rpm = 100f;
    private float fireRate;
    private float nextTimeToFire = 0f;


    // Start is called before the first frame update
    void Start()
    {
        base.Init();
        fireRate = 60 / rpm;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForFire();
        AimDownSights();
        ResetPosition();
        
    }

    protected override void CheckForFire()
    {
        if (Time.time >= nextTimeToFire)
        {
            canFire = true;
        } else
        {
            canFire = false;
        }
        if (input.fireDown && canFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

}
