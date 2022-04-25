using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogPopup : MonoBehaviour
{
    public Sprite[] personSprites;
    public Sprite bg;

    public Image personImage;
    public Image personColor;
    public Text nameText;
    public Text dialogText;
    public AudioSource audioSource;

    public Image backGroundImage;

    private Color _targetColor;
    private List<Dialog> _dialogs;
    private int _index;

    private void Start()
    {
        _dialogs = Dialog.Read();
        Debug.Log(_dialogs[1].text);
        _index = -1;
    }

    private void Update()
    {
        personColor.color = Color.Lerp(personColor.color, _targetColor, 0.05f);
    }

    public void SetDialog(Dialog dialog)
    {
        personImage.sprite = personSprites[(int)dialog.name];
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
        _index++;
        SetDialog(_dialogs[_index]);
    }

    public void VariableBehaviour(string variable)
    {
        switch (variable)
        {
            case "BG_CHANGE":
                backGroundImage.sprite = bg;
                break;
            case "DIALOG_END":
                Debug.Log("DIALOG_END");
                SceneManager.LoadScene("Scene2");
                break;
            case "AUDIO_ON":
                audioSource.Play();
                break;
            default:
                break;
        }
    }
}