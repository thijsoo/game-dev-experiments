using System;
using UnityEngine;
using UnityEngine.Events;

public class CoinPickup : MonoBehaviour
{
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public static UnityAction<int> coinPickupEvent;
    private AudioSource itemPickupSource;

    private void Awake()
    {
        itemPickupSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        coinPickupEvent.Invoke(1);
        if (itemPickupSource)
        {
            AudioSource.PlayClipAtPoint(itemPickupSource.clip,gameObject.transform.position,itemPickupSource.volume);
        }
        Destroy(gameObject);
    }
    
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
