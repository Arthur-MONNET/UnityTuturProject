using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float sidewaysForce = 500f;
    [SerializeField] float rotationSpeed = 4.0f;
    [SerializeField] float maxAngle = 45f;

    float initialRotationZ;
    float currentRotationZ;

    void Start()
    {
        initialRotationZ = transform.rotation.eulerAngles.z;
        currentRotationZ = initialRotationZ;
    }

    // Start is called before the first frame update
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            currentRotationZ += Mathf.Clamp(maxAngle * Time.deltaTime * rotationSpeed, 0f + initialRotationZ - currentRotationZ, maxAngle + initialRotationZ - currentRotationZ);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);
            
            rb.AddForce(new Vector3(-sidewaysForce * Time.deltaTime, 0, 0), ForceMode.VelocityChange);
        } else if (Input.GetKey(KeyCode.D))
        {
            currentRotationZ += Mathf.Clamp(-maxAngle * Time.deltaTime * rotationSpeed, -maxAngle + initialRotationZ - currentRotationZ, 0f + initialRotationZ - currentRotationZ);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);

            rb.AddForce(new Vector3(sidewaysForce * Time.deltaTime, 0, 0), ForceMode.VelocityChange);
        } else
        {
            currentRotationZ += Mathf.Clamp((initialRotationZ - currentRotationZ) * 4f * Time.deltaTime * rotationSpeed, -Mathf.Abs(initialRotationZ - currentRotationZ), Mathf.Abs(initialRotationZ - currentRotationZ));
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);
        }

        

        if (rb.position.y < -2)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

    }
}
