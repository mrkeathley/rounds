using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public VectorValue cameraMinStorage;
    public VectorValue cameraMaxStorage;
    public GameObject fadeInPanel;

    public Vector2 cameraChangeMin;
    public Vector2 cameraChangeMax;


    private void Awake() {
        if (fadeInPanel != null) {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") || other.isTrigger) return;
        playerStorage.initialValue = playerPosition;
        cameraMinStorage.initialValue = cameraChangeMin;
        cameraMaxStorage.initialValue = cameraChangeMax;
        SceneManager.LoadScene(sceneToLoad);
    }
}