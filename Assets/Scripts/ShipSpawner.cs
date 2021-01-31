using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[RequireComponent(typeof(BoxCollider))]
public class ShipSpawner : MonoBehaviour
{
    [SerializeField]
    private int maxShipCount = 10;

    [SerializeField]
    private GameObject shipPrefab;

    [SerializeField]
    private GameObject rowBoatPrefab;

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
        if (GameManager.instance.activeFleets.Count >= maxShipCount)
        {
            return;
        }

        Vector3 targetPos = Vector3.zero;
        
        int x = Mathf.RoundToInt(Random.Range(boxCollider.bounds.min.x, boxCollider.bounds.max.x));
        int z = Mathf.RoundToInt(Random.Range(boxCollider.bounds.min.z, boxCollider.bounds.max.z));

        targetPos = new Vector3(x, transform.position.y, z);

        List<Collider> collisions = Physics.OverlapSphere(targetPos, shipCheckSize).ToList();
        
        bool isOverlappingShip = collisions.Exists(collider => collider is CapsuleCollider
                                                && collider.gameObject.TryGetComponent<ShipController>(out _));
        if (isOverlappingShip)
        {
            return;
        }

        float r = Random.Range(0, 100);
        
        GameObject ship = GameObject.Instantiate(r < 30 ? rowBoatPrefab : shipPrefab);
        ship.transform.position = targetPos;

        shipCount++;
    }
}
