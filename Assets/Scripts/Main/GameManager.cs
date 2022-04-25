using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    FloorCheck, IngameStart, Ingame, DialogStart, Dialog, DialogEnd
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GameStatus GameStatus;

    //public ObjectOnMesh ObjectOnMesh;

    public GameObject popupFloor;
    //public GameObject popupDialog;
    //public GameObject popupHint;
    public GameObject popupInfo;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStatus = GameStatus.FloorCheck;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatus == GameStatus.FloorCheck)
        {
            popupFloor.SetActive(true);
            popupInfo.SetActive(false);
        }
        else if (GameStatus == GameStatus.IngameStart)
        {
            popupFloor.SetActive(false);
        }
       
    }
}