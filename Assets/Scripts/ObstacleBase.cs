using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour {
    [SerializeField] ObstacleOption[] obstacleOptions;
    [SerializeField] Transform nextSpawnLocation;
    [SerializeField] Transform obstacleBase;

    public Transform nextSpawnSlot {
        get => nextSpawnLocation;
    }

    [System.Serializable]
    struct ObstacleOption {
        public GameObject prefab;
        [HideInInspector] public Obstacle obstacle;
    }

    void Awake() {
        for (int i = 0; i < obstacleOptions.Length; i++) {
            GameObject instantiatedPrefab = Instantiate(obstacleOptions[i].prefab, obstacleBase);
            obstacleOptions[i].obstacle = instantiatedPrefab.GetComponent(typeof(Obstacle)) as Obstacle;
        }
    }

    void Start() {
    }

    public void SpawnIn(Obstacle.Type type) {
        ResetObstacle();

        foreach (ObstacleOption obstacleOption in obstacleOptions) {
            if (obstacleOption.obstacle.type == type) {
                obstacleOption.obstacle.Spawn();
                break;
            }
        }
    }

    void ResetObstacle() {
        foreach (ObstacleOption obstacleOption in obstacleOptions) {
            obstacleOption.obstacle.Reset();
        }
    }
}