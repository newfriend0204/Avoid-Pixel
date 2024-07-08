using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject Player;
    public Protect Script;
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Player")) {
            Script.check_protect = 1;
            Player.transform.position = new Vector3(39.8258f, 31.76737f, 48.7253f); 
        }
    }
}
