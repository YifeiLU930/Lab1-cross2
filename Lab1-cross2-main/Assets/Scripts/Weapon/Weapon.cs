using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]


public class Weapon : MonoBehaviour
{
   public Rigidbody rb
    {
        get => _rb;
    }
    
    private Rigidbody _rb;
    
    public Rigidbody projectilePrefab;
    public Transform spawnPoint;

    public float projectileSpeed;
    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        spawnPoint = transform.GetChild(0).transform;
    }

    public virtual void Fire()
    {
        if (!projectilePrefab) return;
        if (!transform.parent.gameObject) return;
      
       
        Rigidbody temp = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

        temp.AddForce(transform.forward * projectileSpeed);
    }
    
}
