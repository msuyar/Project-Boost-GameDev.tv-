using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketSpeed = 100f;
    [SerializeField] float rotationSpeed = 0.5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    Rigidbody myRigidbody;
    AudioSource rocketSound;
    // Gamedev.tv düzeni 
    // 1- Parameters (SerializedFields and other variables)
    // 2- Cache's Rigidbody vs these are gameobject parts
    // state private instance (member variables)

    void Awake()
    {
         myRigidbody = GetComponent<Rigidbody>();
         rocketSound = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
        
    }
    void StopThrusting()
    {
        rocketSound.Stop();
        mainEngineParticles.Stop();
    }

    void StartThrusting()
    {
        //bunun yerine Vector3.up da yapabilirdik
            myRigidbody.AddRelativeForce(0,rocketSpeed,0);
            if(!rocketSound.isPlaying)
            {
                rocketSound.PlayOneShot(mainEngine);
            }
            if(!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            LeftRotation();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void LeftRotation()
    {
        RotateRocket(1);
        if(!leftEngineParticles.isPlaying)
        {
            leftEngineParticles.Play();
        }
    }

    void RightRotation()
    {
        RotateRocket(-1);
        if(!rightEngineParticles.isPlaying)
        {
            rightEngineParticles.Play();
        }
    }

    void StopRotation()
    {
        rightEngineParticles.Stop();
        leftEngineParticles.Stop();
    }

    void RotateRocket(int a)
    {
        // freeze rotation so we can maunally rotate
        myRigidbody.freezeRotation = true;
        // Vector3.forward da kullanılabilir
        transform.Rotate(0, 0, a * rotationSpeed * Time.deltaTime);
        // we are turning the rotation back to normal
        myRigidbody.freezeRotation = false;
    }
}
