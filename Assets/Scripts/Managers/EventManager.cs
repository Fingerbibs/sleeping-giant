using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();

    public static void StartListening(string eventName, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventName))
            eventDictionary.Add(eventName, listener);
        else
            eventDictionary[eventName] += listener;
    }

    public static void StopListening(string eventName, Action listener)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName] -= listener;
    }

    public static void TriggerEvent(string eventName)
    {
        if (eventDictionary.ContainsKey(eventName))
            eventDictionary[eventName]?.Invoke();
    }
}