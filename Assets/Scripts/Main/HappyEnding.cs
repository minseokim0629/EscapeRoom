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
        _d.Add(new Dialog("Main", "Ʋ�� ����."));
        _d.Add(new Dialog("Main", "�������� �ܼ��� ��� �����غ��� ������ �������� �и���."));
        _d.Add(new Dialog("Main", "�� �ܼ����� ���� ������ ��ó�� \n�ٸ���� ���� �ܼ��� �Ұ��� �ž�."));
        _d.Add(new Dialog("Main", "�׷��ٸ� ���� �� ���̱� ���� ������ ���ݾ�!"));
        _d.Add(new Dialog("Main", "�� �����߰ھ�."));
        _d.Add(new Dialog("Main", "�ݷ�, �ݷ�! ���� ���� ���߳�.","AUDIO_1"));//�� ���� �Ҹ�
        _d.Add(new Dialog("Main", "������ ���߰ڴµ�."));
        _d.Add(new Dialog("Main", "�ϴ� �Ű���� �ؾ߰���. \n�� ���̷�������� ���ι̼��� �ɸ� �ž�.","AUDIO_2"));//��ȭ�Ŵ¼Ҹ�
        _d.Add(new Dialog("Main", "� ����� �� ���������� ���̷� �߾��.","AUDIO_2_off"));
        _d.Add(new Dialog("Main", "���� �� �ּ���."));
        _d.Add(new Dialog("Main", "���� ��ġ�� ������ �ڴ޵� �����... 302-17�̿�!"));
        _d.Add(new Dialog("Main", "�Ű� �߰ڴ�. �� ���ڸ� �ҷ��߰ھ�. \n�и� �ֺ����� ���� �������⸸�� ��ٸ��� ���� �ž�.","AUDIO_2"));//��ȭ
        _d.Add(new Dialog("Colleague", "��, �� ���̾�. �� �߰��� �� �־�?","AUDIO_2_off"));
        _d.Add(new Dialog("Main", "��. ���� ������ �ϴ� �� �־. \n���� ���� �� �־�? ��� ���� �ְŵ�."));
        _d.Add(new Dialog("Colleague", "��... ���Դٰ�? ��. ���� ����."));
        _d.Add(new Dialog("Main", "�� �տ��� ��."));
        _d.Add(new Dialog("Main", "���� ���Դٰ� �ϴϱ� �ƽ����ϳ�. �ʹ� �Ӻ��̴� �� �Ƴ�?"));
        _d.Add(new Dialog("Main", "��, ���� ���δ�. ���� ������ �־��ݾ�. \n���� �������� ��ü�� �������� �����̾���?"));
        _d.Add(new Dialog("Main", "�� ����! �����!"));
        _d.Add(new Dialog("Colleague", "�ϴ� ������ ������ �𸣴ϱ� �ٸ� ���� ���� ����ұ�? ������."));
        _d.Add(new Dialog("Main", "�Ƴ�. �� ������ ������ �̹� ���� ���Ⱦ�. \n�� ���ɼ��� ���� �����ϱ� ���� �Ͼ�."));
        _d.Add(new Dialog("Colleague", "���� �ϳ���� �� �����ϱ�."));
        _d.Add(new Dialog("Main", "������ ���� ��. �̰� ���� �װ� �ٹ� ���̶�� �� �˾����ϱ�."));
        _d.Add(new Dialog("Colleague", "......"));
        _d.Add(new Dialog("Main", "�ܼ��鵵 ���� ¥��� �س��� ����? ������ �־��� ���� ���� �Ͼ�� ��ó�� �ٸ�� �� ���̾�. �� ���̱� ���ؼ�."));
        _d.Add(new Dialog("Colleague", "���� �Ҹ���... �� �𸣰ڴµ�..."));
        _d.Add(new Dialog("Main", "�߻��� �������� ��. ������ ������ �ͱ��� ���� Ȯ�������ϱ�."));
        _d.Add(new Dialog("Main", "���� ������ Ǭ ����? \n�׸��� ���� ������ ������ ��ٸ��� �־��� �Ű�."));
        _d.Add(new Dialog("Colleague", "���ؾ�. ���� ���� �����Ұ�."));
        _d.Add(new Dialog("Main", "���� �տ��� ������."));
        _d.Add(new Dialog("Main", "���� �ҷ����ϱ�.","AUDIO_3"));//������ �Ҹ�
        _d.Add(new Dialog("Colleague", "���ض�ϱ�! ����! �������� �߸� �Ű��� �Ŷ�� ��!"));
        _d.Add(new Dialog("Main", "�������� �ƴ����� ���� ����������. ����������!","PIC_APPEAR_1"));//������ ����
        _d.Add(new Dialog("Colleague", "��!!!!!!!!!!!!!!!"));
        _d.Add(new Dialog("Main", "", "PIC_APPEAR_2"));//���� ������� ���� �������
        _d.Add(new Dialog("Main", "�������� �ᱹ ���ǿ��� ���ι̼� �ǰ��� �޾Ҵ�."));
        _d.Add(new Dialog("Main", "�װͰ� ���� ������ �񸮱��� ������ �� ���ڿ� ���� ���� Ư������ �ƴϴ��� �Ź��� ���� ������ ������ �� �־���."));
        _d.Add(new Dialog("Main", "���������� ������ ���� ���ͼ� \n���� ���ٸ� �̻��� ���� �Ŷ�� �ߴ�."));
        _d.Add(new Dialog("Main", "�� �Ϸ� ���� ������ ��� ���� �ǽ����ڴ� ���̴�."));
        _d.Add(new Dialog("Main", "���� �����̳� ��ȭ�� Ȯ�������� ���� �������� ���ƾ���."));
        _d.Add(new Dialog("Main", "��, ���� �Դ�."));
        _d.Add(new Dialog("Main", "��� ����. �δ��� �䰡�� ���?"));
        _d.Add(new Dialog("Main", "����, ���λ���� ���ٰ�? �䰡�� ����ī�� ȸ���鳢�� ���� �ϴٰ� ���λ���� �Ͼ�ٶ�."));
        _d.Add(new Dialog("Main", "�̰� �����߰ھ�. �и� Ư���� �ž�."));
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