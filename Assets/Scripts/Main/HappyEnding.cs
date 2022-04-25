using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HappyEnding : MonoBehaviour
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
        for (int i = 0; i < 2; i++)
        {
            content[i].enabled = false;
        }
        Button.SetActive(false);
        _index = -1;
    }

    
    private void Update()
    {
        personColor.color = Color.Lerp(personColor.color, _targetColor, 0.05f);

        if (last)
        {
            content[1].enabled = true;
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
        _d.Add(new Dialog("Main", "이제까지 단서를 모두 조합해보면 범인은 이윤석이 분명해."));
        _d.Add(new Dialog("Main", "이 단서들은 그저 범인인 것처럼 \n꾸며놓은 거짓 단서에 불과할 거야."));
        _d.Add(new Dialog("Main", "그렇다면 여긴 날 죽이기 위한 함정인 거잖아!"));
        _d.Add(new Dialog("Main", "얼른 나가야겠어."));
        _d.Add(new Dialog("Main", "콜록, 콜록! 정말 죽을 뻔했네.","AUDIO_1"));//문 여는 소리
        _d.Add(new Dialog("Main", "병원을 가야겠는데."));
        _d.Add(new Dialog("Main", "일단 신고부터 해야겠지. \n날 죽이려들었으니 살인미수는 걸릴 거야.","AUDIO_2"));//전화거는소리
        _d.Add(new Dialog("Main", "어떤 사람이 절 유독가스로 죽이려 했어요.","AUDIO_2_off"));
        _d.Add(new Dialog("Main", "빨리 와 주세요."));
        _d.Add(new Dialog("Main", "여기 위치는 서수시 박달동 우수로... 302-17이요!"));
        _d.Add(new Dialog("Main", "신고도 했겠다. 이 기자를 불러야겠어. \n분명 주변에서 내가 쓰러지기만을 기다리고 있을 거야.","AUDIO_2"));//전화
        _d.Add(new Dialog("Colleague", "어, 나 앞이야. 뭐 발견한 건 있어?","AUDIO_2_off"));
        _d.Add(new Dialog("Main", "응. 같이 봤으면 하는 게 있어서. \n지금 와줄 수 있어? 잠깐 나와 있거든."));
        _d.Add(new Dialog("Colleague", "아... 나왔다고? 응. 지금 갈게."));
        _d.Add(new Dialog("Main", "문 앞에서 봐."));
        _d.Add(new Dialog("Main", "내가 나왔다고 하니까 아쉬워하네. 너무 속보이는 거 아냐?"));
        _d.Add(new Dialog("Main", "아, 저기 보인다. 정말 가까이 있었잖아. \n내가 쓰러지면 시체라도 수거해줄 생각이었나?"));
        _d.Add(new Dialog("Main", "이 기자! 여기야!"));
        _d.Add(new Dialog("Colleague", "일단 범인이 올지도 모르니까 다른 곳에 가서 얘기할까? 위험해."));
        _d.Add(new Dialog("Main", "아냐. 내 생각에 범인은 이미 여길 버렸어. \n올 가능성은 전혀 없으니까 나만 믿어."));
        _d.Add(new Dialog("Colleague", "만에 하나라는 게 있으니까."));
        _d.Add(new Dialog("Main", "거짓말 하지 마. 이거 전부 네가 꾸민 짓이라는 거 알았으니까."));
        _d.Add(new Dialog("Colleague", "......"));
        _d.Add(new Dialog("Main", "단서들도 전부 짜깁기 해놓은 거지? 예전에 있었던 일을 지금 일어나는 일처럼 꾸며둔 것 뿐이야. 날 죽이기 위해서."));
        _d.Add(new Dialog("Colleague", "무슨 소린지... 잘 모르겠는데..."));
        _d.Add(new Dialog("Main", "발뺌할 생각하지 마. 포스겐 구입한 것까지 전부 확인했으니까."));
        _d.Add(new Dialog("Main", "지금 포스겐 푼 거지? \n그리고 내가 쓰러질 때까지 기다리고 있었던 거고."));
        _d.Add(new Dialog("Colleague", "오해야. 내가 전부 설명할게."));
        _d.Add(new Dialog("Main", "경찰 앞에서 설명해."));
        _d.Add(new Dialog("Main", "경찰 불렀으니까.","AUDIO_3"));//경찰차 소리
        _d.Add(new Dialog("Colleague", "오해라니까! 제발! 경찰한테 잘못 신고한 거라고 해!"));
        _d.Add(new Dialog("Main", "오해인지 아닌지는 차차 밝혀지겠지. 경찰아저씨!","PIC_APPEAR_1"));//경찰차 사진
        _d.Add(new Dialog("Colleague", "야!!!!!!!!!!!!!!!"));
        _d.Add(new Dialog("Main", "", "PIC_APPEAR_2"));//사진 사라지고 검은 배경으로
        _d.Add(new Dialog("Main", "이윤석은 결국 재판에서 살인미수 판결을 받았다."));
        _d.Add(new Dialog("Main", "그것과 내가 폭로한 비리까지 합쳐져 이 기자에 대한 기사는 특종까진 아니더라도 신문의 작은 구석은 차지할 수 있었다."));
        _d.Add(new Dialog("Main", "병원에서는 다행히 빨리 나와서 \n몸에 별다른 이상은 없을 거라고 했다."));
        _d.Add(new Dialog("Main", "이 일로 얻은 교훈은 모든 것을 의심하자는 것이다."));
        _d.Add(new Dialog("Main", "제보 메일이나 전화도 확실해지기 전엔 움직이지 말아야지."));
        _d.Add(new Dialog("Main", "어, 메일 왔다."));
        _d.Add(new Dialog("Main", "어디 보자. 부덕리 흉가의 비밀?"));
        _d.Add(new Dialog("Main", "뭐야, 살인사건이 났다고? 흉가에 괴담카페 회원들끼리 정모를 하다가 살인사건이 일어났다라."));
        _d.Add(new Dialog("Main", "이건 가봐야겠어. 분명 특종일 거야."));
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
                break;
            case "DIALOG_END":
                back1.SetActive(false);
                back2.SetActive(false);
                back3.SetActive(false);
                back4.SetActive(false);
                back5.SetActive(false);
                back6.SetActive(false);
            
                Button.SetActive(true);
                last = true;
                break;
            case "AUDIO_1":
                audioSource[0].Play();
                break;
            case "AUDIO_2":
                audioSource[1].Play();
                break;
            case "AUDIO_2_off":
                audioSource[1].Pause();
                break;
            case "AUDIO_3":
                audioSource[2].Play();
                break;
            default:
                break;
        }
    }


}