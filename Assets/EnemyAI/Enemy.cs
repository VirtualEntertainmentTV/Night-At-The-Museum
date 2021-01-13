using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    //Index in array
    private int index = 0;
    //Array of locations
    public Transform[] LocationCords;

    // Adjust the speed for the Enemy
    public float speed = 10f;

    void Update()
    {
        //If index is at the last position in the array. Reset it to 0 so enemy will go to the first location
        if (index > LocationCords.Length - 2)
        {
            index = 0;
        }
        //How far the enemey will move every frame
        float step = speed * Time.deltaTime;
        //Set in what direction the enemy shall move.Vector3.MoveTowards() takes 3 parameters, first one is the origin location, second one is goal location and third one is speed.
        transform.position = Vector3.MoveTowards(transform.position, LocationCords[index].position, step);

        //If enemy distance from the next location is less than 0.001 increase index so that he goes to the next location.
        if (Vector3.Distance(transform.position, LocationCords[index].position) < 0.001f)
        {
            index++;
        }
    }

}