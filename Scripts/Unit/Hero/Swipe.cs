using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public bool Tap { get; private set; }
    public bool SwipeLeft { get; private set; }
    public bool SwipeRight { get; private set; }
    public bool SwipeDawn { get; private set; }
    public bool SwipeUp { get; private set; }

    [SerializeField]
    private float sensevity = 125f;
    private Vector2 startTouch, swipeDelta;
    private bool isDraging = false;

    private void Update()
    {
        Tap = SwipeLeft = SwipeDawn = SwipeRight = SwipeUp = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            Tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Reset();
        }
        #endregion

        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended ||
                Input.touches[0].phase == TouchPhase.Canceled)
            {
                Reset();
            }
        }
        #endregion
        
        #region calculate 
        swipeDelta = Vector2.zero;
        if (startTouch != Vector2.zero)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        if (swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //left or right
                if (x < 0)
                    SwipeLeft = true;
                else SwipeRight = true;
            }
            else // на будущее
            {
                //up or dawn
                if (y < 0) SwipeDawn = true;
                else SwipeUp = true;
            }

            Reset();
        }
        #endregion
    }
    private void Reset()
    {
        isDraging = false;
        startTouch = swipeDelta = Vector2.zero;
    }
}
