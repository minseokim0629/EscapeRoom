using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class DialogPopup2 : MonoBehaviour
{
    public Sprite[] personSprites;
    public Image personImage;
    public Image personColor;
    public Image[] content;
    public Text nameText;
    public Text dialogText;
    public GameObject dialogPopup;
    public GameObject p_InputField;
    public InputField p_text;
    public string objectName = null;
    public int note_count = 0;//노트에 나오는 인적사항 3장
    public int laptop_count = 0;


    private Color _targetColor;
    private List<Dialog>_dialogs;
    private int _index;
    private bool usb_clicked = false;
    private bool laptop_clicked = false;
    private bool pencil_clicked = false;
    private bool note_clicked = false;
    private bool person_appear = false;
    private bool middle = false;
    private int l_passwd_success = 0;
    private int b_passwd_success = 0;
    public AudioSource audio2;//풍선소리
    public AudioSource audio3;//전화벨소리
    public AudioSource audio4;//기침소리
    public VideoPlayer vp;//수첩(연필로 비밀번호 칠하기)
    public RawImage raw;

    private void Start()
    {
        /*
         0 : 신문 (노인 무료 급식소) 0_1
         1 : 신문 (실종 노인 인적사항) 0_2
         2 ~4 : 수첩 (서수시에 있는 노인들의 인적사항) 1_1~1_3
         5 : 수첩 ( 검은 가방 비번 -50516) 1_4
         6 : 수첩 (찢어진 페이지) 1_5
         7 : 수첩 (연필 사용시 나오는 비번 - usb 비번 LYS0203) 1_6
         8 : 노트북 기본 배경화면 2_1
         9 : 노트북에서 비번치면 나오는 메모장 2_2
         10 : 노트북 에서 usb 사용 시 비번 치라는 화면 2_3
         11~15 : 노트북에서 비밀번호 성공시 공개되는 화면 2_4~2_8
         16~17 : 중간분기 이후 노트북 화면(검색 기록) 2_9~2_10
         18 : 청구서에 등장하는 청구서 3
         19 : 검은 가방에 등장하는 자물쇠 이미지 4
         20 : 연기 5
 
         */
        for (int i=0; i<21; i++)
        {
            content[i].enabled = false;
        }
        p_InputField.SetActive(false);
        raw.enabled = false;
        personImage.enabled = false;
        _index = -1;
    }

    private void Update()
    {
        personColor.color = Color.Lerp(personColor.color, _targetColor, 0.05f);
        
    }

    public void SetDialog(Dialog dialog)
    {
        if (person_appear)
        {
            personImage.enabled = true;
            personImage.sprite = personSprites[(int)dialog.name];
            if ((int)dialog.name == 1)
            {
                RectTransform rect = (RectTransform)personImage.transform;
                rect.anchoredPosition = new Vector2(-460, 400);
                rect.sizeDelta = new Vector2(600, 500);
            }else if ((int)dialog.name == 0)
            {
                RectTransform rect = (RectTransform)personImage.transform;
                rect.anchoredPosition = new Vector2(20, 400);
                rect.sizeDelta = new Vector2(450, 450);
            }
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
        audio2.Play();
    
        if (objectName == "notepad")
        {
            if (pencil_clicked && note_clicked)
                ChangeDialog(8);
            else 
                ChangeDialog(1);
        }
        if (objectName == "newspaper")
            ChangeDialog(2);
        if (objectName == "laptop")
        {
            if (middle)
            {
                ChangeDialog(15);
                _index++;
                SetDialog(_dialogs[_index]);
                return;
            }
    
            if (l_passwd_success == 1)
            {
                ChangeDialog(9);
                _index++;
                SetDialog(_dialogs[_index]);
                return;
            }
            else if (l_passwd_success == 2)
            {
                ChangeDialog(10);
                _index++;
                SetDialog(_dialogs[_index]);
                return;
            }

            if (usb_clicked&&laptop_clicked)
                ChangeDialog(6);
            else
                ChangeDialog(3);
        }
        if (objectName == "usb")
        {
            usb_clicked = true;
            ChangeDialog(4);
        }
        if (objectName == "pill")
            ChangeDialog(5);
        if (objectName == "pencil")
        {
            pencil_clicked = true;
            ChangeDialog(7);
        }
        if (objectName == "envelope")
            ChangeDialog(11);
        if (objectName == "bag")
        {
            if (b_passwd_success == 1)
            {
                ChangeDialog(13);
                _index++;
                SetDialog(_dialogs[_index]);
                return;
            }
            else if (b_passwd_success == 2)
            {
                ChangeDialog(14);
                _index++;
                SetDialog(_dialogs[_index]);
                return;
            }
            ChangeDialog(12);

        }
        _index++;
        SetDialog(_dialogs[_index]);
    }

    public void ChangeDialog(int num)
    {
        if (num == 1) //그냥 노트
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "표지에 ‘최윤기’라고 쓰여 있다. 주인의 이름일까? "));
            _d.Add(new Dialog("Main", "이게 다 뭐야? 대체 뭘 적어둔 거지?","PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "사람들의 공통점으로는...나이가 많다는 거?\n그리고..."));
            _d.Add(new Dialog("Main", "설마 이건 범죄의 표적이 된 사람들을 적어놓은 건가?\n아니면 범행 계획을 세울 때?"));
            _d.Add(new Dialog("Main", "이건 분명 결정적인 단서가 될 거야. 찍어두자."));
            _d.Add(new Dialog("Main", "이건 분명 결정적인 단서가 될 거야. 찍어두자.", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "대체 몇 장이야...", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "아, 이제 끝이다. 이 숫자는 뭐지?", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "이제까지 적힌 것들과는 연관이 없는데.\n아무래도 어딘가의 비밀번호 같아."));
            _d.Add(new Dialog("Main", "이 뒷장은 찢어져 있잖아. 뭔가 중요한 거라도 적어뒀을까?","PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "지금으로서는 알 방법이 없으니 아쉽게 됐네.\n중요한 단서일 거라는 예감이 오는데."));
            if(pencil_clicked)
                _d.Add(new Dialog("Main", "아까 주웠던 연필이 도움이 될 것 같아.\n노트를 다시 눌러보자."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 2)//신문
        {

            content[0].enabled = true;//노인 무료 급식소 기사
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "서수시라면 여기잖아?"));
            _d.Add(new Dialog("Main", "노숙자들과 관련된 지원이 확대된다는 기사인데.\n이 기사만 따로 남겨둔 이유가 있을까."));
            _d.Add(new Dialog("Main", "연쇄 실종사건 피해자들도 전부 노숙자고,\n여긴 용의자의 집이니까 분명 연관이 있을거야."));
            _d.Add(new Dialog("Main", "영현대교에서 무료급식소를 운영했다고?\n알아두자."));
            _d.Add(new Dialog("Main", "신문을 꽤 많이 모아뒀네. 다 의미가 있는 걸까?"));
            _d.Add(new Dialog("Main", "어, 이건 실종 기사잖아. 확실히 크게 이슈가 되진 않았네.","PIC_APPEAR_news"));
            _d.Add(new Dialog("Main", "실종 시각과 위치가 불확실해서 그런가.\n아니면 노인이라서일지도."));
            _d.Add(new Dialog("Main", "노숙자들에 대한 지원을 확대한다는 기사 이후에\n관심을 받지 못하는 노인 실종 기사라니."));
            _d.Add(new Dialog("Main", "씁쓸하지만 현실이지."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 3)//그냥 노트북
        {
            content[8].enabled = true;//노트북 바탕화면
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "브라우저 아이콘과 휴지통만 있는 바탕화면이 보인다."));
            _d.Add(new Dialog("Main", "인터넷은 연결되어 있지 않다."));
            _d.Add(new Dialog("Main", "아무것도 깔려 있는 게 없잖아.\n꽤 오래 쓴 것 같은 구식 노트북인데. 설마 벌써 포맷해둔 건가?"));
            _d.Add(new Dialog("Main", "남은 거라곤 텍스트 파일 하나뿐이군. 열어볼까."));
            _d.Add(new Dialog("Main", "이건 살해 이후 행적이 적힌 거잖아? 이게 정말 사실일까?","PIC_APPEAR_laptop"));
            _d.Add(new Dialog("Main", "이게 사실이라면 용의자가 범인이라는 증거가 될 거야."));
            if(usb_clicked)
                _d.Add(new Dialog("Main", "아까 발견했던 usb를 한번 연결해볼까?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 4) //usb
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "노트북을 찾아서 연결해볼까?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;

        }
        if (num == 5) //비타민
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "통을 열어보니 이미 많이 먹었는지 얼마 남아있지 않다."));
            _d.Add(new Dialog("Main", "유통기한이 많이 남은 걸 보니 평소에 범인이 먹던 건가 봐."));
            _d.Add(new Dialog("Main", "동료가 먹었던 것과 같은 제품이다.\n유명한가?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 6) //usb 꽂은 노트북
        {
            content[10].enabled = true;//비밀번호를 입력하라는 화면
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "비밀번호를 입력해야하네?"));
            _d.Add(new Dialog("Main", "", "PASS_APPEAR"));
            _dialogs = _d;
        }
        if (num == 7)//pencil
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "위에 먼지가 내려앉아 있지만\n사용하는 데에 문제는 없어 보인다."));
            _d.Add(new Dialog("Main", "어디에 쓸 수 있을까?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 8)//pencil누르고 난후 note
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", " 맞아! 바로 이거야.","VID_APPEAR_note"));
            _d.Add(new Dialog("Main", "연필로 노트를 살살 칠해보면\n눌린 자국으로 앞장에 뭐가 쓰였었는지 알 수 있지."));
            _d.Add(new Dialog("Main", "'LYS0203'이라...\n뭘 의미하는 거지? 비밀번호인가?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if(num == 9)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "찢어간 건 USB의 비밀번호였구나.\n자 이제 폴더를 하나하나 봐볼까."));
            _d.Add(new Dialog("Main", "이건 신문기사잖아? 용의자가 기자였나?", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "최근 것까지도 있네. 근데 기사가 어째 낯이 익은데.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "뭐야. 이거 이 기자가 쓴 기사잖아?\n스크랩인가? 그렇다기엔 워드 파일로 저장되어 있는데.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "아 그러고 보니 비밀번호가 \n이 기자 이름 이니셜이랑 생일이구나!"));
            _d.Add(new Dialog("Main", "이윤석에 2월 3일. 맞네."));
            _d.Add(new Dialog("Main", "이건 이 기자 USB가 분명해.\n왜 이 기자 USB가 여기에 있는 거지?", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "그럼 수첩도 이 기자 건가? 그렇다면 거기 적힌 것들은?"));
            _d.Add(new Dialog("Main", "뭔가 이상해."));
            _d.Add(new Dialog("Main", "이 기자가 먼저 여길 와서 취재를 하고 갔다고 해도\n수첩은 설명이 안 돼."));
            _d.Add(new Dialog("Main", "용의자의 수첩에 비밀번호를 적었을 리가."));
            _d.Add(new Dialog("Main", "어? 여기 폴더에 분류되지 않은 기사가 있는데."));
            _d.Add(new Dialog("Main", "이건, 노인 연쇄 실종 사건에 관한 거잖아.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "이미 기사가 난 내용이라고? 그럼 이걸 왜 나에게 알려준 거지?"));
            _d.Add(new Dialog("Main", "폴더로 분류되지 않은 걸로 보면, 아직 기사를 쓰지 않은 건가? \n확실히 최근까지 이런 내용의 기사는 본 적이 없고."));
            
            _d.Add(new Dialog("Main", "", "Bell_sound_on"));
            _d.Add(new Dialog("Main", "깜짝이야!"));
            _d.Add(new Dialog("Main", "들어오면서 무음모드로 해둔다는 걸 깜빡했네.\n하마터면 큰일날 뻔 했는걸."));
            _d.Add(new Dialog("Main", "전화가 왔잖아. 누구지?"));
            _d.Add(new Dialog("Main", "이 기자네. 급한 일이 있어서 못 온다더니.\n갑자기 일이 풀렸나보지?"));
            _d.Add(new Dialog("Main", "일단 물어봐야 할 것도 있으니까 받아보자."));
            _d.Add(new Dialog("Main", "", "Bell_sound_off"));

            _d.Add(new Dialog("Main", "여보세요?"));
            _d.Add(new Dialog("Colleague", "나야. 오늘 취재는 어떻게 됐나 해서.\n연락이 없길래."));
            _d.Add(new Dialog("Main", "빨리도 물어보네. \n해가 다 지도록 인기척 하나 없고 문도 열려 있어서 그냥 들어왔어."));
            _d.Add(new Dialog("Colleague", "뭐? 벌써? 너무 무모한 거 아니야?"));
            _d.Add(new Dialog("Main", "만약 집주인이 온다면 \n이상한 소리가 나는 것 같아서 들어왔다고 할 거야."));
            _d.Add(new Dialog("Main", "그건 그렇고. 여기 뭔가 이상해."));
            _d.Add(new Dialog("Main", "증거물들은 확실한 것 같은데 수도도 끊겨 있고, 전기도 끊겨 있어.\n여기에 사람이 살긴 했나?"));
            _d.Add(new Dialog("Colleague", "사람은, 살았을 텐데. 잘 모르겠네. 뭐... \n아지트로만 썼을 수도 있지?"));
            _d.Add(new Dialog("Main", "그건 그렇다고 쳐도 여기 네가 쓰던 USB가 있거든."));
            _d.Add(new Dialog("Main", "설마 네가 취재하던 거야? 여길 왔었어?"));
            _d.Add(new Dialog("Colleague", "USB라니?"));
            _d.Add(new Dialog("Main", "똑바로 얘기해. 이미 전부 봤으니까. \n수첩에 적혀 있던 거 당신 USB 폴더 비밀번호잖아."));
            _d.Add(new Dialog("Main", "수첩엔 용의자 이름인 “최윤기”가 적혀 있는데 \n왜 여기에 당신 USB 비밀번호가 있는 거야?"));
            _d.Add(new Dialog("Colleague", "그건..."));
            _d.Add(new Dialog("Main", "이거 확실한 거 아니거나 일부러 잘못된 정보를 준 거라면 나 가만히 안 있을 거니까."));
            _d.Add(new Dialog("Main", "특종 잡겠다고 조작한 거랑 사람들 돈 먹인 거\n협박하고 횡령한 것까지 전부 다 폭로하고 기사 쓸 거야."));
            _d.Add(new Dialog("Main", "펜이 제일 무서운 거 알지?\n당신 자체가 특종 될 수도 있는 거라고."));
            _d.Add(new Dialog("Colleague", "........"));
            _d.Add(new Dialog("Colleague", "맞아. 내가 취재하던 거야.\n 수첩이 놓여 있길래 들고 다니다가 습관적으로 적었어."));
            _d.Add(new Dialog("Colleague", "그런데 갑자기 인기척이 들려서 도망나왔어. \n간신히 비밀번호를 적은 페이지는 찢었지만 \n내 걸 몇 개 떨어트렸나 봐."));

            _d.Add(new Dialog("Colleague", "언젠가 다시 와야겠다 생각은 했는데 네가 특종 달라고 하니까..."));
            _d.Add(new Dialog("Colleague", "아무튼 믿어 봐. 내가 봤을 땐 그거 확실한 거니까."));
            _d.Add(new Dialog("Colleague", "아직 경찰이 찾아내기 전에 단독취재 달고 기사 터트리면\n특종은 물론이거니와 뉴스까지 탈 수 있을 거라니까."));
            _d.Add(new Dialog("Main", "그렇단 말이지. 좋아."));
            _d.Add(new Dialog("Main", "뉴스까진 안 바라도 \n이게 정말 사실이라면 당신 비리는 눈 감아주겠어."));
            _d.Add(new Dialog("Colleague", "알겠어. 일단 위험하니까 내가 그쪽으로 갈게. 얼마 안 걸려."));
            _d.Add(new Dialog("Main", "좋아. 밖에서 대기하다가 연락하면 받아."));
            _d.Add(new Dialog("Colleague", "그래."));

            _d.Add(new Dialog("Main", "이 기자 이거... 확실한 거 맞는 거야?\n떨어진 USB나 그런 걸로 봐서는 거짓말 같진 않은데."));
            _d.Add(new Dialog("Main", "왜 먼저 취재했단 사실을 숨겼지?"));
            _d.Add(new Dialog("Main", "그리고 USB를 흘리고 다니면 어떡해."));
            _d.Add(new Dialog("Main", "얘길 들어보면 용의자는 여길 완전히 버린 거 같진 않으니까\n빨리 증거물들을 찾아서 나가야겠는데."));
            _d.Add(new Dialog("Main", "정말 위험해질 수도 있으니까."));
            _d.Add(new Dialog("Main", "근데 이게 무슨 냄새지.....? \n뭔가 이상한 냄새가 나는데.","PIC_APPEAR_smog"));
            _d.Add(new Dialog("Main", "가스 냄새 같기도 하고. 설마 시체가 숨겨져 있다거나?"));
            _d.Add(new Dialog("Main", "아무튼 불길한 냄새야. 서둘러야겠어."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 10)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "이 비밀번호는 틀렸나봐. 폴더가 열리질 않아."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==11)//청구서
        {
            content[18].enabled = true;
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "잠깐. 이거 날짜가 너무 오래전인데.\n대체 언제부터 밀려둔 거지?"));
            _d.Add(new Dialog("Main", "이 정도면 이미 전기와 수도가 끊겼을 테고."));
            _d.Add(new Dialog("Main", "그럼 생활이 불가능할 텐데. 이상해.\n설마 빈 집에서 범행을 저지른 건가?"));
            _d.Add(new Dialog("Main", "가능성이 아예 없는 얘긴 아니지만.\n그래서 이웃들이 이상하게 생각해서 제보했을 수도 있고."));
            _d.Add(new Dialog("Main", "일단 이름이, 최윤기.\n알아둬야겠군. 용의자 최 모 씨."));
            _d.Add(new Dialog("Main", "근데 이상하게 어디선가 들어본 듯한 이름이란 말이야."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==12)//검은 가방
        {
            content[19].enabled = true;
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "자물쇠가 걸려 있잖아.\n굳이 자물쇠를 걸어둘 정도라면 숨겨야 할 물건이라는 뜻이겠지."));
            _d.Add(new Dialog("Main", "다섯 글자 숫자 비밀번호라.\n비밀번호에 관한 단서가 있을까."));
            _d.Add(new Dialog("Main", "", "PASS_APPEAR"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==13)//검은 가방 비번 성공
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "역시 노트에 뜬금없이 적혀 있던 숫자는\n이 자물쇠의 비밀번호였군. 어디 열어볼까."));
            _d.Add(new Dialog("Main", "이건, 줄이랑 전기충격기잖아. 그리고 비닐이랑... 더러워진 옷."));
            _d.Add(new Dialog("Main", "분명히 범행에 사용한 도구일 거야."));;
            _d.Add(new Dialog("Main", "엄청난 단서를 찾아낸 것 같은데! 찍어둬야겠어."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 14)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "이 비밀번호는 틀렸나봐. 가방이 열리질 않아."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 15) // 중간분기 이후 노트북 화면
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "아, 연결할 인터넷이 없으면 \n휴대폰 인터넷을 끌어다 쓰면 되잖아?"));
            _d.Add(new Dialog("Main", "좋아. 핫스팟 켰다. 연결해볼까."));
            _d.Add(new Dialog("Main", "연결 상태 양호하고. 이제 크롬을 켜볼까."));
            _d.Add(new Dialog("Main", "자동 로그인이 되어있네. 검색 기록을 살펴볼 수 있겠어.","PIC_APPEAR_HOT"));
            _d.Add(new Dialog("Main", "별거 없잖아? 유독가스.","PIC_APPEAR_LOG"));
            _d.Add(new Dialog("Main", "......유독가스? 뭐야, 이게. 질식제, 포스겐, 포스겐 구입?"));
            _d.Add(new Dialog("Main", "포스겐은 살상무기로도 사용되었던 유독가스잖아. \n이걸 구입했다는 건."));
            _d.Add(new Dialog("Main", "범인의 범행도구가 밧줄 뿐만은 아닐 수도 있다는 거잖아."));
            _d.Add(new Dialog("Main", "이것도 단서가 될 수 있겠어."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
    }

    public void Test() {
        if (p_text.text == "LYS0203" && objectName == "laptop")
        {
            p_InputField.SetActive(false);

            content[10].enabled = false;
            l_passwd_success = 1;
            ChangeDialog(9);
            _index = -1;
            SetDialog(_dialogs[_index]);
            return;

        }
        else if (p_text.text == "50516" && objectName == "bag")
        {
            p_InputField.SetActive(false);

            content[19].enabled = false;
            b_passwd_success = 1;
            ChangeDialog(13);
            _index = -1;
            SetDialog(_dialogs[_index]);
            return;
        }
        else if (p_text.text != "" && objectName == "laptop")
        {
            l_passwd_success = 2;
            ChangeDialog(10);
            _index = -1;
            SetDialog(_dialogs[_index]);
            return;
        }
        else if (p_text.text != "" && objectName == "bag")
        {
            b_passwd_success = 2;
            ChangeDialog(14);
            _index = -1;
            SetDialog(_dialogs[_index]);
            return;
        }
    }

    public void VariableBehaviour(string variable)
    {
        switch (variable)
        {
            
            case "DIALOG_END":
                Debug.Log("DIALOG_END");
                dialogPopup.SetActive(false);
                for (int i = 0; i < 21; i++)
                {
                    content[i].enabled = false;
                }
                p_InputField.SetActive(false);
                raw.enabled = false;
                _index = -1;
                note_count = 0;
                laptop_count=0;
                if (objectName == "notepad")
                {
                    if (!note_clicked)
                        note_clicked = true;
                }
                if(objectName=="laptop")
                {
                    if (!laptop_clicked)
                        laptop_clicked = true;
                }

                if (l_passwd_success == 1)
                {
                    middle = true;
                    generator b = GameObject.Find("Item Generator").GetComponent<generator>();
                    b.box = "true";
                }
                if (l_passwd_success==2) l_passwd_success = 0;
                if (b_passwd_success == 2) b_passwd_success = 0;
                if (person_appear)
                {
                    person_appear = false;
                    personImage.enabled = false;
                }
                break;
            case "PIC_APPEAR_note":
                note_count++;
                if(note_count==1)
                {
                    content[note_count+1].enabled = true;//실종된 사람들에 대한 정보
                }
                else 
                {
                    content[note_count].enabled = false;
                    content[note_count+1].enabled = true;
                }
                break;
            case "VID_APPEAR_note":
                raw.enabled = true;
                vp.Play();
                break;
            case "PIC_APPEAR_news":
                content[0].enabled = false;//노인 무료 급식소 이미지
                content[1].enabled = true;//실종 노인 정보
                break;
            case "PIC_APPEAR_laptop":
                content[8].enabled = false;
                content[9].enabled = true;
                break;
            case "PIC_APPEAR_smog":
                generator g = GameObject.Find("Item Generator").GetComponent<generator>();
                g.smog = "true";
                content[20].enabled = true;
                audio4.Play();
                break;
            case "PIC_APPEAR_HOT":
                content[16].enabled = true;
                break;
            case "PIC_APPEAR_LOG":
                content[16].enabled = false;
                content[17].enabled = true;
                break;
            case "PASS_APPEAR":
                p_InputField.SetActive(true);
                break;
            case "PIC_APPEAR_passwd":
                laptop_count++;
                if (laptop_count == 1)
                {
                    content[laptop_count + 10].enabled = true;
                }
                else
                {
                    content[laptop_count + 9].enabled = false;
                    content[laptop_count + 10].enabled = true;
                }
                break;
            case "Bell_sound_on":
                person_appear = true;
                content[15].enabled = false;
                audio3.Play();
                Handheld.Vibrate();
                break;
            case "Bell_sound_off":
                audio3.Pause();
                break;
            default:
                break;
        }
    }
}