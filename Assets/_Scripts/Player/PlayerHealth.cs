/*  Author: Salick Talhah; Edited by Tyler McMillan, Joseph Malibiran
 *  Date Created: February 14, 2021
 *  Last Updated: March 13, 2021
 *  Description: This script is used to control the damage and amount of health, load the game over screen and check collision with hazard.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject gameover;
    [SerializeField] private GameObject win;
    public int maxhealth = 100;
    public int currentHealth = 100;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        if (currentHealth > maxhealth) {
            currentHealth = maxhealth;
        }
        else if (currentHealth < 0) {
            currentHealth = 0;
        }

        //Note: The lines below are commented out because it will interfere in the scenario where we will be loading a health amount from a save file.
        //currentHealth = maxhealth;
        //healthBar.SetMaxHealth(maxhealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentHealth <= 0)
        {
           // FindObjectOfType<SoundManager>().Play("dead");
            GameOver();
            gameover.SetActive(true);
        }
    }
   public void TakeDamage(int damage)
   {
        if ( (currentHealth - damage) < 0) {
            currentHealth = 0;
        }
        else {
            currentHealth -= damage;
        }
        
        healthBar.SetHealth(currentHealth);
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hazard"))
        {
            TakeDamage(20);
        }
        if(other.gameObject.CompareTag("WinScene"))
        {
            win.SetActive(true);
        }
        if(other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<SoundManager>().Play("Attacked");
            TakeDamage(5);
        }
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2.0f);
        //  gameover.SetActive(true);
    }

    //Hopefully you dont mind salick but im writing this add health script, you can do as you wish with it.
    public void AddHealth(int _amount){ //add amount of health from inventory screen when seed button is pressed
        if(currentHealth < maxhealth){  //check if health is under max health
            currentHealth += _amount; //add amount to current health
            if(currentHealth > maxhealth){ //if health is greater then the max allowed set it to max allowed.
                currentHealth = maxhealth;
            }
        }
        healthBar.SetHealth(currentHealth);
    }
    
    //Note: Used in loading/saving game
    public void SetHealth(int _amount) {
        if (_amount > maxhealth) {
            currentHealth = maxhealth;
        }
        else if (_amount < 0) {
            currentHealth = 0;
        }
        else {
            currentHealth = _amount;
        }
        healthBar.SetHealth(currentHealth);
    }

}
