using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : MonoBehaviour {
    static public ObstacleManger Instance;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject initialObstacleSpawn;
    [SerializeField] int obstacleActiveAmount;
    [SerializeField] Transform obstacleBase;

    ObstaclePoolItem[] pool;

    Transform nextSlot = null;
    int poolIndex = 0;

    Vector3 playerStartPosition;
    bool shouldResetPosition = false;

    void Start() {
        Instance = this;
        playerStartPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

        pool = new ObstaclePoolItem[obstacleActiveAmount];
        for (int i = 0; i < obstacleActiveAmount; i++) {
            pool[i] = new ObstaclePoolItem(Instantiate(obstaclePrefab, obstacleBase));
        }

        for (int i = 0; i < obstacleActiveAmount; i++) {
            SpawnInNextSlot();
        }
    }

    public void QueueResetPosition() {
        shouldResetPosition = true;
    }

    void ResetPosition () {
        float yReset = playerStartPosition.y - GameObject.FindGameObjectWithTag("Player").gameObject.transform.position.y;

        Vector3 playerCurrentPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        playerCurrentPosition.y += yReset;
        GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = playerCurrentPosition;

        Vector3 cameraCurrentPosition = GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform.position;
        cameraCurrentPosition.y += yReset;
        GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform.position = cameraCurrentPosition;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowPlayerSphereDown>().ResetLowestPlayerPosition();

        foreach (ObstaclePoolItem item in pool) {
            Vector3 itemCurrentPosition = item.obstacle.gameObject.transform.position;
            itemCurrentPosition.y += yReset;
            item.obstacle.gameObject.transform.position = itemCurrentPosition;
        }
    }

    public void SpawnInNextSlot(Transform callerTransform) {
        if (callerTransform != nextSlot) {
            return;
        }

        SpawnInNextSlot();
    }
    public void SpawnInNextSlot() {
        if (nextSlot == null) {
            nextSlot = initialObstacleSpawn.transform;
        }

        pool[poolIndex].Deploy(nextSlot.position);
        nextSlot = pool[poolIndex].obstacle.nextSpawnLocation;

        int nextIndex = poolIndex + 1;
        if (nextIndex >= obstacleActiveAmount)
            poolIndex = 0;
        else
            poolIndex = nextIndex;
    }

    // Update is called once per frame
    void Update() {
        if (shouldResetPosition == true) {
            ResetPosition();
            shouldResetPosition = false;
        }
    }
}

class ObstaclePoolItem {
    public Obstacle obstacle;

    public ObstaclePoolItem(GameObject obstaclePrefab) {
        obstacle = obstaclePrefab.GetComponent<Obstacle>();
        obstacle.gameObject.SetActive(false);
    }

    public void Deploy(Vector3 location) {
        obstacle.SpawnIn(Obstacle.ObstacleTypeEnum.ChunkOut);
        obstacle.gameObject.SetActive(true);
        obstacle.transform.position = location;
    }
}