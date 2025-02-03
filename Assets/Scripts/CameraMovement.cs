using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Transform target;
    public float smoothing;

    public Vector2 maxPosition;
    public Vector2 minPosition;

    public VectorValue cameraMinPosition;
    public VectorValue cameraMaxPosition;

    public Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
        minPosition += cameraMinPosition.initialValue;
        maxPosition += cameraMaxPosition.initialValue;

        MoveCameraToPlayer();
    }

    void FixedUpdate() {
        MoveCameraToPlayer();
    }

    void MoveCameraToPlayer() {
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick() {
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo() {
        yield return new WaitForSeconds(.2f);
        anim.SetBool("kickActive", false);
    }
}