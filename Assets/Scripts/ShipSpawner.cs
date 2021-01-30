using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShipSpawner : MonoBehaviour
{
    [SerializeField]
    private int maxShipCount = 10;

    [SerializeField]
    private GameObject shipPrefab;

    [SerializeField]
    private float shipCheckSize = 4.5f;

    [SerializeField]
    private float spawnEvery = 5f;

    private int shipCount = 0;

    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        InvokeRepeating("SpawnShip", 0f, spawnEvery);
    }

    void SpawnShip()
	{
        if (shipCount >= maxShipCount)
		{
            return;
        }

        bool validSpawn = false;
        int checkCount = 0;
        Vector3 targetPos = Vector3.zero;

        while (!validSpawn && checkCount < 5)
        {
            int x = Mathf.RoundToInt(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x));
            int z = Mathf.RoundToInt(Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));

            targetPos = new Vector3(x, transform.position.y, z);

            bool foundShip = false;
            foreach (Collider collider in Physics.OverlapSphere(targetPos, shipCheckSize))
            {
                if (collider != boxCollider && true /* check for ship component here */)
                {
                    foundShip = true;
                }
            }

            if (!foundShip)
            {
                validSpawn = true;
            }

            checkCount++;
        }

        if (!validSpawn)
		{
            return;
		}

        GameObject ship = GameObject.Instantiate(shipPrefab);
        ship.transform.position = targetPos;

        shipCount++;
    }
}
