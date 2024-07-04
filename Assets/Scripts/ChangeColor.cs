using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
    private Renderer rend;
    private float colorLerp = 0f;

    void Start() {
        rend = GetComponent<Renderer>();
        StartCoroutine(ChangeColorRoutine());
    }

    private IEnumerator ChangeColorRoutine() {
        while (true) {
            colorLerp += Time.deltaTime * 2;
            rend.material.color = Color.Lerp(Color.white, Color.gray, colorLerp);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
