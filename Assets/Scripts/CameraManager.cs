using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private Camera camera;

    private Vector3 moveStartPosition;
    private bool moveStarted;

    public Vector2 minPos;
    public Vector2 maxSize;
    public Vector2 minSize;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        camera.orthographicSize -= BattleManager.input.ZoomSpeed();

        if (BattleManager.input.CameraMove() != Vector3.zero)
        {
            if (!moveStarted)
            {
                Debug.Log("Move Start, " + BattleManager.input.CameraMove());
                // Beginning of move
                moveStartPosition = camera.transform.position;
                moveStarted = true;
            }
            camera.transform.position = BattleManager.input.CameraMove() + moveStartPosition;
        }

        if (BattleManager.input.CameraMoveEnded())
        {
            Debug.Log("Move End");
            moveStarted = false;
        }

        FitCameraToWorld();
    }

    public Vector2 GetCameraSize()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        return new Vector2(cameraHeight * screenAspect, cameraHeight);
    }

    public Vector2 GetCameraBottomLeft()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float halfCameraHeight = camera.orthographicSize;
        float halfCameraWidth = halfCameraHeight * screenAspect;
        return new Vector2(transform.position.x - halfCameraWidth, transform.position.y - halfCameraHeight);
    }

    public void FitCameraToWorld()
    {
        float maxX = maxSize.x;
        float maxY = maxSize.y;
        float minX = minSize.x;
        float minY = minSize.y;

        float maxOrthographicSize = Mathf.Min(maxX, maxY);
        float minOrthographicSize = Mathf.Max(minX, minY);

        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize, minOrthographicSize, maxOrthographicSize);

    }
}
