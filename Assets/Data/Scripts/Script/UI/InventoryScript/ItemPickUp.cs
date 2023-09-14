using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;

    private void Update()
    {
        LocationRotate();
    }

    public void LocationRotate()
    {
        float Speed = 30.0f;
        //transform.Rotate(Vector3.up * Speed * Time.deltaTime);
    }
}
