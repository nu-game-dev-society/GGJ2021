using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float health;

    public float strength;
    public Vector3 position;
    public bool attacking;
    [Tooltip("Fine if null")]
    public Fleet myFleet;
    public Animator animator;
    [HideInInspector] public ShipController controller;

    private float cooldown;
    public event System.Action onDie;

    public Ship()
    {
        health = 100;
        strength = 2;
        position = new Vector3(0, 0, 0);
        cooldown = 5;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public void TakeDamage(float damage, Ship damageSource)
    {
        health -= damage;
        if (health <= 0)
            Die(damageSource.myFleet);
    }

    public void Die()
    {
        animator.SetBool("IsDead", true);
        LeaveFleet();
        onDie.Invoke();
    }

#if UNITY_EDITOR
    public Fleet toJoinEditorTest;
    [ContextMenu("Die")]
    public void DieEditor()
    {
        Die(toJoinEditorTest);
    }
#endif

    public void Die(Fleet newFleet)
    {
        Die();
        myFleet = newFleet;
        StartCoroutine(RespawnAtTime(1.25f));
    }
    void CreateFleet()
    {
        if (myFleet == null)
        {
            myFleet = gameObject.AddComponent<Fleet>();
            FleetManager.activeFleets.Add(myFleet);
        }
        if (!myFleet.ships.Contains(this))
            myFleet.ships.Add(this);
    }
    void LeaveFleet()
    {
        myFleet.removeShip(this);
        if (myFleet.ships.Count <= 0)
        {
            FleetManager.activeFleets.Remove(myFleet);
            Destroy(myFleet);
        }
    }

    public void Respawn()
    {
        if (myFleet == null)
        { Destroy(gameObject); return; }

        int index = myFleet.AddShip(this);

        Vector3 newPos = ShipTargetPositionLocator.GetShipTargetPosition(index + 1);

        Transform t = new GameObject().transform;
        t.parent = myFleet.transform;
        t.localPosition = newPos;
        controller.target = t;

        transform.position = myFleet.transform.TransformPoint(newPos);

        animator.SetBool("IsDead", false);
    }

    void Attack(Ship target)
    {
        if (cooldown <= 0)
        {
            Debug.Log(gameObject + " Attacked " + target.gameObject, target.gameObject);
            // Do damage to other ship
            target.TakeDamage(strength, this);
            cooldown = 5;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ShipController>();
        CreateFleet();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if (cooldown < 0 && myFleet.targetFleets.Count > 0)
        {
            Ship target = null;
            float distance = 1000.0f;
            for (int i = 0; i < myFleet.targetFleets.Count; i++)
            {
                foreach (Ship s in myFleet.targetFleets[i].ships)
                {
                    if (Vector3.Distance(transform.position, s.transform.position) < distance)
                    {
                        if (Mathf.Abs(Vector3.Dot(transform.right, (s.transform.position - transform.position).normalized)) > 0.2)
                            target = s;
                    }
                }
            }
            if (target != null)
                Attack(target);
        }
    }

    IEnumerator RespawnAtTime(float timeToJoin)
    {
        yield return new WaitForSeconds(timeToJoin);
        Respawn();
    }
}
