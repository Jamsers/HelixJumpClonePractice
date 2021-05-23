using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] ObstacleOption[] obstacleOptions;
    [SerializeField] public Transform nextSpawnLocation;

    public enum ObstacleTypeEnum {
        ChunkOut
    }

    [System.Serializable]
    public struct ObstacleOption {
        public ObstacleTypeEnum type;
        public GameObject reference;
    }


    public void SpawnIn(ObstacleTypeEnum type) {
        ResetObstacle();

        for (int i = 0; i < obstacleOptions.Length; i++) {
            if (obstacleOptions[i].type == type) {
                obstacleOptions[i].reference.GetComponent<ObstacleChunkOut>().Spawn();
                break;
            }
        }
    }

    void ResetObstacle() {
        for (int i = 0; i < obstacleOptions.Length; i++) {
            obstacleOptions[i].reference.GetComponent<ObstacleChunkOut>().Reset();
        }
    }

    // Start is called before the first frame update
    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
