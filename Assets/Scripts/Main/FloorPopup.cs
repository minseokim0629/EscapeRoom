using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FloorPopup : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public Image loadingImage;
    public Text loadingText;

    public Button startButton;

    public AudioSource audio;//bgm

    private bool _ready = false;
    public float ratio = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        loadingImage.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            ratio = ratio + 0.01f;
            loadingImage.fillAmount = Mathf.Lerp(loadingImage.fillAmount, ratio, 0.1f);
            loadingText.text = Mathf.RoundToInt(Mathf.Clamp(ratio, 0, 1) * 100) + "%";
            if (ratio >= 0.99f) {
                _ready = true;
                startButton.gameObject.SetActive(_ready);
            }
            
        }

    }

    public void OnRetry()
    {
        SceneManager.LoadScene("Scene2");
    }

    public void OnStart()
    {
        if (_ready == true)
        {
            GameManager.instance.GameStatus = GameStatus.IngameStart;
            audio.Play();
            Debug.Log("Start clicked");
        }
    }
}
