using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] Transform secondArrow, minuteArrow, hourArrow;
    [SerializeField] TMP_InputField digitalClock;
    public static bool IsMovingArrows = false;
    public bool IsDigital { get; set; }
    private void Update()
    {
        if (IsMovingArrows) SetTimeByArrows();
        else if (IsDigital)
        {
            if (DateTime.TryParse(digitalClock.text, out DateTime time))
                GlobalTime.Time = time;
        }
        else
        {
            SetAllArrows(GlobalTime.Time);
            digitalClock.text = GlobalTime.Time.ToString("HH:mm:ss");
        }
    }
    void SetTimeByArrows()
    {
        int Seconds = GetTimeByArrow(secondArrow, 60);
        int Minutes = GetTimeByArrow(minuteArrow, 60);
        int Hours = GetTimeByArrow(hourArrow, 12);
        try
        {
            GlobalTime.Time = new(2024, 9, 1, Hours, Minutes, Seconds);
        }
        catch { }
        digitalClock.text = GlobalTime.Time.ToString("HH:mm:ss");
    }
    int GetTimeByArrow(Transform arrow, int maxValue)
    {
        float angle = 360f - arrow.rotation.eulerAngles.z;
        float percent = Mathf.InverseLerp(0, 360, angle);
        return (int)Math.Round(Mathf.Lerp(0, maxValue, percent),0, MidpointRounding.AwayFromZero);
    }
    void SetAllArrows(DateTime time)
    {
        SetArrow(time.Second, 60, secondArrow);
        SetArrow(time.Minute, 60, minuteArrow);
        SetArrow(time.Hour, 12, hourArrow);
    }
    void SetArrow(int value, int maxValue, Transform arrow)
    {
        float percent = Mathf.InverseLerp(0, maxValue, value);
        float angle = Mathf.Lerp(0, 360, percent);
        arrow.localRotation = Quaternion.Euler(new Vector3(0,0,-angle));
    }
}
