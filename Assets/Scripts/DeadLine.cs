using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLine : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Fruit" && collision.transform.parent==null)
        {
            Debug.Log("game_end");
            GameManager.GameOver();
        }
    }
}
