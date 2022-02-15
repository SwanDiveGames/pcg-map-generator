using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRooms : MonoBehaviour
{
    public LayerMask anyRoom;
    public LevelGeneration levelGeneration;

    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, anyRoom);

        if (roomDetection == null && levelGeneration.stopGeneration == true)
        {
            //spawn random room
            int rand = Random.Range(0, levelGeneration.rooms.Length);
            Instantiate(levelGeneration.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}   
