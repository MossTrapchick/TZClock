using System;
using System.Collections;
using UnityEngine;

public class GlobalTime : MonoBehaviour
{
    static DateTime globalTime = new(2024, 9, 1, 6, 0, 0);
    public static DateTime Time
    {
        get => globalTime;
        set => globalTime = value;
    }
    private void Start()
    {
        SynchronizeTime();
    }
    public void SynchronizeTime()
    {
        StopAllCoroutines();
        StartCoroutine(getServerTime());
    }
    IEnumerator getServerTime()
    {
        var www = new WWW("google.com");
        while (!www.isDone && www.error == null)
            yield return new WaitForSeconds(0.1f);
        string timeString = www.responseHeaders["Date"];
        if (DateTime.TryParse(timeString, out DateTime time))
        {
            globalTime = new();
            globalTime += time.TimeOfDay;
        }
        StartCoroutine(startTime());
    }
    IEnumerator startTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            globalTime = globalTime.AddSeconds(1f);
        }
    }
}
