using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFlight : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Moving speed on Y axis in local space")]
    public float speed;

    //moving the object with the defined speed
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
