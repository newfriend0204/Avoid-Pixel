using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPixels : MonoBehaviour
{
    public GameObject Pixels;
    private int numObjects = 20;
    void Start() {
        for (int i = 0; i < numObjects; i++) {
            SpawnObject();
        }
    }

    public void SpawnObject() {
        float randomX = Random.Range(2f, 98f);
        float randomZ = Random.Range(2f, 98f);
        Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);

        Instantiate(Pixels, randomPosition, Quaternion.identity);
    }
}
