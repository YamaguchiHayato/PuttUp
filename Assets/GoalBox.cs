using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Goal_01")
        {
            Debug.Log("Hit");
        }
    }
}
