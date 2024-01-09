using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {hold, wait, generate}
public class Spawner : MonoBehaviour
{
    GameObject fruit;
    int seed;
    [SerializeField] float y_pos = 4;
    public State state;

    void Start()
    {
        this.transform.position = new Vector2(0, y_pos);
        state = State.hold;

        SpawnFruit();
    }

    void Update()
    {
        if(!GameManager.IsGameOver && !GameManager.GetStop())
        {
            switch (state)
            {
                case State.hold:
                    MouseFollow();
                    if (Input.GetMouseButtonDown(0)) this.state = State.wait;
                    break;
                case State.wait:
                    this.fruit.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    break;
                case State.generate:
                    this.state = State.hold;
                    SpawnFruit();
                    break;
            }
        }

    }

    void SpawnFruit()
    {
        GameManager.Set(KEY.SEED);
        seed = GameManager.Get<int>(KEY.SEED);

        this.fruit = null;
        this.fruit = Instantiate(Resources.Load<GameObject>("Fruit"), this.transform);
        fruit.GetComponent<Fruit>().Init(GameManager.Get<int>(KEY.SEED), true);
    }

    void MouseFollow()
    {
        fruit.transform.position = GetMouse();
    }
    Vector2 GetMouse()
    {
        float range = 0.35f * (1 + seed);
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mouse_pos.x > 2.5f - range / 2) mouse_pos.x = 2.5f - range / 2;
        else if (mouse_pos.x < -2.5f + range / 2) mouse_pos.x = -2.5f + range / 2;
        return new Vector2(mouse_pos.x, y_pos);
    }


}