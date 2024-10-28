using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject projectileOwner;
    [SerializeField] Transform spawnPoint;
    
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifeTime;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectileTarget;
    
    
}
