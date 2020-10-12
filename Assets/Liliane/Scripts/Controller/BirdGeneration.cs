using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdGeneration : MonoBehaviour
{
    public GameObject birdPrefab;

    public Transform posSpawnA;
    public Transform posSpawnB;

    public int signalDirection;
    public float speed;

    private Vector3 posToSpawn;
    
    void Start()
    {
        StartCoroutine("SpawnBird");
    }

    private IEnumerator SpawnBird()
    {
        yield return new WaitForSeconds(Random.Range(2, 8));

        posToSpawn = new Vector2(this.transform.position.x, Random.Range(posSpawnA.position.y, posSpawnA.position.y));
        GameObject temp = Instantiate(birdPrefab, posToSpawn, transform.rotation);
        temp.TryGetComponent(out Rigidbody2D tempRb);
        tempRb.velocity = new Vector2(Random.Range(2, speed) * signalDirection, 0);
        
        Destroy(temp, 15);
        StartCoroutine("SpawnBird");

    }

}
