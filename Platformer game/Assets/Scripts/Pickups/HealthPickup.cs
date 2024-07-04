using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private AudioSource itemPickupSource;

    private void Awake()
    {
        itemPickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageble damageble = collision.GetComponent<Damageble>();

        if (damageble)
        {
            bool healed = damageble.Heal(healthRestore);
            if (healed)
            {
                if (itemPickupSource)
                {
                    AudioSource.PlayClipAtPoint(itemPickupSource.clip,gameObject.transform.position,itemPickupSource.volume);
                }

                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}