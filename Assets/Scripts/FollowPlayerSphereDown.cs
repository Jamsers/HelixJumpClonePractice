using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerSphereDown : MonoBehaviour {
    [SerializeField] Transform playerSphereTransform;

    Vector3 lowestPlayerSpherePosition;

    void Start() {
        ResetLowestPlayerPosition();
    }

    public void ResetLowestPlayerPosition() {
        lowestPlayerSpherePosition = playerSphereTransform.position;
    }

    void Update() {
        Vector3 currentPostion = transform.position;
        if (playerSphereTransform.position.y < lowestPlayerSpherePosition.y) {
            currentPostion.y = playerSphereTransform.position.y;
            transform.position = currentPostion;
            lowestPlayerSpherePosition = playerSphereTransform.position;
        }
    }
}
