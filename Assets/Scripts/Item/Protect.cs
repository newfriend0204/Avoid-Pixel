using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect : MonoBehaviour {
    public GameObject Protect_Sphere;
    public GameObject Player;
    public GameObject Protect_Image;
    public PlayerController Script;
    public GameManager GameManager;
    public int check_protect = 0;
    int check_timer = 0;

    private void Start() {
        check_protect = 1;
    }

    public void Update() {
        Protect_Image.transform.position = Protect_Sphere.transform.position;
        if (check_protect == 1) {
            Script.check_protect = 1;
            Protect_Sphere.transform.position = Player.transform.position;
            if (check_timer == 0)
                StartCoroutine(Timer());
        }
    }

    IEnumerator Timer() {
        check_timer = 1;
        yield return new WaitForSeconds(3f);
        check_timer = 0;
        check_protect = 0;
        Script.check_protect = 0;
        Protect_Sphere.transform.position = new Vector3(-16.17f, 0, -61.74f);
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Life")) {
            Life col_life = col.GetComponent<Life>();
            int x = col_life.x;
            int y = col_life.y;
            GameManager.cellStates[y, x] = 0;
            Destroy(col.gameObject);
        }
    }
}