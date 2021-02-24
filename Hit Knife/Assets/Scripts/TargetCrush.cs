using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCrush : MonoBehaviour
{
    public List<Rigidbody2D> Chunks;
    public float Force;
    public void Crush()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        if (Chunks.Count > 0)
        {
            for (int i = 0; i < Chunks.Count; i++)
            {
                Chunks[i].transform.parent = null;
                Chunks[i].bodyType = RigidbodyType2D.Dynamic;
                Chunks[i].gravityScale = 3;
                Chunks[i].AddForce(new Vector2(Random.Range(-Force, Force), Random.Range(-Force, Force)), ForceMode2D.Impulse);
                Chunks[i].gameObject.AddComponent<ChunkDestruction>();
            }
        }
        List<GameObject> objs = gameObject.GetComponent<Target>().Objects;
        if (Chunks.Count > 0)
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (objs[i])
                {
                    objs[i].tag = null;
                    Rigidbody2D rb = objs[i].GetComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.gravityScale = 3;
                    rb.AddForce(new Vector2(Random.Range(-Force, Force), Random.Range(-Force, Force)), ForceMode2D.Impulse);
                }
            }
        }

    }
}
