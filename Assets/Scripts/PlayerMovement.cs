using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float sidewaysForce = 500f;
    [SerializeField] float rotationSpeed = 4.0f;
    [SerializeField] float maxAngle = 45f;
    // particules left
    [SerializeField] ParticleSystem leftParticles;
    // particules right
    [SerializeField] ParticleSystem rightParticles;
    [SerializeField] int angleParticlesShow = 20;

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
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);

            currentRotationZ = Mathf.Lerp(currentRotationZ, initialRotationZ + maxAngle, Time.deltaTime * rotationSpeed);
            rb.AddForce(new Vector3(-sidewaysForce * Time.deltaTime, 0, 0), ForceMode.VelocityChange);
        } else if (Input.GetKey(KeyCode.D))
        {
            currentRotationZ = Mathf.Lerp(currentRotationZ, initialRotationZ - maxAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);
            rb.AddForce(new Vector3(sidewaysForce * Time.deltaTime, 0, 0), ForceMode.VelocityChange);
        } else
        {
            currentRotationZ = Mathf.Lerp(currentRotationZ, initialRotationZ, 0.1f);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, currentRotationZ);
        }

        if(currentRotationZ > initialRotationZ + maxAngle - angleParticlesShow)
        {
            // show left particles
            leftParticles.Play();
            rightParticles.Play();
        } else if (currentRotationZ < initialRotationZ - maxAngle + angleParticlesShow)
        {
            // show right particles
            rightParticles.Play();
            leftParticles.Play();
        } else
        {
            // hide particles
            leftParticles.Stop();
            rightParticles.Stop();
        }

        

        if (rb.position.y < -2)
        {
            FindObjectOfType<GameManager>().EndGame();
        }

    }
}
