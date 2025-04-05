using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Controls Settings")]
    [SerializeField, Tooltip("Forward speed")]
    private float _speed = 5f;
    [SerializeField, Tooltip("Speed of backward and sideways movement")]
    private float _shuntingSpeed = 1f;
    [SerializeField, Tooltip("For rotations speed")]
    private float _mouseSensitivity = 150f;
    [SerializeField, Tooltip("Limits of OX rotations")]
    private float _cameraMaxAngleX = 70f;
    private float _xRotation;
    
    private void Update()
    {
        Rotate();
        Move();
    }

    private void Rotate()
    {
        float yRotation = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        _xRotation -= Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;
        _xRotation = Math.Clamp(_xRotation, -_cameraMaxAngleX, _cameraMaxAngleX);
        transform.Rotate(0f, yRotation, 0f);
        transform.localEulerAngles = new Vector3(_xRotation, transform.localEulerAngles.y, 0f);
    }

    private void Move()
    {
        Vector3 movement = Vector3.zero;
        movement.z = Input.GetAxis("Vertical");
        movement.z *= movement.z > 0 ? _speed : _shuntingSpeed;
        movement.x = Input.GetAxis("Horizontal") * _shuntingSpeed;
        movement *= Time.deltaTime;
        transform.Translate(movement);
    }
}
