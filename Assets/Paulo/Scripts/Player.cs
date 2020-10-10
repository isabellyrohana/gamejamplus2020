using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerRotationSrite
{
    Left,
    Right
}

public class Player : MonoBehaviour
{

    [Range(.5f, 10f)]
    [SerializeField]
    private float speed = 2.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        #region Movement

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Rotate(PlayerRotationSrite.Left);
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
            
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rotate(PlayerRotationSrite.Right);
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        #endregion

        #region ToogleSpriteRotation
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Rotate(PlayerRotationSrite.Left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Rotate(PlayerRotationSrite.Right);
        }
        #endregion
    }


    private void Rotate(PlayerRotationSrite side)
    {
        switch (side)
        {
            case PlayerRotationSrite.Left:
                transform.Rotate(new Vector3(0, 0, 0), Space.World);
                break;

            case PlayerRotationSrite.Right:
                transform.Rotate(new Vector3(0, 180, 0), Space.World);
                break;

            default:
                transform.Rotate(new Vector3(0, 0, 0), Space.World);
                break;
        }
            
    }
}
