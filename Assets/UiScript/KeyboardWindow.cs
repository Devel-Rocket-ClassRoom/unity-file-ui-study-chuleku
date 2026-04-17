using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyboardWindow : GenericWindow
{
    public TextMeshProUGUI stringText;
    private string[] textMasage = new string[8];
    private int currentstring;
    private int maxstring = 8;
    private float t;
    private float timer = 0.5f;
    private bool onOffChack;
    private string laststring;

    public override void Open()
    {
        base.Open();
        stringText.text = "_";
        t = 0;
        onOffChack = true;
        laststring = string.Empty;
        OnCancel();
    }
    public override void Close()
    {
        base.Close();
    }

    private void Update()
    {
        t+= Time.deltaTime;
        if(onOffChack&&t>timer)
        {
            t = 0;
            stringText.text = $"{laststring}";
            onOffChack=false;
        }
        else if(!onOffChack&&t>timer&&currentstring<8) 
        {
            t = 0;
            stringText.text = $"{laststring}_";
            onOffChack=true;
        }

    }
    public void OnDelete()
    {
        if (currentstring>0)
        {
            currentstring--;
            textMasage[currentstring] = string.Empty;
            laststring = string.Empty;
            foreach (string s in textMasage)
            {
                laststring = laststring + s;
                stringText.text = laststring;
            }
        }  
    }
    public void OnCancel()
    {
        for(int i = textMasage.Length-1; i >= 0;i--)
        {
            textMasage[i] = string.Empty;
        }
        laststring = string.Empty;
        foreach (string s in textMasage)
        {
            laststring = laststring + s;
            stringText.text = laststring;
        }
        currentstring = 0;
    }
    public void InputKey(string input)
    {
        if(currentstring<maxstring)
        {
           laststring = string.Empty;
           textMasage[currentstring] = input;
           foreach (string s in textMasage)
           {
               laststring = laststring + s;
               stringText.text = laststring;
           }
           currentstring++;
        }
    }
    public void OnAccept()
    {
        windowManager.Open(0);
    }
}

