using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Rigidbody2D RB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wood" && !GameController.Instance.IsLevelFinished)
        {           
            RB.velocity = Vector2.zero;
            RB.bodyType = RigidbodyType2D.Kinematic;
            transform.parent = collision.transform;
            tag = "Knife";

            GameController.Instance.IncreaseScoreCount();

            collision.GetComponent<Target>().Objects.Add(gameObject);

            Vector3 pos = collision.ClosestPoint(transform.position);

            ParticleController.Instance.StartEffect(new Vector3(pos.x,pos.y,1));

            if (GameController.Instance.KnivesCount == 0)
            {
                GameController.Instance.Victory();
            }
            else
            {
                Vibration.Vibrate();
            }
            
        }
        if (collision.gameObject.tag == "Knife" && !GameController.Instance.IsLevelFinished)
        {
            Vibration.Vibrate();

            RB.velocity = Vector2.zero;
            RB.gravityScale = 1;
            GameController.Instance.Defeat();
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Apple" && !GameController.Instance.IsLevelFinished)
        {
            GameController.Instance.IncreaseAppleCount();
            Destroy(collision.gameObject);
        }
    }
}
