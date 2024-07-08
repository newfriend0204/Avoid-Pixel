using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GlitchEffect Script;
    public int check_protect = 0;

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Life") && check_protect == 0)
            Script.IncreaseIntensity();
    }
}
