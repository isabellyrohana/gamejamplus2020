using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToShoot : MonoBehaviour
{

    private bool _gameIsPaused = false;

    public void CanDestroy()
    {
        Destroy(this.gameObject, 4.05f);
        //StartCoroutine("SpawnNewObject");
    }

    /*private IEnumerator SpawnNewObject()
    {
        yield return new WaitForSeconds(4f);
        SpawnObject.Instance.SpawnObjectToShoot();
    }*/
}
