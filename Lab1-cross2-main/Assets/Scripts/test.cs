using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    monsterSpawner Spawner;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Spawner != null) Spawner.currentMonster.Remove(this.gameObject);
            Destroy(other.gameObject);

        }
    }

    public void SetSpawner(monsterSpawner _spawner)
    {
        Spawner = _spawner;
    }
}
