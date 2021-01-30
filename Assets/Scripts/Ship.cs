using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float health;
    
    public float strength;
    public Vector3 position;
    public bool attacking;

    private int cooldown;
    

    public Ship()
    {
        health = 100;
        strength = 2;
        position = new Vector3(0, 0, 0);
        cooldown = 100;
        attacking = false;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }

    public void attack(Ship target)
    {
        if (cooldown <= 0)
        {
            // Do damage to other ship
            target.takeDamage(strength);
            attacking = true;
            cooldown = 100;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking == true)
        {
            cooldown -= 1;
        }
    }
}
