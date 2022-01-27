using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.UI;

public class PlayerScript : Agent
{
    public float Force;

    public SpikeGenerator spikeGenerator;
    public Text TextCurrentScore;
    public Text TextBestScore;


    private bool AllowJump = false;
    private bool isAlive = true;

    public Transform target;

    private Rigidbody2D rgBody;

    private const string PREFS_KEY = "BestScoreIA";

    private void Start()
    {
        rgBody = GetComponent<Rigidbody2D>();
        TextBestScore.text = "Best Score: " + PlayerPrefs.GetFloat(PREFS_KEY);
    }


    private void FixedUpdate()
    {

        if (isAlive)
        {
            RequestAction();
            AddReward(0.001f);
            TextCurrentScore.text = "Score: " + GetCumulativeReward();   
        }
     
        if(GetCumulativeReward() > 3)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        }

    }



    public override void OnEpisodeBegin()
    {
        AllowJump = false;
        isAlive = true;
        transform.localPosition = new Vector2(transform.localPosition.x, 10);
    }

    public override void OnActionReceived(Unity.MLAgents.Actuators.ActionBuffers actions)
    {
        float actionJump = Mathf.RoundToInt(actions.ContinuousActions[0]);
        float actionDown = Mathf.RoundToInt(actions.ContinuousActions[1]);
        Debug.Log("Jump: " + actionJump + " | Down: " + actionDown);

        if (actionJump > 0 && AllowJump)
        {
            Up();
        }
        else if(actionDown > 0 && !AllowJump)
        {
            Down();
        }
    }

    public override void Heuristic(in Unity.MLAgents.Actuators.ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = 0;
        continuousActions[1] = 0;

        continuousActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        continuousActions[1] = Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            AllowJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            AllowJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("spike"))
        {
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = new Color(1,0,0,1);
            isAlive = false;
            if(GetCumulativeReward() > PlayerPrefs.GetFloat(PREFS_KEY))
            {
                PlayerPrefs.SetFloat(PREFS_KEY, GetCumulativeReward());
                TextBestScore.text = "Best Score: " + GetCumulativeReward();
            }
            SetReward(-3f);
            EndEpisode();
        }

       
    }

    private void Up()
    {

        rgBody.AddForce(Vector2.up *  Force);
    }

    private void Down()
    {
        rgBody.AddForce(Vector2.down * 100);
    }
}


