using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Variables : MonoBehaviour
{
    //makke them const and start with uppercase
    //Target
    public static float targetHealth = 50;                
    public static int targetProspectiveScore = 100;                 
    public static float targetDeathTime = 2;


    //Barrel
    public static float barrelHealth = 50;
    public static float barrelDeathTime = 4;  
    public static float barrelRadius = 4;
    public static float barrelDamage = 50;
    public static int barrelProspectiveScore = 200;

    //Bomb
    public static float bombHealth = 50;              
    public static float bombDeathTime = 2;          
    public static int bombProspectiveScore = -100;

    //Upgrade
    public static float upgradeHealth = 50;                    
    public static float upgradeDeathTime = 4;

    //WheelSpin
    public static float wheelSpeed = 500;                
    public static float wheelDeathTime = 10;            
    public static float wheelBrakeSpeed = 25;


    //bonusLerp
    public static float bonusLerpDuration = 0.5f;

    //scoreLerp
    public static float scoreLerpDuration = 0.7f;            
    public static float scoreLerpDistance = 0.02f;

    //wheelBonusLerp
    public static float wheelBonusLerpDuration = 0.5f;


    //Bullet
    public static float bulletDamage = 10;
    public static float bulletDeathTime = 2;


    //GameManager
    public static float frenzyTime = 2;


    //Guns

    //pistol
    public static float pistolShootForce = 600;
    public static float pistolUpwardForce = 0;
    public static float pistolTimeBetweenShooting = 0.1f;
    public static float pistolSpread = 0;
    public static float pistolReloadTime = 1.5f;
    public static float pistolTimeBetweenShots = 0;
    public static int pistolMagazineSize = 100;
    public static int pistolBulletsPerTap = 1;

    //shotgun
    public static float shotgunShootForce = 400;
    public static float shotgunUpwardForce = 0;
    public static float shotgunTimeBetweenShooting = 0.1f;
    public static float shotgunSpread = 2;
    public static float shotgunReloadTime = 1.5f;
    public static float shotgunTimeBetweenShots = 0;
    public static int shotgunMagazineSize = 100;
    public static int shotgunBulletsPerTap = 3;

    //assault
    public static float assaultShootForce = 800;
    public static float assaultUpwardForce = 0;
    public static float assaultTimeBetweenShooting = 0.1f;
    public static float assaultSpread = 0;
    public static float assaultReloadTime = 1.5f;
    public static float assaultTimeBetweenShots = 0;
    public static int assaultMagazineSize = 100;
    public static int assaultBulletsPerTap = 1;



    //Mouse Movement
    public static float mouseSensitivity = 500;


    //Player Controller
    public static float playerSpeed = 5;
    public static float playerSprintSpeed = 8;
    public static float playerJumpForce = 4;
    public static float playerGravity = 10;

    //Object Pooler
    public static int poolNum = 6;

    public static string pool1Tag = "Bullet";
    public static int pool1Size = 20;

    public static string pool2Tag = "Target";
    public static int pool2Size = 3;

    public static string pool3Tag = "Bomb";
    public static int pool3Size = 1;

    public static string pool4Tag = "Upgrade";
    public static int pool4Size = 1;

    public static string pool5Tag = "Barrel";
    public static int pool5Size = 1;

    public static string pool6Tag = "WheelOfFortune";
    public static int pool6Size = 2;



}
