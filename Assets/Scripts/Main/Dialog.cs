using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Person
{
    Main, Colleague
}

public struct ER_Colors
{
    public static readonly Color lightblue = new Color(0.8117647f, 0.9019608f, 0.9419608f);
    public static readonly Color lightred = new Color(0.9f, 0.64f, 0.64f);
    public static readonly Color lightgreen = new Color(0.6392157f, 0.9f, 0.6392157f);
}

public class Dialog
{
    public static string path = "DialogData/dialogdata";

    public static Dialog NothingHappened = new Dialog("Main", "아무 일도 일어나지 않았다.", "DIALOG_CUT");

    public Person name;
    public string text;
    public string[] variables;

    public string Name
    {
        get
        {
            switch (name)
            {
                case Person.Colleague:
                    return "동료 기자";
                case Person.Main:
                    return "나";
            }

            return "";
        }
    }

    public Color Color
    {
        get
        {
            switch (name)
            {
                case Person.Colleague:
                    return ER_Colors.lightblue;
                case Person.Main:
                    return ER_Colors.lightgreen;
                
            }

            return ER_Colors.lightblue;
        }
    }

    public Dialog(string name, string text, params string[] variables)
    {
        switch (name)
        {
            case "Main":
                this.name = Person.Main;
                break;
            case "Colleague":
                this.name = Person.Colleague;
                break;
            
        }

        this.text = text;
        this.variables = variables;
    }

    public static List<Dialog> Read()
    {
        var output = new List<Dialog>();

        TextAsset asset = Resources.Load(path) as TextAsset;

        var text = asset.text;
        var dialogRaws = text.Split('\n');


        foreach (var dialogRaw in dialogRaws)
        {
            var edited = dialogRaw;
            edited = edited.Replace("\n", "");
            edited = edited.Replace('*', '\n');
            edited = edited.Replace("\r", "");

            var dialogData = edited.Split(',');

            for (int i = 0; i < dialogData.Length; i++)
            {
                dialogData[i] = dialogData[i].Replace('^', ',');
            }


            if (dialogData.Length < 2)
            {
                continue;
            }

            var dialog = new Dialog(dialogData[0], dialogData[1], dialogData.Length < 3 ? null : dialogData.Skip(2).ToArray());
            Debug.Log(dialog.name);
            output.Add(dialog);
        }

        return output;
    }
}