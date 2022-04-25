using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGestureManager : MonoBehaviour
{
    public Vector2 delta;

    private Touch touch;
    private readonly float modifier = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                delta = new Vector2(touch.deltaPosition.x * modifier, touch.deltaPosition.y * modifier);
            }
            else
            {
                delta = Vector2.zero;
            }
        }
    }
}
