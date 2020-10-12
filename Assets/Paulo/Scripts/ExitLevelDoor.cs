using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevelDoor : MonoBehaviour
{

    public void ExitLevel()
    {
        StartCoroutine(OpenDoor());
    }

    IEnumerator OpenDoor()
    {
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.1f);

        SceneController.ToStage02();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Porta colidiu com o player");
            ExitLevel();
        }
    }
}
