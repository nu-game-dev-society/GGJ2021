using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCombat : MonoBehaviour
{
    private List<Fleet> enemyFleets;
    private Fleet playerFleet;

    public ShipCombat()
    {
        enemyFleets = new List<Fleet>();
    }

    public void attack(Ship attacker, Ship attacking)
    {
        // Do some particles for a cannon blast
        // TO DO
        // Do damage to other ship
        attacker.attack(attacking);
    }

    public bool withinRange(Vector3 pos1, Vector3 pos2)
    {
        float dist = Vector3.Distance(pos1, pos2);
        if (dist < 10)
        {
            return true;
        }
        return false;
    }

    public bool checkForEnemies()
    {
        for (int i = 0; i < enemyFleets.Count; i++)
        {
            if (withinRange(playerFleet.center, enemyFleets[i].center))
            {
                return true;
            }
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
