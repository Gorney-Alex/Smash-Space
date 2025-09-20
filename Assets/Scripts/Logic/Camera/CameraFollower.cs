using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, 0);
    }
}
