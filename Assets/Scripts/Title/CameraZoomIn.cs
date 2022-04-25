using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraZoomIn : MonoBehaviour
{
    public Transform target;

    private bool _started = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (_started)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 0.05f);

            if (Vector3.Magnitude(transform.position - target.position) < 0.3f)
            {
                OnContact();
                _started = false;
            }
        }
    }

    public void OnStart()
    {
        _started = true;
       
    }

    private void OnContact()
    {
        SceneManager.LoadScene("Scene1");
    }
}
