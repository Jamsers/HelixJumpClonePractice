using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool isMousedDown = false;
    Vector3 mousedDownLocation = Vector3.zero;
    float origYRotation;
    [SerializeField] float rotateMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isMousedDown == false) {
            isMousedDown = true;
            mousedDownLocation = Input.mousePosition;
            origYRotation = transform.rotation.eulerAngles.y;
        }

        if (Input.GetMouseButtonUp(0)) {
            isMousedDown = false;
            mousedDownLocation = Vector3.zero;
        }

        if (isMousedDown) {
            Vector3 currentRotation = transform.rotation.eulerAngles;
            currentRotation.y = origYRotation + ((mousedDownLocation.x - Input.mousePosition.x) * rotateMultiplier);
            transform.rotation = Quaternion.Euler(currentRotation);
            Debug.Log(-(mousedDownLocation.x - Input.mousePosition.x));
        }
        else {
            Debug.Log(0);
        }

        
    }
}
