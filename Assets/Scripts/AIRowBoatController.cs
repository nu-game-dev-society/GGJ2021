using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRowBoatController : AIShipController
{
    public override void HeadAwayOrTowardsNearestFleet(Fleet fleet)
    {
        if (!fleet)
            return;

        if (ship.myFleet == null)
            return;

        shipController.target.position = transform.position + (3 * (fleet.center - transform.position).normalized);
    }
}
