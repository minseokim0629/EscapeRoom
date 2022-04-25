using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeOverEnding : MonoBehaviour
{
    public Sprite[] personSprites;
    public Image[] content;
    public Image personImage;
    public Image personColor;
    public Text nameText;
    public Text dialogText;
    public AudioSource[] audioSource;
    public GameObject infopopup;
    public GameObject Button;
    public GameObject back1;
    public GameObject back2;
    public GameObject back3;
    public GameObject back4;
    public GameObject back5;
    public GameObject back6;

    public Image fade;
    private Color _targetColor;
    private List<Dialog> _dialogs;
    private int _index;


    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            content[i].enabled = false;
        }
        _index = -1;
    }

    private void Update()
    {
        personColor.color = Color.Lerp(personColor.color, _targetColor, 0.05f);
    }


    public void SetDialog(Dialog dialog)
    {

        personImage.sprite = personSprites[(int)dialog.name];

        if ((int)dialog.name == 1)
        {
            RectTransform rect = (RectTransform)personImage.transform;
            rect.anchoredPosition = new Vector2(-460, 400);
            rect.sizeDelta = new Vector2(600, 500);
        }
        else if ((int)dialog.name == 0)
        {
            RectTransform rect = (RectTransform)personImage.transform;
            rect.anchoredPosition = new Vector2(20, 400);
            rect.sizeDelta = new Vector2(450, 450);
        }
        _targetColor = dialog.Color;

        nameText.text = dialog.Name;
        dialogText.text = dialog.text;

        if (dialog.variables == null)
        {
            return;
        }

        foreach (var variable in dialog.variables)
        {
            VariableBehaviour(variable);
        }
    }

    public void OnPopupClick()
    {
        Change();
        _index++;
        SetDialog(_dialogs[_index]);
    }

    public void Change()
    {
        var _d = new List<Dialog>();
        _d.Add(new Dialog("Main", "이젠 정말 모르겠어. 누가 범인인지."));
        _d.Add(new Dialog("Main", "여기가 용의자의 집은 맞는 걸까?"));
        _d.Add(new Dialog("Main", "단서들도 하나같이 뒤죽박죽이야."));
        _d.Add(new Dialog("Main", "돌아가서 제보 내용이랑 다른 정보들도 찾아봐야겠어."));
        _d.Add(new Dialog("Main", "이웃의 증언이라든가..."));
        _d.Add(new Dialog("Main", "그런데 왜 이렇게 숨 쉬기가 힘들지?"));
        _d.Add(new Dialog("Main", "언제부터 방에 연기가 자욱한 거야? \n타는 냄새는 나지 않았는데.", "PIC_APPEAR_1"));
        _d.Add(new Dialog("Main", "콜록, 콜록.", "AUDIO_1"));//기침소리
        _d.Add(new Dialog("Main", "숨이, 안 쉬어져."));
        _d.Add(new Dialog("Main", "살려주세요...!", "AUDIO_2"));//쓰러지는소리
        _d.Add(new Dialog("Main", "누구 없어요? 여기, 사람 있어요...!"));
        _d.Add(new Dialog("Main", "살려주세요...!"));
        _d.Add(new Dialog("Main", "119에라도 신고를 해야겠어..."));
        _d.Add(new Dialog("Main", "......"));
        _d.Add(new Dialog("Main", "......"));
        _d.Add(new Dialog("Main", "아, 틀렸어. 앞이 보이지 않아. 너무 어지러워."));
        _d.Add(new Dialog("Main", "이렇게 죽을 수는 없는데."));
        _d.Add(new Dialog("Main", "살려, 주세요..."));
        _d.Add(new Dialog("Main", "살려......"));
        _d.Add(new Dialog("Main", "", "DIALOG_END"));
        _dialogs = _d;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Title");
    }

    public void VariableBehaviour(string variable)
    {
        switch (variable)
        {
            case "PIC_APPEAR_1":
                content[0].enabled = true;
                break;
            case "DIALOG_END":
                content[0].enabled = false;
                back1.SetActive(false);
                back2.SetActive(false);
                back3.SetActive(false);
                back4.SetActive(false);
                back5.SetActive(false);
                back6.SetActive(false);

                fade.GetComponent<Animator>().SetBool("fade_out", true);
                Button.SetActive(true);
                content[1].enabled = true;
                break;
            case "AUDIO_1":
                audioSource[0].Play();
                break;
            case "AUDIO_2":
                audioSource[1].Play();
                break;
            default:
                break;
        }
    }
}