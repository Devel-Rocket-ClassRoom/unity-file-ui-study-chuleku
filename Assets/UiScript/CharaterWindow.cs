using UnityEngine;
using UnityEngine.UI;

public class CharaterWindow : GenericWindow
{
    public Button itembutton;

    public Uicharater charater;
    public override void Open()
    {
        base.Open();
    }
    public override void Close()
    {
        base.Close();
    }

    public void OnItemButtonClick()
    {
        windowManager.Open(1);
    }
}
