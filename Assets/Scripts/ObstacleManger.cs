using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : MonoBehaviour {
    static public ObstacleManger Instance;
    static public GameObject Player;
    static public GameObject Camera;

    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] GameObject initialObstacleSpawn;
    [SerializeField] int obstacleActiveAmount;
    [SerializeField] Transform obstacleBase;

    GameObject[] pool;
    int poolIndex = 0;
    Transform nextSlot = null;

    Vector3 playerStartPosition;
    bool shouldResetPosition = false;

    void Awake() {
        pool = new GameObject[obstacleActiveAmount];
        for (int i = 0; i < obstacleActiveAmount; i++) {
            pool[i] = Instantiate(obstaclePrefab, obstacleBase);
            pool[i].SetActive(false);
        }
    }

    void Start() {
        Instance = this;
        Player = GameObject.FindGameObjectWithTag("Player");
        Camera = GameObject.FindGameObjectWithTag("MainCamera");

        playerStartPosition = Player.transform.localPosition;

        for (int i = 0; i < obstacleActiveAmount; i++) {
            SpawnInNextSlot();
        }
    }

    public void QueueResetPosition() {
        shouldResetPosition = true;
    }

    void ResetPosition () {
        float yReset = playerStartPosition.y - Player.transform.localPosition.y;

        Vector3 playerCurrentPosition = Player.transform.localPosition;
        playerCurrentPosition.y += yReset;
        Player.transform.localPosition = playerCurrentPosition;

        Vector3 cameraCurrentPosition = Camera.gameObject.transform.localPosition;
        cameraCurrentPosition.y += yReset;
        Camera.transform.localPosition = cameraCurrentPosition;
        Camera.GetComponent<FollowPlayerSphereDown>().ResetLowestPlayerPosition();

        foreach (GameObject item in pool) {
            Vector3 itemCurrentPosition = item.transform.localPosition;
            itemCurrentPosition.y += yReset;
            item.transform.localPosition = itemCurrentPosition;
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

        Obstacle.Type typeToSpawn = Obstacle.Type.ChunkOut;

        switch (Random.Range(0, 0)) {
            case 0:
                typeToSpawn = Obstacle.Type.ChunkOut;
                break;
        }

        pool[poolIndex].GetComponent<ObstacleBase>().SpawnIn(typeToSpawn);
        pool[poolIndex].SetActive(true);
        pool[poolIndex].transform.position = nextSlot.position;
        nextSlot = pool[poolIndex].GetComponent<ObstacleBase>().nextSpawnSlot;

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