using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float ascendSpeed = 5f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;

    public Transform cameraPivot;
    public Camera _camera;

    [SerializeField] private Vector3 offset = new Vector3(0, 10, -10);
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float lookSpeed = 5f;

    void Awake()
    {
        if (cameraPivot != null)
            _camera.transform.LookAt(cameraPivot.position);
            
    }

    void LateUpdate()
    {
        if (cameraPivot == null) return;

        // rotate to look at pivot
        Quaternion desiredRot = Quaternion.LookRotation(cameraPivot.position - _camera.transform.position);
        _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, desiredRot, lookSpeed * Time.deltaTime);
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical"); 

        Vector3 move = (transform.forward * v + transform.right * h).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel"); // scroll up/down
        if (Mathf.Abs(scroll) > 0.01f)
            _camera.transform.position += Vector3.up * scroll * ascendSpeed;
    }
}
