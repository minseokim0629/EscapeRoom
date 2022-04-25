using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class generator : MonoBehaviour
{
    public ARRaycastManager arRaycaster;
    public GameObject[] placeObject;
    public GameObject infopopup;
    public GameObject infopopup2;//?
    public GameObject dialogpopup;
    public GameObject Button;
    public GameObject Button2;//time
    public Text contentName;
    public Text contentName2;//?
    public Text infotext;
    public Text infotext2;//?
    public Image[] content;
    public GameObject nowObject;
    public AudioSource audio;//물체 클릭하면 나는 총소리
    public AudioSource audio2;//버튼 클릭하면 나는 풍선소리
    [SerializeField] private Camera arCamera;
    public string smog = "false";
    public string box = "false";
    private Pose placementPose;
    private Pose tempPose;
    GameObject[] spawnObject = new GameObject[8];

    public GameObject p_InputField;
    public InputField p_text;
    public float LimitTime;
    public Text text_TImer;

    // Start is called before the first frame update
    void Start()
    {
        content[0].enabled = false;
        infopopup.SetActive(false);
        infopopup2.SetActive(false);
        dialogpopup.SetActive(false);
        p_InputField.SetActive(false);
        Button.SetActive(false);
        Button2.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (smog == "true")
        {
            content[0].enabled = true;
            if (box == "true")
            {
                Button.SetActive(true);
                Button2.SetActive(true);
                LimitTime -= Time.deltaTime;
                text_TImer.text = "남은 시간 \n" + Math.Truncate(LimitTime/60) + ":" + Mathf.Round(LimitTime)%60;

                if (LimitTime <= 0.0f)
                {
                    text_TImer.text = "실패";
                    SceneManager.LoadScene("Scene5");
                }

            }
        }

        if (GameManager.instance.GameStatus == GameStatus.IngameStart)
        {
            UpdateCenterObject();

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray;
                RaycastHit hitobj;

                ray = arCamera.ScreenPointToRay(touch.position);
                
                if (Physics.Raycast(ray, out hitobj))
                {
                    audio.Play();

                    if (hitobj.collider.name == "notepad_top")
                     {
                         contentName.text = "수첩";
                         infotext.text = "사용감이 있지만 산 지 얼마 안 됐는지 깨끗하다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[0];   
                     }

                     if(hitobj.collider.name == "Newspaper 1 1(Clone)")
                     {
                         contentName.text = "신문";
                         infotext.text = "좀 오래돼보이는 신문이 놓여 있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[1];
                     }

                     if(hitobj.collider.name=="Laptop 1(Clone)")
                     {
                         contentName.text = "노트북";
                         infotext.text = "노트북의 전원이 켜져있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[2];
                     }

                     if (hitobj.collider.name == "USB(Clone)")
                     {
                         contentName.text = "USB";
                         infotext.text = "8GB짜리 USB이다.\n좋은 정보라도 들어있을까?";
                         infopopup.SetActive(true);
                         nowObject = placeObject[3];
                     }

                     if (hitobj.collider.name == "Pill Bottle 1(Clone)")
                     {
                         contentName.text = "비타민 통";
                         infotext.text = "비타민 통이 떨어져 있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[4];
                     }

                     if (hitobj.collider.name == "Pencil(Clone)")
                     {
                         contentName.text = "연필";
                         infotext.text = "연필이 떨어져 있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[5];
                     }

                     if (hitobj.collider.name == "envelope_fan_1575")
                     {
                         contentName.text = "청구서";
                         infotext.text = "한쪽 구석에 청구서가 쌓여 있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[6];
                     }

                      if (hitobj.collider.name == "Suitcase_24_hours_Top_Cube_1")
                     {
                         contentName.text = "검은 가방";
                         infotext.text = "내용물을 알 수 없는 커다란 검은 가방이 놓여 있다.";
                         infopopup.SetActive(true);
                         nowObject = placeObject[7];
                     }
                 }
             }
         }
     }


    private void UpdateCenterObject()
    {
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRaycaster.Raycast(screenCenter, hits, TrackableType.Planes);

        List<ARRaycastHit> hits1 = new List<ARRaycastHit>();
        screenCenter += new Vector3(300, 300, 300);
        arRaycaster.Raycast(screenCenter, hits1, TrackableType.Planes);

        List<ARRaycastHit> hits2 = new List<ARRaycastHit>();
        screenCenter += new Vector3(-1000, 0, -1000);
        arRaycaster.Raycast(screenCenter, hits2, TrackableType.Planes);

        List<ARRaycastHit> hits3 = new List<ARRaycastHit>();
        screenCenter += new Vector3(700, 0, 3000);
        arRaycaster.Raycast(screenCenter, hits3, TrackableType.Planes);

        List<ARRaycastHit> hits4 = new List<ARRaycastHit>();
        screenCenter += new Vector3(200, 0, -1600);
        arRaycaster.Raycast(screenCenter, hits4, TrackableType.Planes);

        List<ARRaycastHit> hits5 = new List<ARRaycastHit>();
        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        screenCenter += new Vector3(-500, 0, 0);
        arRaycaster.Raycast(screenCenter, hits5, TrackableType.Planes);

        List<ARRaycastHit> hits6 = new List<ARRaycastHit>();
        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        screenCenter += new Vector3(300, 0, -1000);
        arRaycaster.Raycast(screenCenter, hits6, TrackableType.Planes);

        List<ARRaycastHit> hits7 = new List<ARRaycastHit>();
        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        screenCenter += new Vector3(1500, 0, -2000);
        arRaycaster.Raycast(screenCenter, hits7, TrackableType.Planes);

        if (hits.Count > 0 && hits1.Count > 0 && hits2.Count > 0 && hits3.Count > 0 && hits4.Count>0 && hits5.Count>0 && hits6.Count>0 &&hits7.Count>0)
        {
            if (!spawnObject[0])
            {
                   placementPose = hits[0].pose;
                   spawnObject[0] = Instantiate(placeObject[0], placementPose.position, placementPose.rotation);
                   placeObject[0].SetActive(true);
            }

            if (!spawnObject[1])
            {
                placementPose = hits1[0].pose;
                placementPose.rotation = Quaternion.Euler(new Vector3(90, 90, 0));
                placeObject[1].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                spawnObject[1] = Instantiate(placeObject[1], placementPose.position, placementPose.rotation);
                placeObject[1].SetActive(true);
            }

            if (!spawnObject[2])
            {
                placementPose = hits2[0].pose;
                placementPose.rotation = Quaternion.Euler(new Vector3(-90, 180, 0));
                spawnObject[2] = Instantiate(placeObject[2], placementPose.position, placementPose.rotation);
                placeObject[2].SetActive(true);
            }

            if (!spawnObject[3])
            {
                placementPose = hits3[0].pose;
                placeObject[3].transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                spawnObject[3] = Instantiate(placeObject[3], placementPose.position, placementPose.rotation);
                placeObject[3].SetActive(true);
            }

            if(!spawnObject[4])
            {
                placementPose = hits4[0].pose;
                placeObject[4].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                spawnObject[4] = Instantiate(placeObject[4], placementPose.position, placementPose.rotation);
                placeObject[4].SetActive(true);
            }

            if (!spawnObject[5])
            {
                placementPose = hits5[0].pose;
                placeObject[5].transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                spawnObject[5] = Instantiate(placeObject[5], placementPose.position, placementPose.rotation);
                placeObject[5].SetActive(true);
            }

            if (!spawnObject[6])
            {
                placementPose = hits6[0].pose;
                placeObject[6].transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
                spawnObject[6] = Instantiate(placeObject[6], placementPose.position, placementPose.rotation);
                placeObject[6].SetActive(true);
            }

            if (!spawnObject[7])
            {
                placementPose = hits7[0].pose;
                placementPose.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
                placeObject[7].transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
                spawnObject[7] = Instantiate(placeObject[7], placementPose.position, placementPose.rotation);
                placeObject[7].SetActive(true);
            }
        }
        else
        {
              placeObject[0].SetActive(true);
              placeObject[1].SetActive(true);
              placeObject[2].SetActive(true);
              placeObject[3].SetActive(true);
              placeObject[4].SetActive(true);
              placeObject[5].SetActive(true);
              placeObject[6].SetActive(true);
              placeObject[7].SetActive(true);
        }
    }

    public void Ontry()
    {
        infopopup2.SetActive(true);
        contentName2.text = "?";
        infotext2.text = "범인의 이름을 입력해주세요.";
    }

    public void Test()
    {
        if (p_text.text == "이윤석")
        {
            SceneManager.LoadScene("Scene4");
        }
        else if(p_text.text=="최윤기")
        {
            SceneManager.LoadScene("Scene3");
        }
        else if (p_text.text != "")
        {
            infotext2.text = "다시 입력해주세요.";
            LimitTime /= 2;
        }
    }

    public void OnClose()
    {
        audio2.Play();
        infopopup.SetActive(false);
        p_InputField.SetActive(false);
        infopopup2.SetActive(false);
        
    }

    public void Onout()
    {
        p_InputField.SetActive(true);
    }
    public void OnInteract()
    {
        audio2.Play();
        if (nowObject.name == "Notepad")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "notepad";
        }
        
        if(nowObject.name == "Newspaper 1 1")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "newspaper";
        }

        if(nowObject.name == "Laptop 1")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "laptop";
        }

        if(nowObject.name=="USB")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "usb";
        }

        if(nowObject.name=="Pill Bottle 1")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName= "pill";
        }

        if (nowObject.name == "Pencil")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "pencil";
        }

        if(nowObject.name== "envelope_fan_1575 Variant")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "envelope";
        }

        if (nowObject.name == "1 1 Variant")
        {
            infopopup.SetActive(false);
            dialogpopup.SetActive(true);
            GameObject.Find("Popup_Dialog").GetComponent<DialogPopup2>().objectName = "bag";
        }
    }
}