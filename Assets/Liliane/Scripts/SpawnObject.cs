using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : Singleton<SpawnObject>
{
    public GameObject prefabObjectToShoot;

    private void Start()
    {
        SpawnObjectToShoot();
    }

    public void SpawnObjectToShoot()
    {
        Instantiate(prefabObjectToShoot, transform.position, transform.rotation);
    }
}
