using System.Collections.Generic;
using UnityEngine;


public enum KEY { SCORE, SEED, SPRITES};

public class GameManager : Singleton<GameManager>
{
    Dictionary<KEY, object> Data = new Dictionary<KEY, object>();
    int[] scores = { 1, 3, 6, 10, 15, 21, 28, 36, 45, 55, 66 };
    bool isGameOver = false;
    bool stop = false;
    public static void init()
    {
        Instance.Data[KEY.SCORE] = 0;
        Instance.Data[KEY.SEED] = Random.Range(0, 3);
        Instance.Data[KEY.SPRITES] = Resources.LoadAll<Sprite>("Animal");
        Instance.isGameOver = false;
        Instance.stop = false;
    }
    public static void STOP()
    {
        Instance.stop = Instance.stop == true? false: true;
    }
    public static bool GetStop() {  return Instance.stop; }
    public static T Get<T>(KEY key)
    {
        if(!Instance.Data.ContainsKey(key)) init();

        return (T)Instance.Data[key];
    }
    public static void Set(KEY key, object value = null)
    {
        if (!Instance.Data.ContainsKey(key)) init();

        switch (key)
        {
            case KEY.SCORE:
                Instance.Data[key] = (int)Instance.Data[key] + Instance.scores[(int)value];
                break;
            case KEY.SEED:
                Instance.Data[key] = Random.Range(0, 3);
                break;
            case KEY.SPRITES:
                break;
        }
    }

    public static bool IsGameOver { get { return Instance.isGameOver; } }
    public static void GameOver() { Instance.isGameOver = true; }
}