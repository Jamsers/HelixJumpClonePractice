using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallObstacleManagerOnHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Lower Plane Of Visibility" && gameObject.tag == "Next Spawn Location") {
            SlotFillIn();
        }
        else if (other.tag == "Lower Plane Of Visibility" && gameObject.tag == "Reset Position Trigger") {
            ObstacleManger.Instance.QueueResetPosition();
        }
    }
    


    public void SlotFillIn() {
        ObstacleManger.Instance.SpawnInNextSlot(transform);
    }
}
