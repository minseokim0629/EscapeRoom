using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public TouchGestureManager touchGestureManager;

    [Range(1, 20)]
    public int range;

    private void FixedUpdate()
    {
        var t = transform;

        t.position += Vector3.right * touchGestureManager.delta.x;
        t.position = new Vector3(Mathf.Clamp(t.position.x, -range, range), t.position.y, t.position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
