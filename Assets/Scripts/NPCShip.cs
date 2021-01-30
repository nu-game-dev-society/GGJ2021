using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCShip : MonoBehaviour
{
    private enum ShipState { idle, combat, dead };
    private ShipState shipState;
    private Ship combatTarget;
    private Vector3 idleTargetPosition;
    public Ship ship;

    public NPCShip()
    {
        shipState = ShipState.idle;
    }

    private void onShipDie()
    {
        shipState = ShipState.dead;
    }

    public void engageWithEnemyShip(Ship target)
    {
        shipState = ShipState.combat;
        combatTarget = target;
    }

    public void doIdle()
    {
        shipState = ShipState.idle;
    }

    public void setIdlePosition(Vector3 targetPos)
    {
        idleTargetPosition = targetPos;
    }

    private void combatUpdate()
    {
        
        // Do path finding
        Debug.Log("combat path finding");
        // Fire cannons
    }

    private void idleUpdate()
    {
        // Set ship controller target position
    }

    // Start is called before the first frame update
    void Start()
    {
        ship.onDie.AddListener(onShipDie);
    }

    // Update is called once per frame
    void Update()
    {
        if (shipState == ShipState.combat)
        {
            combatUpdate();
        }
        if (shipState == ShipState.idle)
        {
            idleUpdate();
        }
    }
}
