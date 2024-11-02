using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject template;      
    public float range = 10f;        
    public int maxSpawns = 5;        

    public GameObject[] spawnedObjects;   

    void Start()
    {
        
        spawnedObjects = new GameObject[maxSpawns];
        
       
        for (int i = 0; i < maxSpawns; i++)
        {
            SpawnObject(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            if (spawnedObjects[i] != null && !spawnedObjects[i].activeInHierarchy)
            {
                SpawnObject(i);
            }
        }
    }

    void SpawnObject(int index)
    {
        Vector3 randomPosition = GetRandomPositionWithinRange();

        // If the object doesn't exist yet, instantiate it
        if (spawnedObjects[index] == null)
        {
            spawnedObjects[index] = Instantiate(template, randomPosition, Quaternion.identity);
        }
        else
        {
            spawnedObjects[index].transform.position = randomPosition;
            spawnedObjects[index].SetActive(true);
        }

        PositionAboveGround(spawnedObjects[index]);
    }

    Vector3 GetRandomPositionWithinRange()
    {
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);
        Vector3 randomPosition = new Vector3(x, 0, z) + transform.position;
        return randomPosition;
    }

    void PositionAboveGround(GameObject obj)
    {
        Collider objCollider = obj.GetComponent<Collider>();
        if (objCollider != null)
        {
            Vector3 position = obj.transform.position;
            position.y += objCollider.bounds.extents.y;
            obj.transform.position = position;
        }
    }
}

