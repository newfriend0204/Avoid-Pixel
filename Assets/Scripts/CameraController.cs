using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Joystick moveobject;
    public float speed;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(vertical, 0, -horizontal) * 0.3f);



        Vector3 dir = new Vector3(moveobject.Vertical, 0, -moveobject.Horizontal);
        dir.Normalize();
        transform.position += dir * speed * Time.deltaTime;
    }
}
