using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //bullet 
    [SerializeField] GameObject bullet;
    
    //bullet force
    [SerializeField] float shootForce, upwardForce;

    //Gun stats
    [SerializeField] float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] int magazineSize, bulletsPerTap;
    [SerializeField] bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;
    bool laserActive = false;

    //Reference
    [SerializeField] Camera fpsCam;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject bulletPoint;
    [SerializeField] GameObject laserPoint;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject crosshair;
    [SerializeField] TextMeshProUGUI ammunitionDisplay;

    //bug fixing :D
    public bool allowInvoke = true;

    //flash
    public ParticleSystem muzzleFlash;
    AudioSource shootingSound;

    //Object pooler
    ObjectPooler pooler;
    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        pooler = ObjectPooler.instance;
    }

    private void Start() // Initializes shootingSound with the audioSource and disables the laser
    {
        shootingSound = GetComponent<AudioSource>();
        laser.SetActive(false);
    }
    private void Update()
    {
        // toggles laser if right mouse button pressed and changes the bullet spawn point accordingly
        if (Input.GetKeyDown(KeyCode.Mouse1) && !laserActive)
        {
            attackPoint.position = laserPoint.transform.position;
            laserActive = true;
            laser.SetActive(true);
            crosshair.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && laserActive)
        {
            attackPoint.position = bulletPoint.transform.position;
            laserActive = false;
            laser.SetActive(false);
            crosshair.SetActive(true);
        }

        MyInput();

        //Set ammo display, if it exists
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        readyToShoot = false;
        shootingSound.Play();

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = attackPoint.position;

        //Calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        //GameObject currentBullet = Instantiate(bullet, attackPoint.position, attackPoint.rotation); //store instantiated bullet in currentBullet
        GameObject currentBullet = pooler.SpawnFromPool("Bullet" , attackPoint.position, attackPoint.rotation);
        //Rotate bullet to shoot direction

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward * shootForce, ForceMode.Impulse);//add to bullet script


        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player (should only be called once)
            //playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void EnableFrenzy()
    {
        timeBetweenShooting /= 2;
    }

    public void DisableFrenzy()
    {
        timeBetweenShooting *= 2;
    }

    public void TurnLaserOff()
    {
        attackPoint.position = bulletPoint.transform.position;
        laserActive = false;
        laser.SetActive(false);
        crosshair.SetActive(true);
    }
}
