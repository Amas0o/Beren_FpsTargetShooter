using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Variables : MonoBehaviour
{
    //makke them const and start with uppercase
    //Target
    public const float targetHealth = 50;                
    public const int  targetProspectiveScore = 100;                 
    public const float targetDeathTime = 2;


    //Barrel
    public const float barrelHealth = 50;
    public const float barrelDeathTime = 4;  
    public const float barrelRadius = 4;
    public const float barrelDamage = 50;
    public const int barrelProspectiveScore = 200;

    //Bomb
    public const float bombHealth = 50;              
    public const float bombDeathTime = 2;          
    public const int bombProspectiveScore = -100;

    //Upgrade
    public const float upgradeHealth = 50;                    
    public const float upgradeDeathTime = 4;

    //WheelSpin
    public const float wheelSpeed = 500;                
    public const float wheelDeathTime = 10;            
    public const float wheelBrakeSpeed = 25;


    //bonusLerp
    public const float bonusLerpDuration = 0.5f;

    //scoreLerp
    public const float scoreLerpDuration = 0.7f;            
    public const float scoreLerpDistance = 0.02f;

    //wheelBonusLerp
    public const float wheelBonusLerpDuration = 0.5f;


    //Bullet
    public const float bulletDamage = 10;
    public const float bulletDeathTime = 2;


    //GameManager
    public const float frenzyTime = 2;


    //Guns

    //pistol
    public const float pistolShootForce = 600;
    public const float pistolUpwardForce = 0;
    public const float pistolTimeBetweenShooting = 0.1f;
    public const float pistolSpread = 0;
    public const float pistolReloadTime = 1.5f;
    public const float pistolTimeBetweenShots = 0;
    public const int pistolMagazineSize = 100;
    public const int pistolBulletsPerTap = 1;

    //shotgun
    public const float shotgunShootForce = 400;
    public const float shotgunUpwardForce = 0;
    public const float shotgunTimeBetweenShooting = 0.1f;
    public const float shotgunSpread = 2;
    public const float shotgunReloadTime = 1.5f;
    public const float shotgunTimeBetweenShots = 0;
    public const int shotgunMagazineSize = 100;
    public const int shotgunBulletsPerTap = 3;

    //assault
    public const float assaultShootForce = 800;
    public const float assaultUpwardForce = 0;
    public const float assaultTimeBetweenShooting = 0.1f;
    public const float assaultSpread = 0;
    public const float assaultReloadTime = 1.5f;
    public const float assaultTimeBetweenShots = 0;
    public const int assaultMagazineSize = 100;
    public const int assaultBulletsPerTap = 1;



    //Mouse Movement
    public const float mouseSensitivity = 500;


    //Player Controller
    public const float playerSpeed = 5;
    public const float playerSprintSpeed = 8;
    public const float playerJumpForce = 4;
    public const float playerGravity = 10;

    //Object Pooler
    public const int poolNum = 6;

    public const string pool1Tag = "Bullet";
    public const int pool1Size = 20;

    public const string pool2Tag = "Target";
    public const int pool2Size = 5;

    public const string pool3Tag = "Bomb";
    public const int pool3Size = 3;

    public const string pool4Tag = "Upgrade";
    public const int pool4Size = 3;

    public const string pool5Tag = "Barrel";
    public const int pool5Size = 3;

    public const string pool6Tag = "WheelOfFortune";
    public const int pool6Size = 4;



}
