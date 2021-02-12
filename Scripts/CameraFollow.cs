using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    GameObject[] players;
    private void Start()
    {
        transform.position = playerTransform.position;
    }

    private void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            print("player dead");
        }
        else
        {
            if (playerTransform != null)
            {
                float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX); //clamped = restraint this will stop the player from going to a position greater than the map on the x axis
                float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY); //clamped = restraint this will stop the player from going to a position greater than the map on the y axis
                transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX, clampedY), speed);// FUNCTION THAT LETS YOU SMOOTHLY MOVE FROM ONE POINT TO ANOTHER BASED ON A SPEED

            }
        }
    }
}
