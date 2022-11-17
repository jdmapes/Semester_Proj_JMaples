using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Game game;
    public int maxHealth = 10;
    public int health;

    private void Start()
    {
        health = maxHealth;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) {
            Damage(100);
        }
    }
    public void Damage(int amount)
    {
        if (amount < 0) amount = 0;
        health -= amount;

        if(health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount < 0) amount = 0;
        health += amount;

        if (health > maxHealth) health = maxHealth;
    }

    public void Die()
    {
        Destroy(gameObject);
        // Notify game that we have died
        game.SendEvent("death", new object[] { this });
    }

}
