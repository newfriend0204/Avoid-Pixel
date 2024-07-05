using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public Joystick moveobject;
    public float speed;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(vertical, 0, -horizontal) * 0.5f);



        Vector3 dir = new Vector3(moveobject.Vertical, 0, -moveobject.Horizontal);
        dir.Normalize();

        float newX = transform.position.x + dir.x * speed * Time.deltaTime;
        newX = Mathf.Clamp(newX, minX, maxX);

        float newZ = transform.position.z + dir.z * speed * Time.deltaTime;
        newZ = Mathf.Clamp(newZ, minZ, maxZ);

        transform.position = new Vector3(newX, transform.position.y, newZ);
    }
}
