using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    bool isMousedDown = false;
    Vector3 mousedDownLocation = Vector3.zero;
    float origYRotation;
    [SerializeField] float rotateMultiplier;
    [SerializeField] Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScoreAndHealth.Instance.isAlive == false)
            return;

        if (Input.GetMouseButtonDown(0) && isMousedDown == false) {
            isMousedDown = true;
            mousedDownLocation = Input.mousePosition;
            origYRotation = transform.localRotation.eulerAngles.y;
        }

        if (Input.GetMouseButtonUp(0)) {
            isMousedDown = false;
            mousedDownLocation = Vector3.zero;
        }

        if (isMousedDown) {
            float rawMove = mousedDownLocation.x - Input.mousePosition.x;
            float percentMove = rawMove / Screen.width;
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            currentRotation.y = origYRotation + (percentMove * rotateMultiplier);
            transform.localRotation = Quaternion.Euler(currentRotation);
            cameraTransform.localRotation = Quaternion.Euler(currentRotation);
        }

        
    }
}
