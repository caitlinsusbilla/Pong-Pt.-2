using System;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float maxTravelHeight;
    public float minTravelHeight;
    public float speed;
    public float collisionBallSpeedUp = 1.5f;
    public string inputAxis;
    public bool wallOne;

    private AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        if(wallOne){
            transform.Translate(0f, 0f, Input.GetAxis("Left Paddle") * speed * Time.deltaTime);
        }
        else{
            transform.Translate(0f, 0f, Input.GetAxis("Right Paddle") * speed * Time.deltaTime);
    
        }
    }
    

    //-----------------------------------------------------------------------------
    void OnCollisionEnter(Collision other)
    {
        var paddleBounds = GetComponent<BoxCollider>().bounds;
        float maxPaddleHeight = paddleBounds.max.y;
        float minPaddleHeight = paddleBounds.min.y;

        // Get the percentage height of where it hit the paddle (0 to 1) and then remap to -1 to 1 so we have symmetry
        float pctHeight = (other.transform.position.y - minPaddleHeight) / (maxPaddleHeight - minPaddleHeight);
        float bounceDirection = (pctHeight - 0.5f) / 0.5f;
        // Debug.Log($"pct {pctHeight} + bounceDir {bounceDirection}");

        // flip the velocity and rotation direction
        Vector3 currentVelocity = other.relativeVelocity;
        float newSign = -Math.Sign(currentVelocity.x);
        float newRotSign = -newSign;;

        // Change the velocity between -60 to 60 degrees based on where it hit the paddle
        float newSpeed = currentVelocity.magnitude * collisionBallSpeedUp;
        Vector3 newVelocity = new Vector3(newSign, 0f, 0f) * newSpeed;
        newVelocity = Quaternion.Euler(0f, newRotSign * 60f * bounceDirection, 0f) * newVelocity;
        other.rigidbody.velocity = newVelocity;

        // Debug.DrawRay(other.transform.position, newVelocity, Color.yellow);
        // Debug.Break();

        // Sound effect
        audioSource.Play();
    }
}
