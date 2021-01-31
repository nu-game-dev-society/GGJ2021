using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public float strength;
    public Vector3 position;
    [Tooltip("Fine if null")]
    public Fleet myFleet;
    public ShipColour colour;
    public Animator animator;
    [HideInInspector] public ShipController controller;

    public ParticleSystem leftCannonParticles;
    public ParticleSystem rightCannonParticles;
    public ParticleSystem hitParticles;


    private float cooldown;
    public UnityEvent onDie;

    public AudioSource audioSource;
    public AudioClip fireClip;
    public AudioClip hitClip;
    public AnimationCurve falloff;
    public float maxDistance = 500f;

    public bool isPlayer;
    public bool isRowBoat; 

    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            if (audioSource.clip != hitClip || !audioSource.isPlaying)
            {
                CalculateFalloff();
                audioSource.clip = hitClip;
                audioSource.pitch = Random.Range(0.8f, 1.1f);
                audioSource.Play();
                hitParticles.Play();
            }
        }

        health -= damage;
        if (health <= 0)
            Die();
    }

    public void TakeDamage(float damage, Ship damageSource)
    {
        if (damage > 0)
        {
            if (audioSource.clip != hitClip || !audioSource.isPlaying)
            {
                CalculateFalloff();
                audioSource.clip = hitClip;
                audioSource.pitch = Random.Range(0.8f, 1.1f);
                audioSource.Play();
                hitParticles.Play();
            }
        }

        health -= damage;
        if (health <= 0)
            Die(damageSource.myFleet);
    }

    public void Die()
    {
        SetAnimTrigger("ShipSink");
        Debug.Log("Died: " + gameObject, gameObject);
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

        if (isRowBoat && newFleet.ships[0].isPlayer)
        {
            myFleet = newFleet;

            AIShipController aiShip = GetComponent<AIShipController>();
            if (aiShip)
                aiShip.enabled = false;

            StartCoroutine(RespawnAtTime(1.25f));
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void CreateFleet()
    {
        if (myFleet == null)
        {
            Fleet f = GetComponent<Fleet>();
            myFleet = f == null ? gameObject.AddComponent<Fleet>() : f;
            GameManager.instance.activeFleets.Add(myFleet);
            Debug.Log("Added new fleet");
        }
        if (!myFleet.ships.Contains(this))
        {
            myFleet.ships.Add(this);
            myFleet.liveShipsCount++;
        }
    }
    public void LeaveFleet()
    {
        myFleet.RemoveShip(this);
        if (myFleet.liveShipsCount <= 0)
        {
            if (colour)
                GameManager.instance.SetColourAvailable(colour.getShipColour());

            GameManager.instance.activeFleets.Remove(myFleet);
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

        SetAnimTrigger("ShipSpawn");
        health = maxHealth;
    }

    void Attack(Ship target, bool leftFire)
    {
        if (strength > 0)
        {
            if (cooldown <= 0)
            {
                Debug.Log(gameObject + " Attacked " + target.gameObject, target.gameObject);
                // Do damage to other ship
                target.TakeDamage(strength, this);
                if (leftFire)
                {
                    if (leftCannonParticles)
                        leftCannonParticles.Play();
                    SetAnimTrigger("LeftFire");
                }
                else
                {
                    if (rightCannonParticles)
                        rightCannonParticles.Play();
                    SetAnimTrigger("RightFire");
                }

                if (audioSource.clip != fireClip || !audioSource.isPlaying)
                {
                    CalculateFalloff();
                    audioSource.clip = fireClip;
                    audioSource.pitch = Random.Range(0.8f, 1.1f);
                    audioSource.Play();
                }

                cooldown = 5;
            }
        }
    }

    void CalculateFalloff()
    {
        float distance = Vector3.Distance(GameManager.instance.player.position, transform.position);

        float distanceVal = (distance / maxDistance);

        audioSource.volume = 0.3f * falloff.Evaluate(distanceVal);

    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ShipController>();
        CreateFleet();
        colour = GetComponentInChildren<ShipColour>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (myFleet != null)
        {
            if (cooldown < 0 && myFleet.targetFleets.Count > 0)
            {
                Ship target = null;
                float distance = 1000.0f;
                float angle = 0.0f;
                for (int i = 0; i < myFleet.targetFleets.Count; i++)
                {
                    if (myFleet.targetFleets[i] == null)
                    {
                        myFleet.targetFleets.RemoveAt(i);
                        continue;
                    }

                    for (int j = myFleet.targetFleets[i].liveShipsCount > 1 ? 1 : 0; j < myFleet.targetFleets[i].ships.Count; j++)
                    {
                        Ship s = myFleet.targetFleets[i].ships[j];
                        if (s != null && Vector3.Distance(transform.position, s.transform.position) < distance)
                        {
                            angle = Vector3.Dot(transform.right, (s.transform.position - transform.position).normalized);
                            if (Mathf.Abs(angle) > 0.7f)
                                target = s;
                        }
                    }

                }
                if (target != null)
                    Attack(target, angle < 0);
            }
        }
        if (cooldown < -8)
        {
            health = Mathf.Clamp(health + (Time.deltaTime * 2), 0, maxHealth);
        }
    }

    IEnumerator RespawnAtTime(float timeToJoin)
    {
        yield return new WaitForSeconds(timeToJoin);
        Respawn();
    }
    public void SetAnimTrigger(string trigger)
    {
        if (animator == null)
            return;
        animator.SetTrigger(trigger);
    }

}
