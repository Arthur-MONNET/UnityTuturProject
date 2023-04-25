using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        // modifaction de offset x en fonction de la position du joueur ( position 0 = offset x, position 30 = offset x -1, position -30 = offset x +1)
        Vector3 newOffset = offset;
        newOffset.x = offset.x - player.position.x / 30;
        transform.position = player.position + newOffset;
    }
}
