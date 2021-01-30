using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float health;
    
    public float strength;
    public Vector3 position;

    private float cooldown;
    public event System.Action onDie;

    public Ship()
    {
        health = 100;
        strength = 2;
        position = new Vector3(0, 0, 0);
        cooldown = 5;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    public void attack(Ship target)
    {
        if (cooldown <= 0)
        {
            // Do damage to other ship
            target.takeDamage(strength);
            cooldown = 5;
        }
    }

    private void die()
    {
        onDie();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }
}
