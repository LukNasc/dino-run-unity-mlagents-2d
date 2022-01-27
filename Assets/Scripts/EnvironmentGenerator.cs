using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGenerator : MonoBehaviour
{
    public GameObject Environment;

    private float MinSpeed = 5;
    private float MaxSpeed = 12;
    public float CurrentSpeed;

    public float SpeedMultiplier = 0.02f;

    // Start is called before the first frame update
    void Awake()
    {
        CurrentSpeed = MinSpeed;
        generateEnvironment();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentSpeed < MaxSpeed)
        {
            CurrentSpeed += SpeedMultiplier;
        }
    }

    public void generateEnvironment()
    {
        int envNumber = Random.Range(0,3);
        GameObject Env = Instantiate(Environment, transform.position, transform.rotation);
        Env.transform.GetChild(envNumber).gameObject.SetActive(true);
        Env.GetComponent<EnvironmentScript>().envGenerator = this;
    }


    public void GenerateNextEnvironmentWithGap()
    {
        Invoke("generateEnvironment", 0.1f);
    }
}
