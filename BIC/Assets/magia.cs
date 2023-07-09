using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magia : MonoBehaviour
{
    public GameObject foxling;
    // Start is called before the first frame update
    void Start()
    {
        foxling = GameObject.Find("foxling");
        transform.position = foxling.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
