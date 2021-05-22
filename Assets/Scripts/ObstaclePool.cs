using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour {
    [SerializeField] int poolSize;
    [SerializeField] GameObject obstacleItem;

    GameObject[] pool;

    // Start is called before the first frame update
    void Start() {
        pool = new GameObject[poolSize];
        for (int i = 0; i < poolSize; i++) {
            pool[i] = Instantiate(obstacleItem);
            pool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
