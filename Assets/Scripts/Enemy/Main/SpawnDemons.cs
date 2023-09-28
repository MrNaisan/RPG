using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDemons : MonoBehaviour
{
    public static SpawnDemons Default;
    public GameObject DemonPrefab;
    public int DemonsNum = 5;
    public float Radius = 5;
    public int BordersNum = 2;
    [HideInInspector]
    public float borderStep;
    [HideInInspector]
    public int borderCounter = 1;
    Enemy golem; 

    private void Start() 
    {
        golem = FindObjectOfType<Enemy>();
        Default = this;
        borderStep = 1f / (BordersNum + 1f);
    }

    public void Spawn()
    {
        for(int i = 0; i < DemonsNum; i++)
        {
            float angle = i * Mathf.PI * 2f / DemonsNum;

            // Вычисляем позицию для нового объекта на окружности с радиусом radius.
            Vector3 spawnPosition = golem.transform.position + new Vector3(Mathf.Cos(angle) * Radius, 0f, Mathf.Sin(angle) * Radius);

            // Заспавниваем объект.
            Instantiate(DemonPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
