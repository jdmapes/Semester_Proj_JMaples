using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTime
{
    public PlayerSpawner spawner;
    public float time;
}

[CreateAssetMenu(fileName = "Game", menuName = "Data/Game", order = 1)]
public class Game : ScriptableObjectsWithEvent
{
    private List<SpawnTime> spawnTimes = new List<SpawnTime>();
   public void Initialize()
    {
        AddListener("death", OnDeathEvent);

        var playerSpawns = GameObject.FindObjectsOfType<PlayerSpawner>();

        if (playerSpawns.Length == 0) return;
        Debug.Log("Works");
        playerSpawns[0].Spawn();
    }

    public void Update()
    {
        for (var i = spawnTimes.Count - 1; i >= 0; i--)
        {
            if (spawnTimes[i].time < Time.fixedTime)
            {
                spawnTimes[i].spawner.Spawn();
                spawnTimes.RemoveAt(i);
            }
        }
    }

    public void OnDeathEvent(object[] parameters)
    {
        if (parameters.Length is not 1) return;

        if(!(parameters[0] is Health health)) return;

        if(health.TryGetComponent(out PlayerMovement _))
        {
            var playerSpawns = GameObject.FindObjectsOfType<PlayerSpawner>();

            if (playerSpawns.Length == 0) return;            
            playerSpawns[0].Spawn();

            spawnTimes.Add(new SpawnTime
            {
                spawner = playerSpawns[0],
                time = Time.fixedTime + 1f
            });
        }
    }
}
