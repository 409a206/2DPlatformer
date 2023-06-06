using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1;
    public float yMargin = 1;
    public float xSmooth = 4;
    public float ySmooth = 4;

    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    private Transform player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    bool CheckXMargin() {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckYMargin() {
        return Mathf.Abs(transform.position.y - player.position.y) > yMargin;
    }

    private void FixedUpdate() {
        TrackPlayer();
    }

    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if(CheckXMargin()) {
            targetX = Mathf.Lerp(transform.position.x, player.transform.position.x, xSmooth * Time.deltaTime);
        }

        if(CheckYMargin()) {
            targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);
        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
