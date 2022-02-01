using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// READ ME!
// This script is to be added to the main camera object inside Unity.
// The target object is to be set to whichever object we wish to track.
// The farBackground and middleBackground objects will be images that fill up the background and exist
// on different z coordinates.
// The minHeight and maxHeight values are to be set manually in Unity. Drag the camera to the height
// don't want it to go past and assign the values to the variables in the Unity Inspector.
// What I mean by this is, drag the camera in the Unity scene editor to a minimum point that you
// don't want it to go past. Whatever the main camera's y value is at that position will be minHeight.
// Do vice versa for maxHeight, drag the main camera to whichever height you want it to stop at and plug in
// the y value shown in the transform element of the main camera object into the max height variable in the
// camera controller script.

public class CameraControl : MonoBehaviour
{
    // public transform variable called target, can be player or other characters.
    public Transform target;

    // variables for background elements
    public Transform farBackground; // middleBackground; 

    // create variables to keep track of background movement
    private float lastXPos;

    public float minHeight, maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {   
        // Alter values put in for x and y to determine where the camera is positioned.
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Background elements will move at some speed relative to the camera movement
        float amountToMoveX = transform.position.x - lastXPos;

        // farBackground will move at the speed of the character
        // middleBackground will move at half the speed of the character
        // 0f is in place of the y and z positional arguments as we don't want to move those values yet
        farBackground.position += new Vector3(amountToMoveX, 0f, 0f);
        // middleBackground.position += new Vector3(amountToMoveX * .5f, 0f, 0f);

        lastXPos = transform.position.x;
    }
}
