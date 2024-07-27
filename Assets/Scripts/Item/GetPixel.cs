using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPixel : MonoBehaviour
{
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            GameManager.SpawnObject();
            GameManager.getPixels += 1.0f;
            Destroy(gameObject);
        }
    }
}
