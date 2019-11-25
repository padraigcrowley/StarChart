using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InputManager : Singleton<GameplayManager>
{
  private Vector3 startTouchPos, endTouchPos;
  private float minSwipeDistanceThreshold = 1.0f;
 
  
  void Start()
  {
    print("Starting");

    
  }

  // Update is called once per frame
  void Update()
  {
    DetectTouch(); //detect and handle the screen touch/mouse clicks
    DetectKeys();
    
  }

  private void DetectTouch()
  {
    // Handle native touch events
    foreach (Touch touch in Input.touches)
    {
      HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
    }

    // Simulate touch events from mouse events
    if (Input.touchCount == 0)
    {
      if (Input.GetMouseButtonDown(0))
      {
        HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
      }
      if (Input.GetMouseButton(0))
      {
        HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
      }
      if (Input.GetMouseButtonUp(0))
      {
        HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
      }
    }
  }

  private void DetectKeys()
  {
    
  }

    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
  {
    
    switch (touchPhase)
    {
      case TouchPhase.Began:
        // TODO
        startTouchPos = touchPosition;
        //Debug.Log("startTouchPos: " + startTouchPos);
        //print("Objects LocalRot: " + objectToRotate.transform.localRotation + "Objects Rotation: " + objectToRotate.transform.rotation);

        //Debug.Log("Q: " + mouseClickQueue.ToString());
        break;
      case TouchPhase.Moved:
        // TODO
        break;
      case TouchPhase.Ended:
        endTouchPos = touchPosition;
        //Debug.Log("endTouchPos" + endTouchPos);
				Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
				// RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
				if (hitInfo)
				{
					//Debug.Log($"Object Name {hitInfo.transform.gameObject.name}");
					GameplayManager.Instance.cardTouchProcessed = false;
					GameplayManager.Instance.lastTouchedGameObject = hitInfo.transform.gameObject;
					// Here you can check hitInfo to see which collider has been hit, and act appropriately.
				}
				break;
    }
  }

  
  
}
