using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCute : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private Animator playerAnim;
    public GameObject continuePanel;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        
        StartCoroutine("PlayAnim");
    }

    IEnumerator PlayAnim()
    {
        playerAnim.SetBool("walk", true);
        yield return new WaitForSeconds(0.05f);
        playerRb.velocity = new Vector2(1.5f, 0);

        yield return new WaitForSeconds(2f);
        playerRb.velocity = Vector2.zero;
        playerAnim.SetBool("walk", false);

        yield return new WaitForSeconds(4f);
        continuePanel.SetActive(true);

        yield return new WaitForSeconds(5f);
        GameController.Instance.SceneToLoad("SceneMainMenu");
    }
}
