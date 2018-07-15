// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraManager : MonoBehaviour
// {
//     private Camera camera;

//     public float recoverTime;

//     private Vector3 moveStartPosition;
//     private bool moveStarted;

//     private float screenAspect;
//     private Bounds mapBounds;
//     private Vector2 minPos;
//     private Vector2 maxSize;
//     public float minSize;
//     private float orthoVelocity = 0.0f;
//     private float xVelocity = 0.0f;
//     private float yVelocity = 0.0f;

//     // Use this for initialization
//     void Start()
//     {
//         camera = GetComponent<Camera>();
//         Transform mapTransform = BattleManager.instance.map.transform;
//         mapBounds = BattleManager.instance.map.GetComponent<BoxCollider2D>().bounds;
//         minPos = mapTransform.position - new Vector3(mapBounds.extents.x, mapBounds.extents.y);
//         maxSize = mapBounds.size;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         screenAspect = (float)Screen.width / (float)Screen.height;

//         if (BattleManager.input.ZoomSpeed() != 0)
//         {
//             camera.orthographicSize -= BattleManager.input.ZoomSpeed();
//             orthoVelocity = 0;
//         }

//         if (!StateMachine.instance.IsSelection() && BattleManager.input.IsDragging())
//         {
//             if (!moveStarted)
//             {
//                 // Beginning of move
//                 moveStartPosition = camera.transform.position;
//                 moveStarted = true;
//             }
//             camera.transform.position = BattleManager.input.DragDistance() + moveStartPosition;
//             xVelocity = 0;
//             yVelocity = 0;
//         }

//         if (BattleManager.input.DragEnded())
//         {
//             moveStarted = false;
//         }

//         FitCameraToWorld();
//     }

//     public Vector2 GetCameraSize()
//     {
//         float cameraHeight = camera.orthographicSize * 2;
//         return new Vector2(cameraHeight * screenAspect, cameraHeight);
//     }

//     public Vector2 GetCameraBottomLeft()
//     {
//         float halfCameraHeight = camera.orthographicSize;
//         float halfCameraWidth = halfCameraHeight * screenAspect;
//         return new Vector2(transform.position.x - halfCameraWidth, transform.position.y - halfCameraHeight);
//     }

//     public void FitCameraToWorld()
//     {
//         // Handle zoom
//         float maxX = maxSize.x / screenAspect / 2;
//         float maxY = maxSize.y / 2;

//         float maxOrthographicSize = Mathf.Max(maxX, maxY);

//         camera.orthographicSize = Mathf.SmoothDamp(
//             camera.orthographicSize,
//             Mathf.Clamp(camera.orthographicSize, minSize, maxOrthographicSize),
//             ref orthoVelocity,
//             recoverTime
//         );

//         // Handle out of bounds
//         Vector3 newPosition = camera.transform.position;
//         Vector2 cameraSize = GetCameraSize();

//         if (newPosition.x < minPos.x + cameraSize.x / 2.0f)
//         {
//             newPosition.x = minPos.x + cameraSize.x / 2.0f;
//         }
//         else if (newPosition.x > minPos.x + mapBounds.size.x - cameraSize.x / 2.0f)
//         {
//             newPosition.x = minPos.x + mapBounds.size.x - cameraSize.x / 2.0f;
//         }

//         if (newPosition.y < minPos.y + cameraSize.y / 2.0f)
//         {
//             newPosition.y = minPos.y + cameraSize.y / 2.0f;
//         }
//         else if (newPosition.y > minPos.y + mapBounds.size.y - cameraSize.y / 2.0f)
//         {
//             newPosition.y = minPos.y + mapBounds.size.y - cameraSize.y / 2.0f;
//         }

//         // When clamping orthographic size
//         if (camera.orthographicSize >= maxX) // X is overflowing
//         {
//             newPosition.x = BattleManager.instance.map.transform.position.x;
//         }

//         if (camera.orthographicSize >= maxY) // Y is overflowings
//         {
//             newPosition.y = BattleManager.instance.map.transform.position.y;
//         }

//         newPosition.x = Mathf.SmoothDamp(
//             camera.transform.position.x,
//             newPosition.x,
//             ref xVelocity,
//             recoverTime
//         );

//         newPosition.y = Mathf.SmoothDamp(
//             camera.transform.position.y,
//             newPosition.y,
//             ref yVelocity,
//             recoverTime
//         );

//         newPosition.z = camera.transform.position.z;

//         camera.transform.position = newPosition;

//     }

// }
