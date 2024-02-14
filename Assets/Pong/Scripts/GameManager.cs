using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Transform ball;
    public float startSpeed = 3f;
    public GoalTrigger leftGoalTrigger;
    public GoalTrigger rightGoalTrigger;
    public TextMeshProUGUI rightText;
    public TextMeshProUGUI leftText;
    

    int leftPlayerScore;
    int rightPlayerScore;
    Vector3 ballStartPos;

    const int scoreToWin = 11;

    //---------------------------------------------------------------------------
    void Start()
    {
        rightText = GameObject.Find("RightScore").GetComponent<TextMeshProUGUI>();
        leftText = GameObject.Find("LeftScore").GetComponent<TextMeshProUGUI>(); 
        ballStartPos = ball.position;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = new Vector3(1f, 0f, 0f) * startSpeed;
    }

    //---------------------------------------------------------------------------
    public void OnGoalTrigger(GoalTrigger trigger)
    {
        // If the ball entered a goal area, increment the score, check for win, and reset the ball

        if (trigger == leftGoalTrigger)
        {
            rightPlayerScore++;
            Debug.Log($"Right player scored: {rightPlayerScore}");
            GameObject.Find("RightScore").GetComponent<TextMeshProUGUI>().SetText(rightPlayerScore.ToString());
            if (rightPlayerScore == scoreToWin)
                Debug.Log("Right player wins!");
            else
                ResetBall(-1f);
            
            // Change color
            if(rightPlayerScore % 2 == 0){
                rightText.color = Color.green;
            }else{
                rightText.color = Color.red;
            }
            
        }
        else if (trigger == rightGoalTrigger)
        {
            leftPlayerScore++;
            Debug.Log($"Left player scored: {leftPlayerScore}");
            GameObject.Find("LeftScore").GetComponent<TextMeshProUGUI>().SetText(leftPlayerScore.ToString());
            if (rightPlayerScore == scoreToWin)
                Debug.Log("Right player wins!");
            else
                ResetBall(1f);

                // Change color
            if(leftPlayerScore % 2 == 0){
                leftText.color = Color.green;
            }else{
                leftText.color = Color.red;
            }
        }
    }

    //---------------------------------------------------------------------------
    void ResetBall(float directionSign)
    {
        ball.position = ballStartPos;

        // Start the ball within 20 degrees off-center toward direction indicated by directionSign
        directionSign = Mathf.Sign(directionSign);
        Vector3 newDirection = new Vector3(directionSign, 0f, 0f) * startSpeed;
        newDirection = Quaternion.Euler(0f, Random.Range(-20f, 20f), 0f) * newDirection;

        var rbody = ball.GetComponent<Rigidbody>();
        rbody.velocity = newDirection;
        rbody.angularVelocity = new Vector3();

        // We are warping the ball to a new location, start the trail over
        ball.GetComponent<TrailRenderer>().Clear();
    }
}
