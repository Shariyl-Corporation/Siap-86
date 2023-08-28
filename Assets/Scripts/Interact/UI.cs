using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public static Texture2D mouseCursor, hoverCursor;

    public BaseButton chatButton;
    public BaseButton interrogateButton;

    public Text dateBox, timeBox;

    int hour, minute;

    void Start() {
        mouseCursor = Resources.Load("Cursor") as Texture2D;
        hoverCursor = Resources.Load("CursorHover") as Texture2D;
        Cursor.SetCursor(mouseCursor, new Vector2(0, 0), CursorMode.Auto);

        int month = Random.Range(1, 12);
        string monthString = month < 10 ? "0" + month.ToString("D") : month.ToString("D");
        int day = Random.Range(1, 31);
        string dayString = day < 10 ? "0" + day.ToString("D") : day.ToString("D");
        dateBox.text = monthString + "/" + dayString;

        timeBox.text = "05:00";
        hour = 5;
        minute = 0;
    }

    void Update()
    {

    }

    public static void changeCursor(bool hover)
    {
        Cursor.SetCursor(hover ? hoverCursor : mouseCursor, hover ? new Vector2(0.5f, 0.5f) : new Vector2(0, 0), CursorMode.Auto);
    }

    public void incrementTime()
    {
        minute += 5;
        if (minute == 60)
        {
            hour += 1;
            minute = 0;
        }

        string minuteString = minute < 10 ? "0" + minute.ToString("D") : minute.ToString("D");
        string hourString = hour < 10 ? "0" + hour.ToString("D") : hour.ToString("D");

        timeBox.text = hourString + ":" + minuteString;
    }
}