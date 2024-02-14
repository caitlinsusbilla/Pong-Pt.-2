using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float pitchChangeAmount = 0.1f; // Amount to change pitch by
    public AudioSource audioSource; // Reference to the AudioSource component
    GameObject rightPaddle;
    GameObject leftPaddle;
    Vector3 rightScale;
    Vector3 leftScale;

    void Start(){
        rightPaddle = GameObject.Find("Right Paddle");
        rightScale = rightPaddle.transform.localScale;

        leftPaddle = GameObject.Find("Left Paddle");
        leftScale = leftPaddle.transform.localScale;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the object
        if (collision.gameObject.CompareTag("Player"))
        {
            // Change the pitch of the audio
            audioSource.pitch -= pitchChangeAmount;
            //Debug.Log(audioSource.pitch);
        }

        
    }

    // Power ups
    void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("leftPower"))
        {
            other.gameObject.SetActive(false);
            rightScale.z = 5f;
            rightPaddle.transform.localScale = rightScale;
            
        }


        if (other.gameObject.CompareTag("rightPower"))
        {
            other.gameObject.SetActive(false);
            leftScale.z = 2f;
            leftPaddle.transform.localScale = leftScale;
            
        }
    }

}
