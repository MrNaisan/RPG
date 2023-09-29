using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] AsteroidPrefabs;
    public int AsteroidsCount;
    public Vector3 Size;

    void Start()
    {
        CreateWay();
        SpawnAsteroids();
    }

    void CreateWay()
    {
        var way = new GameObject("Way").transform;

        CreatePart("Floor", new Vector3(0, -Size.y / 2, Size.z / 2), Quaternion.identity, new Vector3(Size.x, 1, Size.z), way);
        CreatePart("Roof", new Vector3(0, Size.y / 2, Size.z / 2), Quaternion.identity, new Vector3(Size.x, 1, Size.z), way);
        CreatePart("WallL", new Vector3(-Size.x / 2, 0, Size.z / 2), Quaternion.identity, new Vector3(1, Size.y, Size.z), way);
        CreatePart("WallR", new Vector3(Size.x / 2, 0, Size.z / 2), Quaternion.identity, new Vector3(1, Size.y, Size.z), way);
        CreatePart("WallB", Vector3.zero, Quaternion.identity, new Vector3(Size.x, Size.y, 1), way);
    }

    void CreatePart(string name, Vector3 pos, Quaternion rot, Vector3 size, Transform parent)
    {
        var obj = new GameObject(name);
        
        obj.transform.parent = parent;
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.AddComponent<BoxCollider>().size = size;
    }

    void SpawnAsteroids()
    {
        var asteroids = new GameObject("Asteroids").transform;

        for(int i = 0; i < AsteroidsCount; i++)
        {
            float posX = Random.Range(0, 2) == 0 ? Random.Range(0, Size.x / 2) : Random.Range(-Size.x / 2, 0);
            float posY = Random.Range(0, 2) == 0 ? Random.Range(0, Size.y / 2) : Random.Range(-Size.y / 2, 0);
            float posZ = Random.Range(1, Size.z);

            Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)], new Vector3(posX, posY, posZ), Quaternion.identity, asteroids);
        }
    }
}
