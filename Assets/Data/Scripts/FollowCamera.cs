using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget = null;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(followTarget);
        myDir = transform.position - followTarget.position;
    }

    Vector3 myDir = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        transform.position = followTarget.position + myDir;
    }
}