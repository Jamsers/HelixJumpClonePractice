using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullPlayerSphereDown : MonoBehaviour {
    float yForce = 0;
    const float second = 1;

    const float bounceTimeOut = 0.5f;
    [SerializeField] float yForceCap = 9.8f;
    float timeOfLastBounce = 0;

    [SerializeField] float gravityForceOverTime = 0.1f;
    [SerializeField] float bounceForce = 20f;

    void Start() {

    }

    void Update() {
        if (PlayerScoreAndHealth.Instance.isAlive) {
            ConstantPullOfGravity();
            ApplyCurrentForce();
        }
    }

    void AddYForce(float force) {
        float newYForce = yForce + force;
        if (newYForce < yForceCap && newYForce > -yForceCap) {
            yForce = newYForce;
        }
        else if (newYForce > yForceCap) {
            yForce = yForceCap;
        }
        else if (newYForce < -yForceCap) {
            yForce = -yForceCap;
        }
    }

    void ConstantPullOfGravity() {
        float forceToAdd = (Time.deltaTime / second) * gravityForceOverTime;
        AddYForce(-forceToAdd);
    }

    void ApplyCurrentForce() {
        float forceToApply = (Time.deltaTime / second) * yForce;
        Vector3 currentPosition = transform.localPosition;
        currentPosition.y += forceToApply;
        transform.localPosition = currentPosition;
    }

    public void PlayerHitSomthing(Collider other) {
        if (other.tag == "Hostile Chunk") {
            PlayerScoreAndHealth.Instance.Died();
        }
        else if (other.tag == "This Spawn Location") {
            PlayerScoreAndHealth.Instance.AddScore();
        }
        else {
            Bounce();
        }
    }

    void Bounce() {
        if (Time.time - timeOfLastBounce > bounceTimeOut) {
            AddYForce(bounceForce);
            timeOfLastBounce = Time.time;
        }
        
    }
}
