using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScript : MonoBehaviour
{
    public EnvironmentGenerator envGenerator;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * envGenerator.CurrentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("nextLine"))
        {
            envGenerator.GenerateNextEnvironmentWithGap();
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }
}
