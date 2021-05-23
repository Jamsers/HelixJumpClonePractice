using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleChunkOut : MonoBehaviour, Obstacle {
    [SerializeField] Material normalMaterial;
    [SerializeField] Material hostileMaterial;

    public Obstacle.Type type {
        get => Obstacle.Type.ChunkOut;
    }

    public void Reset() {
        transform.localRotation = Quaternion.identity;

        for (int i = 0; i < transform.childCount; i++) {
            Vector3 currentPos = transform.GetChild(i).transform.GetChild(0).transform.localPosition;
            currentPos.y = 0f;
            transform.GetChild(i).transform.GetChild(0).transform.localPosition = currentPos;
            transform.GetChild(i).transform.GetChild(0).gameObject.tag = "Untagged";
            transform.GetChild(i).transform.GetChild(0).GetComponent<MeshRenderer>().material = normalMaterial;
        }

        gameObject.SetActive(false);
    }

    public void Spawn() {
        float rotation = Random.Range(0f, 360f);
        transform.localRotation = Quaternion.Euler(new Vector3(0, rotation, 0));

        int chunkToTurnDamage = Random.Range(0, transform.childCount - 1);

        Vector3 currentPos = transform.GetChild(chunkToTurnDamage).transform.GetChild(0).transform.localPosition;
        currentPos.y += 0.01f;
        transform.GetChild(chunkToTurnDamage).transform.GetChild(0).transform.localPosition = currentPos;
        transform.GetChild(chunkToTurnDamage).transform.GetChild(0).gameObject.tag = "Hostile Chunk";
        transform.GetChild(chunkToTurnDamage).transform.GetChild(0).GetComponent<MeshRenderer>().material = hostileMaterial;

        gameObject.SetActive(true);
    }
}