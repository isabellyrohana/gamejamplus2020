using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAt : MonoBehaviour
{
    public GameObject objectToFollow;

    public Vector3 offset = new Vector3(0, 2, -4);

    void LateUpdate()
    {
        transform.position = objectToFollow.transform.position + offset;
    }
}
