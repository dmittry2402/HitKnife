using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkDestruction : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyElement());
    }

    IEnumerator DestroyElement()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
