using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] ObstacleOption[] obstacleOptions;
    [SerializeField] public Transform nextSpawnLocation;

    public enum ObstacleType {
        ChunkOut
    }

    [System.Serializable]
    public struct ObstacleOption {
        public ObstacleType type;
        public GameObject obstacle;
    }


    public void SpawnIn(ObstacleType type) {
        ClearObstacle();

        switch (type) {
            case ObstacleType.ChunkOut:
                SpawnChunkOut();
                break;
        }
    }

    void ClearObstacle() {
        foreach (ObstacleOption option in obstacleOptions) {
            option.obstacle.SetActive(false);
        }
    }

    void SpawnChunkOut() {
        foreach (ObstacleOption option in obstacleOptions) {
            if (option.type == ObstacleType.ChunkOut) {
                option.obstacle.SetActive(true);
                float rotation = Random.Range(0f, 360f);
                option.obstacle.transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ClearObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
