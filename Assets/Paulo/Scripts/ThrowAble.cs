using UnityEngine;

public interface ThrowAble
{
    void Throw(Vector3 direction, Vector3 position);

    void Carry(GameObject player);
}
