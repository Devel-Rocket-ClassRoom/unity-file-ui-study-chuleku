using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public GenericWindow[] windows;

    public int currntWindowId;
    public int defaultWindowId;

    private void Awake()
    {
        foreach (var window in windows)
        {
            window.gameObject.SetActive(false);
            window.Init(this);
        }
        currntWindowId = defaultWindowId;
        windows[currntWindowId].Open();
    }

    public GenericWindow Open(int id)
    {
        windows[currntWindowId].Close();
        currntWindowId = id;
        windows[currntWindowId].Open();

        return windows[currntWindowId];
    }
    public GenericWindow Open(int id, SaveCharaterData data)
    {
        windows[currntWindowId].Close();
        currntWindowId = id;
        if (data != null)
        {
            windows[currntWindowId].Open(data);
        }
        else
        {
            windows[currntWindowId].Open();
        }

        return windows[currntWindowId];

    }
}
