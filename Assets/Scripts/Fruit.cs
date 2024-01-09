using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    int level;
    public int Level
    {
        get { return level; }
    }

    bool isAdd(Vector3 pos1)
    {
        Vector3 pos2 = this.transform.position;

        if (pos1.y > pos2.y) { return true; }
        else if (pos1.y == pos2.y)
        {
            if (pos1.x < pos2.x) { return true; }
        }
        return false;
    }

    public void Init(int value, bool spawned)
    {
        if(value >= GameManager.Get<Sprite[]>(KEY.SPRITES).Length)
        {
            Destroy(gameObject);
            return;
        }
        this.level = value;
        float range = 0.25f * (1 + value);
        transform.localScale = new Vector3(range, range, 1);
        GetComponent<SpriteRenderer>().sprite = GameManager.Get<Sprite[]>(KEY.SPRITES)[value];

        if (spawned) this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        else this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "collider" || collision.collider.tag == "Fruit")
        {
            if (this.transform.parent)
            {
                this.transform.parent.GetComponent<Spawner>().state = State.generate;
                this.transform.parent = null;
            }
        }

        if (collision.collider.tag == "Fruit" && collision.gameObject.GetComponent<Fruit>().Level == this.Level)
        {
            if (isAdd(collision.transform.position))
            {
                Debug.Log("LEVEL UP");
                Destroy(collision.gameObject);

                GameObject new_fruit = Instantiate(Resources.Load<GameObject>("Fruit"), transform.position, transform.rotation);
                new_fruit.GetComponent<Fruit>().Init(this.level + 1, false);

                Destroy(gameObject);
                GameManager.Set(KEY.SCORE, this.level);
            }
        }
    }
}
