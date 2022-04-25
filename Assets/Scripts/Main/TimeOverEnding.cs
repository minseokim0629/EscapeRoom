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
        _d.Add(new Dialog("Main", "���� ���� �𸣰ھ�. ���� ��������."));
        _d.Add(new Dialog("Main", "���Ⱑ �������� ���� �´� �ɱ�?"));
        _d.Add(new Dialog("Main", "�ܼ��鵵 �ϳ����� ���׹����̾�."));
        _d.Add(new Dialog("Main", "���ư��� ���� �����̶� �ٸ� �����鵵 ã�ƺ��߰ھ�."));
        _d.Add(new Dialog("Main", "�̿��� �����̶�簡..."));
        _d.Add(new Dialog("Main", "�׷��� �� �̷��� �� ���Ⱑ ������?"));
        _d.Add(new Dialog("Main", "�������� �濡 ���Ⱑ �ڿ��� �ž�? \nŸ�� ������ ���� �ʾҴµ�.", "PIC_APPEAR_1"));
        _d.Add(new Dialog("Main", "�ݷ�, �ݷ�.", "AUDIO_1"));//��ħ�Ҹ�
        _d.Add(new Dialog("Main", "����, �� ������."));
        _d.Add(new Dialog("Main", "����ּ���...!", "AUDIO_2"));//�������¼Ҹ�
        _d.Add(new Dialog("Main", "���� �����? ����, ��� �־��...!"));
        _d.Add(new Dialog("Main", "����ּ���...!"));
        _d.Add(new Dialog("Main", "119���� �Ű� �ؾ߰ھ�..."));
        _d.Add(new Dialog("Main", "......"));
        _d.Add(new Dialog("Main", "......"));
        _d.Add(new Dialog("Main", "��, Ʋ�Ⱦ�. ���� ������ �ʾ�. �ʹ� ��������."));
        _d.Add(new Dialog("Main", "�̷��� ���� ���� ���µ�."));
        _d.Add(new Dialog("Main", "���, �ּ���..."));
        _d.Add(new Dialog("Main", "���......"));
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