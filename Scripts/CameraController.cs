using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Tilemap currentMap;

    public int musicTrackToPlay;

    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    private float halfHeight;
    private float halfWidth;

    private bool musicStarted;

    void Start()
    {
        if (target == null)
        {
            target = PlayerController.instance.transform;
        }

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = currentMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0);
        topRightLimit   = currentMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0);

        PlayerController.instance.SetBounds(currentMap.localBounds.min, currentMap.localBounds.max);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        // Keep the camera inside the map's bounds.
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
            Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y),
            transform.position.z
        );

        if (!musicStarted)
        {
            AudioManager.instance.PlayBackgroundMusic(musicTrackToPlay);
            musicStarted = true;
        }
    }
}
