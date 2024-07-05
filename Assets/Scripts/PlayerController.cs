using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GlitchEffect Script;

    void OnCollisionEnter(Collision col) {
        if (col.collider.gameObject.CompareTag("Life"))
            Script.IncreaseIntensity();
    }
}
