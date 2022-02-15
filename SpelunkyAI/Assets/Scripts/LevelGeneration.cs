using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public Transform[] startingPositions;
    public GameObject[] rooms;

    private int direction;
    public float moveAmount;

    private float timeBetweenRooms;
    public float startTimeBetweenRooms = 0.25f;

    public float minX;
    public float maxX;
    public float minY;
    public bool stopGeneration;

    public LayerMask room;

    private int lastRoomDown;

    private void Start()
    {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = Random.Range(1, 6);
    }

    private void Update()
    {
        if (timeBetweenRooms <= 0 && stopGeneration == false)
        {
            Move();
            timeBetweenRooms = startTimeBetweenRooms;
        }
        else
        {
            timeBetweenRooms -= Time.deltaTime;
        }
    }

    private void Move()
    {
        //Create room to right of current room
        if (direction == 1 || direction == 2)
        {
            lastRoomDown = 0;

            if (transform.position.x < maxX)
            {
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                //Find new direction but keep the probabilities consistent (left more likely than down)
                direction = Random.Range(1, 4);
                if (direction == 3)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        }

        //Create room to left of current room
        else if (direction == 3 || direction == 4)
        {
            lastRoomDown = 0;

            if (transform.position.x > minX)
            {
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }

        //Create room below current room
        else if (direction == 5)
        {
            lastRoomDown++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);

                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type != 3)
                {
                    if (lastRoomDown > 1)
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestroy();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }

                    else
                    {
                        roomDetection.GetComponent<RoomType>().RoomDestroy();

                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }
                
                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1, 6);
            }
            else
            {
                //Stop Level Generation
                stopGeneration = true;
            }
        }
    }
}
