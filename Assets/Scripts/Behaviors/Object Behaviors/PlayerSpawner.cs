using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform entitiesRootObject;

    public void Spawn()
    {
        GameObject.Instantiate(playerPrefab, transform.position, transform.rotation, entitiesRootObject);
    }
}
