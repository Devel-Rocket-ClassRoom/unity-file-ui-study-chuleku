using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewKeyboardWindow : GenericWindow
{
    private readonly StringBuilder sb = new StringBuilder();

    public TextMeshProUGUI inputField;
    public GameObject rootKeyboard;

    public int maxCharacters = 7;

    private float timer = 0f;
    private float cursorDelay = 0.5f;
    private bool blink;
    private void Awake()
    {
        var keys = rootKeyboard.GetComponentsInChildren<Button>();
        foreach (var button in keys)
        {
            string key = button.GetComponentInChildren<TextMeshProUGUI>().text;
            button.onClick.AddListener(() => Onkey(key));
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if( timer > cursorDelay )
        {
            timer = 0f;
            blink = !blink;
            UpdateInputField();
        }
    }
    public override void Open()
    {
        sb.Clear();
        timer = 0f;
        blink = false;
        base.Open();
        UpdateInputField();

    }
    private void UpdateInputField()
    {
        bool showCursor = sb.Length < maxCharacters && !blink;
        if(showCursor)
        {
            sb.Append("_");
        }
        inputField.SetText(sb);
        if (showCursor)
        {
            sb.Length -= 1;
        }
    }
    public override void Close()
    {
        base.Close();
    }
    public void Onkey(string key)
    {
        if(sb.Length < maxCharacters)
        {
            sb.Append(key);
            UpdateInputField();
        }
    }

    public void OnCancel()
    {
        sb.Clear();
        UpdateInputField();
    }
    public void OnDelete()
    {
        if(sb.Length>0)
        {
            sb.Length -= 1;
            UpdateInputField();
        }
    }
    public void Accept()
    {
        windowManager.Open(0);
    }
}
