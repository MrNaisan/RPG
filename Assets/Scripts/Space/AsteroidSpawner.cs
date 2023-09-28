using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] AsteroidPrefabs;
    public int AsteroidsCount;

    void Start()
    {
        for(int i = 0; i < AsteroidsCount; i++)
        {
            float posX = Random.Range(0, 2) == 0 ? Random.Range(3, AsteroidsCount / 5) : Random.Range(-1 * AsteroidsCount, -3);
            float posY = Random.Range(0, 2) == 0 ? Random.Range(3, AsteroidsCount / 5) : Random.Range(-1 * AsteroidsCount / 5, -3);
            float posZ = Random.Range(0, 2) == 0 ? Random.Range(3, AsteroidsCount / 5) : Random.Range(-1 * AsteroidsCount, -3);

            Instantiate(AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length)], new Vector3(posX, posY, posZ), Quaternion.identity);
        }
    }
}
