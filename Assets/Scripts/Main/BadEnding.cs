using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BadEnding : MonoBehaviour
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
    public float fades = 1.0f;
    public float time = 0;
    public bool last = false;
    private Color _targetColor;
    private List<Dialog> _dialogs;
    private int _index;

    private void Start()
    {
        for(int i=0; i<3; i++)
        {
            content[i].enabled = false;
        }

        _index = -1;
    }

    private void Update()
    {
        personColor.color = Color.Lerp(personColor.color, _targetColor, 0.05f);

        if (last)
        {
            content[2].enabled = true;
            time += Time.deltaTime;
            if (fades > 0.0f && time >= 0.1f)
            {
                fades -= 0.1f;
                fade.color = new Color(0 / 255f, 0 / 255f, 0 / 255f, fades);
                time = 0;
            }
            else if (fades <= 0.0f)
            {
                time = 0;
            }
        }
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
        _d.Add(new Dialog("Main", "틀림 없어."));
        _d.Add(new Dialog("Main", "이제까지 단서를 모두 조합해보면 범인은 최윤기가 분명해."));
        _d.Add(new Dialog("Main", "살해 도구들도 그대로 있고\n 노트북과 수첩에 범행 계획과 일정까지 전부 나와있잖아?"));
        _d.Add(new Dialog("Main", "이건 신문 1면은 물론이거니와 \n정말 TV 뉴스에까지 나올 수 있겠어!"));
        _d.Add(new Dialog("Main", "날카로운 감으로 경찰보다 먼저 범인을 알아낸 기자! \n이거 내가 직접 특종이 되겠구만."));
        _d.Add(new Dialog("Main", "좋아, 이제 무사히 나가기만 하면 되는데."));
        _d.Add(new Dialog("Main", "왜 이렇게 숨 쉬기가 힘들지?"));
        _d.Add(new Dialog("Main", "언제부터 방에 연기가 자욱한 거야? \n타는 냄새는 나지 않았는데.", "PIC_APPEAR_1"));
        _d.Add(new Dialog("Main", "콜록, 콜록.", "AUDIO_1"));//기침소리
        _d.Add(new Dialog("Main", "숨이, 안 쉬어져."));
        _d.Add(new Dialog("Main", "살려주세요...!", "AUDIO_2"));//쓰러지는소리
        _d.Add(new Dialog("Main", "누구 없어요? 여기, 사람 있어요...!"));
        _d.Add(new Dialog("Main", "살려주세요...!", "PIC_APPEAR_2"));
        _d.Add(new Dialog("Colleague", "아직 살아 있었네."));
        _d.Add(new Dialog("Main", "이 기자...! 나 숨 쉬기가 너무 힘들어. \n병원, 병원부터 가자."));
        _d.Add(new Dialog("Colleague", "그래. 내가 병원 데려다 줄게. 일단 잠시만 누워 있어. \n나도 중독되면 안 되니까 연기가 다 빠질 때까지 나가 있을게."));
        _d.Add(new Dialog("Main", "그게 무슨 소리야! 이 기자!"));
        _d.Add(new Dialog("Colleague", "여기 CCTV 없고 한적한 곳인 건 사전조사해서 알지? \n그래서 범인의 아지트라 생각했을 거고."));
        _d.Add(new Dialog("Colleague", "범행을 꾸미기 좋은 곳이라는 소리지."));
        _d.Add(new Dialog("Colleague", "USB를 놓고 가서 하마터면 들킬 뻔했지만 네가 멍청해서 다행이야."));
        _d.Add(new Dialog("Main", "(이게 다 무슨 소리야.)"));
        _d.Add(new Dialog("Main", "(숨을 못 쉬니까 너무 어지러워.)"));
        _d.Add(new Dialog("Main", "(심장이 터질 것 같아.)"));
        _d.Add(new Dialog("Colleague", "내일 기사가 올라가긴 할 거야. \n네 생각대로 1면이라든지, 특종이라든지는 아니겠지만."));
        _d.Add(new Dialog("Colleague", "그냥 욕심 많은 한 기자가 버려진 집을 취재하다가 \n실수로 불을 내 사망했다는 기사일 거야."));
        _d.Add(new Dialog("Colleague", "아, 연기가 꽤 세네. 나도 나가야겠어."));
        _d.Add(new Dialog("Colleague", "잘 자. 기삿거리 만들어줘서 고마워."));
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
            case "PIC_APPEAR_2":
                content[0].enabled = false;
                content[1].enabled = true;
                break;
            case "DIALOG_END":
                content[1].enabled = false;
                back1.SetActive(false);
                back2.SetActive(false);
                back3.SetActive(false);
                back4.SetActive(false);
                back5.SetActive(false);
                back6.SetActive(false);

                last = true;
                Button.SetActive(true);
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