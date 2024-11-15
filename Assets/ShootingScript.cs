using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    [SerializeField] Transform canon;
    private bool isShooting = false;
    private float maxDistance = 20f;


    // Start is called before the first frame update
    void Start()
    {
        inputHandler = PlayerInputHandler.Instance;

    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.ShootTriggered && !isShooting)
            StartShooting();
        else if (!inputHandler.ShootTriggered && isShooting)
            StopShooting();
    }

    void StartShooting()
    {
        Debug.Log("Bang");
        isShooting = true;
        RaycastHit hit;
        if (Physics.Raycast(canon.position, canon.forward, out hit, maxDistance))
        {
            Debug.Log(hit.collider.name);
        }
    }
    void StopShooting()
    {
        isShooting = false;
    }
}
