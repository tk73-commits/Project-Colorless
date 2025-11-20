using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos; // store initial position of the background
    public GameObject cam;
    public float parallaxEffect;    // speed at which the background should move relative to the camera

    void Start()
    {
        // set initial position of background image; only tracks x pos because it's only moving horizontally, add pos.y if you want verticality
        startPos = transform.position.x;
    }

    void FixedUpdate()
    {
        // calculate distance background moves based on camera movement
        float distance = cam.transform.position.x * parallaxEffect;
        // lower parallaxEffect = faster movement w/ camera
        // 0 = move w/ camera; 1 = doesn't move

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
