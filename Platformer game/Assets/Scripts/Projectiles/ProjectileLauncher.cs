using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    
    public GameObject projectilePrefab;
    
    public Transform projectileSpawnPoint;

    public void FireProjectile()
    {
        GameObject projectileGameObject = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectilePrefab.transform.rotation);
        
        Vector3 originalScale = projectileGameObject.transform.localScale;

        projectileGameObject.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? 1: -1,
            originalScale.y * transform.localScale.y > 0 ? 1: -1,
            originalScale.z
        );

    }

}
