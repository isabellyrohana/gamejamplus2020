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

        SceneManager.LoadScene("SceneStage02");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            if(collision.GetComponent<PlayerController>().papelzin != null)
                ExitLevel();
    }
}
