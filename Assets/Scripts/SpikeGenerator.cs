using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGenerator : MonoBehaviour
{
    public GameObject Spike;

    public float MinSpeed;
    public float MaxSpeed;
    public float CurrentSpeed;

    public float SpeedMultiplier;


    // Start is called before the first frame update
    void Awake()
    {
        CurrentSpeed = MinSpeed;
        GenerateObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentSpeed < MaxSpeed)
        {
            CurrentSpeed += SpeedMultiplier;
        }
    }

    public void GenerateObstacle()
    {
        GameObject SpikeIns = Instantiate(Spike, transform.position, transform.rotation);
        SpikeIns.GetComponent<SpikeScript>().spikeGenerator = this;
    }


    public void GenerateObstacleWithGap()
    {
        float randomWait = Random.Range(0.1f, 1.2f);
        Invoke("GenerateObstacle", randomWait);
    }

}
