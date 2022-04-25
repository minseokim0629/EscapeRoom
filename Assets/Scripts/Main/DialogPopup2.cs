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
    public int note_count = 0;//��Ʈ�� ������ �������� 3��
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
    public AudioSource audio2;//ǳ���Ҹ�
    public AudioSource audio3;//��ȭ���Ҹ�
    public AudioSource audio4;//��ħ�Ҹ�
    public VideoPlayer vp;//��ø(���ʷ� ��й�ȣ ĥ�ϱ�)
    public RawImage raw;

    private void Start()
    {
        /*
         0 : �Ź� (���� ���� �޽ļ�) 0_1
         1 : �Ź� (���� ���� ��������) 0_2
         2 ~4 : ��ø (�����ÿ� �ִ� ���ε��� ��������) 1_1~1_3
         5 : ��ø ( ���� ���� ��� -50516) 1_4
         6 : ��ø (������ ������) 1_5
         7 : ��ø (���� ���� ������ ��� - usb ��� LYS0203) 1_6
         8 : ��Ʈ�� �⺻ ���ȭ�� 2_1
         9 : ��Ʈ�Ͽ��� ���ġ�� ������ �޸��� 2_2
         10 : ��Ʈ�� ���� usb ��� �� ��� ġ��� ȭ�� 2_3
         11~15 : ��Ʈ�Ͽ��� ��й�ȣ ������ �����Ǵ� ȭ�� 2_4~2_8
         16~17 : �߰��б� ���� ��Ʈ�� ȭ��(�˻� ���) 2_9~2_10
         18 : û������ �����ϴ� û���� 3
         19 : ���� ���濡 �����ϴ� �ڹ��� �̹��� 4
         20 : ���� 5
 
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
        if (num == 1) //�׳� ��Ʈ
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "ǥ���� �������⡯��� ���� �ִ�. ������ �̸��ϱ�? "));
            _d.Add(new Dialog("Main", "�̰� �� ����? ��ü �� ����� ����?","PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "������� ���������δ�...���̰� ���ٴ� ��?\n�׸���..."));
            _d.Add(new Dialog("Main", "���� �̰� ������ ǥ���� �� ������� ������� �ǰ�?\n�ƴϸ� ���� ��ȹ�� ���� ��?"));
            _d.Add(new Dialog("Main", "�̰� �и� �������� �ܼ��� �� �ž�. ������."));
            _d.Add(new Dialog("Main", "�̰� �и� �������� �ܼ��� �� �ž�. ������.", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "��ü �� ���̾�...", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "��, ���� ���̴�. �� ���ڴ� ����?", "PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "�������� ���� �͵���� ������ ���µ�.\n�ƹ����� ����� ��й�ȣ ����."));
            _d.Add(new Dialog("Main", "�� ������ ������ ���ݾ�. ���� �߿��� �Ŷ� ���������?","PIC_APPEAR_note"));
            _d.Add(new Dialog("Main", "�������μ��� �� ����� ������ �ƽ��� �Ƴ�.\n�߿��� �ܼ��� �Ŷ�� ������ ���µ�."));
            if(pencil_clicked)
                _d.Add(new Dialog("Main", "�Ʊ� �ֿ��� ������ ������ �� �� ����.\n��Ʈ�� �ٽ� ��������."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 2)//�Ź�
        {

            content[0].enabled = true;//���� ���� �޽ļ� ���
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "�����ö�� �����ݾ�?"));
            _d.Add(new Dialog("Main", "����ڵ�� ���õ� ������ Ȯ��ȴٴ� ����ε�.\n�� ��縸 ���� ���ܵ� ������ ������."));
            _d.Add(new Dialog("Main", "���� ������� �����ڵ鵵 ���� ����ڰ�,\n���� �������� ���̴ϱ� �и� ������ �����ž�."));
            _d.Add(new Dialog("Main", "�����뱳���� ����޽ļҸ� ��ߴٰ�?\n�˾Ƶ���."));
            _d.Add(new Dialog("Main", "�Ź��� �� ���� ��Ƶ׳�. �� �ǹ̰� �ִ� �ɱ�?"));
            _d.Add(new Dialog("Main", "��, �̰� ���� ����ݾ�. Ȯ���� ũ�� �̽��� ���� �ʾҳ�.","PIC_APPEAR_news"));
            _d.Add(new Dialog("Main", "���� �ð��� ��ġ�� ��Ȯ���ؼ� �׷���.\n�ƴϸ� �����̶�������."));
            _d.Add(new Dialog("Main", "����ڵ鿡 ���� ������ Ȯ���Ѵٴ� ��� ���Ŀ�\n������ ���� ���ϴ� ���� ���� �����."));
            _d.Add(new Dialog("Main", "���������� ��������."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 3)//�׳� ��Ʈ��
        {
            content[8].enabled = true;//��Ʈ�� ����ȭ��
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "������ �����ܰ� �����븸 �ִ� ����ȭ���� ���δ�."));
            _d.Add(new Dialog("Main", "���ͳ��� ����Ǿ� ���� �ʴ�."));
            _d.Add(new Dialog("Main", "�ƹ��͵� ��� �ִ� �� ���ݾ�.\n�� ���� �� �� ���� ���� ��Ʈ���ε�. ���� ���� �����ص� �ǰ�?"));
            _d.Add(new Dialog("Main", "���� �Ŷ�� �ؽ�Ʈ ���� �ϳ����̱�. �����."));
            _d.Add(new Dialog("Main", "�̰� ���� ���� ������ ���� ���ݾ�? �̰� ���� ����ϱ�?","PIC_APPEAR_laptop"));
            _d.Add(new Dialog("Main", "�̰� ����̶�� �����ڰ� �����̶�� ���Ű� �� �ž�."));
            if(usb_clicked)
                _d.Add(new Dialog("Main", "�Ʊ� �߰��ߴ� usb�� �ѹ� �����غ���?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 4) //usb
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "��Ʈ���� ã�Ƽ� �����غ���?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;

        }
        if (num == 5) //��Ÿ��
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "���� ����� �̹� ���� �Ծ����� �� �������� �ʴ�."));
            _d.Add(new Dialog("Main", "��������� ���� ���� �� ���� ��ҿ� ������ �Դ� �ǰ� ��."));
            _d.Add(new Dialog("Main", "���ᰡ �Ծ��� �Ͱ� ���� ��ǰ�̴�.\n�����Ѱ�?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 6) //usb ���� ��Ʈ��
        {
            content[10].enabled = true;//��й�ȣ�� �Է��϶�� ȭ��
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "��й�ȣ�� �Է��ؾ��ϳ�?"));
            _d.Add(new Dialog("Main", "", "PASS_APPEAR"));
            _dialogs = _d;
        }
        if (num == 7)//pencil
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "���� ������ �����ɾ� ������\n����ϴ� ���� ������ ���� ���δ�."));
            _d.Add(new Dialog("Main", "��� �� �� ������?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if (num == 8)//pencil������ ���� note
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", " �¾�! �ٷ� �̰ž�.","VID_APPEAR_note"));
            _d.Add(new Dialog("Main", "���ʷ� ��Ʈ�� ��� ĥ�غ���\n���� �ڱ����� ���忡 ���� ���������� �� �� ����."));
            _d.Add(new Dialog("Main", "'LYS0203'�̶�...\n�� �ǹ��ϴ� ����? ��й�ȣ�ΰ�?"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }
        if(num == 9)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "��� �� USB�� ��й�ȣ������.\n�� ���� ������ �ϳ��ϳ� ������."));
            _d.Add(new Dialog("Main", "�̰� �Ź�����ݾ�? �����ڰ� ���ڿ���?", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "�ֱ� �ͱ����� �ֳ�. �ٵ� ��簡 ��° ���� ������.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "����. �̰� �� ���ڰ� �� ����ݾ�?\n��ũ���ΰ�? �׷��ٱ⿣ ���� ���Ϸ� ����Ǿ� �ִµ�.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "�� �׷��� ���� ��й�ȣ�� \n�� ���� �̸� �̴ϼ��̶� �����̱���!"));
            _d.Add(new Dialog("Main", "�������� 2�� 3��. �³�."));
            _d.Add(new Dialog("Main", "�̰� �� ���� USB�� �и���.\n�� �� ���� USB�� ���⿡ �ִ� ����?", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "�׷� ��ø�� �� ���� �ǰ�? �׷��ٸ� �ű� ���� �͵���?"));
            _d.Add(new Dialog("Main", "���� �̻���."));
            _d.Add(new Dialog("Main", "�� ���ڰ� ���� ���� �ͼ� ���縦 �ϰ� ���ٰ� �ص�\n��ø�� ������ �� ��."));
            _d.Add(new Dialog("Main", "�������� ��ø�� ��й�ȣ�� ������ ����."));
            _d.Add(new Dialog("Main", "��? ���� ������ �з����� ���� ��簡 �ִµ�."));
            _d.Add(new Dialog("Main", "�̰�, ���� ���� ���� ��ǿ� ���� ���ݾ�.", "PIC_APPEAR_passwd"));
            _d.Add(new Dialog("Main", "�̹� ��簡 �� �����̶��? �׷� �̰� �� ������ �˷��� ����?"));
            _d.Add(new Dialog("Main", "������ �з����� ���� �ɷ� ����, ���� ��縦 ���� ���� �ǰ�? \nȮ���� �ֱٱ��� �̷� ������ ���� �� ���� ����."));
            
            _d.Add(new Dialog("Main", "", "Bell_sound_on"));
            _d.Add(new Dialog("Main", "��¦�̾�!"));
            _d.Add(new Dialog("Main", "�����鼭 �������� �صдٴ� �� �����߳�.\n�ϸ��͸� ū�ϳ� �� �ߴ°�."));
            _d.Add(new Dialog("Main", "��ȭ�� ���ݾ�. ������?"));
            _d.Add(new Dialog("Main", "�� ���ڳ�. ���� ���� �־ �� �´ٴ���.\n���ڱ� ���� Ǯ�ȳ�����?"));
            _d.Add(new Dialog("Main", "�ϴ� ������� �� �͵� �����ϱ� �޾ƺ���."));
            _d.Add(new Dialog("Main", "", "Bell_sound_off"));

            _d.Add(new Dialog("Main", "��������?"));
            _d.Add(new Dialog("Colleague", "����. ���� ����� ��� �Ƴ� �ؼ�.\n������ ���淡."));
            _d.Add(new Dialog("Main", "������ �����. \n�ذ� �� ������ �α�ô �ϳ� ���� ���� ���� �־ �׳� ���Ծ�."));
            _d.Add(new Dialog("Colleague", "��? ����? �ʹ� ������ �� �ƴϾ�?"));
            _d.Add(new Dialog("Main", "���� �������� �´ٸ� \n�̻��� �Ҹ��� ���� �� ���Ƽ� ���Դٰ� �� �ž�."));
            _d.Add(new Dialog("Main", "�װ� �׷���. ���� ���� �̻���."));
            _d.Add(new Dialog("Main", "���Ź����� Ȯ���� �� ������ ������ ���� �ְ�, ���⵵ ���� �־�.\n���⿡ ����� ��� �߳�?"));
            _d.Add(new Dialog("Colleague", "�����, ����� �ٵ�. �� �𸣰ڳ�. ��... \n����Ʈ�θ� ���� ���� ����?"));
            _d.Add(new Dialog("Main", "�װ� �׷��ٰ� �ĵ� ���� �װ� ���� USB�� �ְŵ�."));
            _d.Add(new Dialog("Main", "���� �װ� �����ϴ� �ž�? ���� �Ծ���?"));
            _d.Add(new Dialog("Colleague", "USB���?"));
            _d.Add(new Dialog("Main", "�ȹٷ� �����. �̹� ���� �����ϱ�. \n��ø�� ���� �ִ� �� ��� USB ���� ��й�ȣ�ݾ�."));
            _d.Add(new Dialog("Main", "��ø�� ������ �̸��� �������⡱�� ���� �ִµ� \n�� ���⿡ ��� USB ��й�ȣ�� �ִ� �ž�?"));
            _d.Add(new Dialog("Colleague", "�װ�..."));
            _d.Add(new Dialog("Main", "�̰� Ȯ���� �� �ƴϰų� �Ϻη� �߸��� ������ �� �Ŷ�� �� ������ �� ���� �Ŵϱ�."));
            _d.Add(new Dialog("Main", "Ư�� ��ڴٰ� ������ �Ŷ� ����� �� ���� ��\n�����ϰ� Ⱦ���� �ͱ��� ���� �� �����ϰ� ��� �� �ž�."));
            _d.Add(new Dialog("Main", "���� ���� ������ �� ����?\n��� ��ü�� Ư�� �� ���� �ִ� �Ŷ��."));
            _d.Add(new Dialog("Colleague", "........"));
            _d.Add(new Dialog("Colleague", "�¾�. ���� �����ϴ� �ž�.\n ��ø�� ���� �ֱ淡 ��� �ٴϴٰ� ���������� ������."));
            _d.Add(new Dialog("Colleague", "�׷��� ���ڱ� �α�ô�� ����� �������Ծ�. \n������ ��й�ȣ�� ���� �������� �������� \n�� �� �� �� ����Ʈ�ȳ� ��."));

            _d.Add(new Dialog("Colleague", "������ �ٽ� �;߰ڴ� ������ �ߴµ� �װ� Ư�� �޶�� �ϴϱ�..."));
            _d.Add(new Dialog("Colleague", "�ƹ�ư �Ͼ� ��. ���� ���� �� �װ� Ȯ���� �Ŵϱ�."));
            _d.Add(new Dialog("Colleague", "���� ������ ã�Ƴ��� ���� �ܵ����� �ް� ��� ��Ʈ����\nƯ���� �����̰ŴϿ� �������� Ż �� ���� �Ŷ�ϱ�."));
            _d.Add(new Dialog("Main", "�׷��� ������. ����."));
            _d.Add(new Dialog("Main", "�������� �� �ٶ� \n�̰� ���� ����̶�� ��� �񸮴� �� �����ְھ�."));
            _d.Add(new Dialog("Colleague", "�˰ھ�. �ϴ� �����ϴϱ� ���� �������� ����. �� �� �ɷ�."));
            _d.Add(new Dialog("Main", "����. �ۿ��� ����ϴٰ� �����ϸ� �޾�."));
            _d.Add(new Dialog("Colleague", "�׷�."));

            _d.Add(new Dialog("Main", "�� ���� �̰�... Ȯ���� �� �´� �ž�?\n������ USB�� �׷� �ɷ� ������ ������ ���� ������."));
            _d.Add(new Dialog("Main", "�� ���� �����ߴ� ����� ������?"));
            _d.Add(new Dialog("Main", "�׸��� USB�� �긮�� �ٴϸ� ���."));
            _d.Add(new Dialog("Main", "��� ���� �����ڴ� ���� ������ ���� �� ���� �����ϱ�\n���� ���Ź����� ã�Ƽ� �����߰ڴµ�."));
            _d.Add(new Dialog("Main", "���� �������� ���� �����ϱ�."));
            _d.Add(new Dialog("Main", "�ٵ� �̰� ���� ������.....? \n���� �̻��� ������ ���µ�.","PIC_APPEAR_smog"));
            _d.Add(new Dialog("Main", "���� ���� ���⵵ �ϰ�. ���� ��ü�� ������ �ִٰų�?"));
            _d.Add(new Dialog("Main", "�ƹ�ư �ұ��� ������. ���ѷ��߰ھ�."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 10)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "�� ��й�ȣ�� Ʋ�ȳ���. ������ ������ �ʾ�."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==11)//û����
        {
            content[18].enabled = true;
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "���. �̰� ��¥�� �ʹ� �������ε�.\n��ü �������� �з��� ����?"));
            _d.Add(new Dialog("Main", "�� ������ �̹� ����� ������ ������ �װ�."));
            _d.Add(new Dialog("Main", "�׷� ��Ȱ�� �Ұ����� �ٵ�. �̻���.\n���� �� ������ ������ ������ �ǰ�?"));
            _d.Add(new Dialog("Main", "���ɼ��� �ƿ� ���� ��� �ƴ�����.\n�׷��� �̿����� �̻��ϰ� �����ؼ� �������� ���� �ְ�."));
            _d.Add(new Dialog("Main", "�ϴ� �̸���, ������.\n�˾Ƶ־߰ڱ�. ������ �� �� ��."));
            _d.Add(new Dialog("Main", "�ٵ� �̻��ϰ� ��𼱰� �� ���� �̸��̶� ���̾�."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==12)//���� ����
        {
            content[19].enabled = true;
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "�ڹ��谡 �ɷ� ���ݾ�.\n���� �ڹ��踦 �ɾ�� ������� ���ܾ� �� �����̶�� ���̰���."));
            _d.Add(new Dialog("Main", "�ټ� ���� ���� ��й�ȣ��.\n��й�ȣ�� ���� �ܼ��� ������."));
            _d.Add(new Dialog("Main", "", "PASS_APPEAR"));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if(num==13)//���� ���� ��� ����
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "���� ��Ʈ�� ��ݾ��� ���� �ִ� ���ڴ�\n�� �ڹ����� ��й�ȣ����. ��� �����."));
            _d.Add(new Dialog("Main", "�̰�, ���̶� ������ݱ��ݾ�. �׸��� ����̶�... �������� ��."));
            _d.Add(new Dialog("Main", "�и��� ���࿡ ����� ������ �ž�."));;
            _d.Add(new Dialog("Main", "��û�� �ܼ��� ã�Ƴ� �� ������! ���־߰ھ�."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 14)
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "�� ��й�ȣ�� Ʋ�ȳ���. ������ ������ �ʾ�."));
            _d.Add(new Dialog("Main", "", "DIALOG_END"));
            _dialogs = _d;
        }

        if (num == 15) // �߰��б� ���� ��Ʈ�� ȭ��
        {
            var _d = new List<Dialog>();
            _d.Add(new Dialog("Main", "��, ������ ���ͳ��� ������ \n�޴��� ���ͳ��� ����� ���� ���ݾ�?"));
            _d.Add(new Dialog("Main", "����. �ֽ��� �״�. �����غ���."));
            _d.Add(new Dialog("Main", "���� ���� ��ȣ�ϰ�. ���� ũ���� �Ѻ���."));
            _d.Add(new Dialog("Main", "�ڵ� �α����� �Ǿ��ֳ�. �˻� ����� ���캼 �� �ְھ�.","PIC_APPEAR_HOT"));
            _d.Add(new Dialog("Main", "���� ���ݾ�? ��������.","PIC_APPEAR_LOG"));
            _d.Add(new Dialog("Main", "......��������? ����, �̰�. ������, ������, ������ ����?"));
            _d.Add(new Dialog("Main", "�������� ��󹫱�ε� ���Ǿ��� ���������ݾ�. \n�̰� �����ߴٴ� ��."));
            _d.Add(new Dialog("Main", "������ ���൵���� ���� �Ӹ��� �ƴ� ���� �ִٴ� ���ݾ�."));
            _d.Add(new Dialog("Main", "�̰͵� �ܼ��� �� �� �ְھ�."));
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
                    content[note_count+1].enabled = true;//������ ����鿡 ���� ����
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
                content[0].enabled = false;//���� ���� �޽ļ� �̹���
                content[1].enabled = true;//���� ���� ����
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