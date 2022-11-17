using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Game game;

    // Start is called before the first frame update
    void Start()
    {
        game.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        game.Update();
    }
}
