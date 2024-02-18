using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    void Update()
    {
        transform.position = Vector3.down * Time.deltaTime;
    }
}
