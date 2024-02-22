using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    
    [SerializeField] float currentRotationX;
    [SerializeField] float cameraRotationLimit = 85f;
    [SerializeField] float scrollSpeed = 150.0f;
    [SerializeField] float sensitivity = 450f;
    
    void Start()
    {
        
    }

    void Update()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * sensitivity;
        currentRotationX -= cameraRotationX * Time.deltaTime;
        //Clamp cameraRotationX instead of currentRotationX
        currentRotationX = Mathf.Clamp(currentRotationX, -cameraRotationLimit, cameraRotationLimit);

        transform.localEulerAngles = new Vector3(currentRotationX, 0, 0);
    }
}
