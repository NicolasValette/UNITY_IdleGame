using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineTimer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _focusText;
    [SerializeField]
    private TMP_Text _lastLoginTimeText;
    [SerializeField]
    private Increment _increment;
    private int nb = 0;
    private void Start()
    {
        _focusText.text = $"Focus : {nb}";
    }
    private void OnApplicationFocus(bool focus)
    {
        nb++;
        _focusText.text = $"Focus : {nb}";
        Debug.Log($"Focus : {focus}");

        string focusDateString = DateTime.Now.ToString();
        Debug.Log($"Application Change focus at {focusDateString} !");
        if (focus)                          // When we get focus, we retrieve last login date.
        {
            string stringDate = PlayerPrefs.GetString("LAST_LOGIN");
            if (stringDate != null)
            {
                DateTime lastLogin = DateTime.Parse(stringDate);
                TimeSpan timeSpan = DateTime.Now - lastLogin;
                _lastLoginTimeText.text = $"Welcome back, you were offline for {timeSpan.Days} days and {timeSpan.Hours}H{timeSpan.Minutes}M{timeSpan.Seconds}S !";
                Debug.Log($"Welcome back, you were offline for {timeSpan.Days} days and {timeSpan.Hours}H{timeSpan.Minutes}M{timeSpan.Seconds}S, so {(int)timeSpan.TotalSeconds} seconds !");
        
                float increment = PlayerPrefs.GetFloat("Increment", 0f);
                Debug.Log($"Increment = {increment}");
                increment = (float) (increment * (int)timeSpan.TotalSeconds);
                Debug.Log($"Increment * {(int)timeSpan.TotalSeconds} = {increment}");
                PlayerPrefs.SetFloat("Increment", increment);
            }
        }
        else                                // When we loose focus, we save the date;
        {
            PlayerPrefs.SetString("LAST_LOGIN", focusDateString);
            PlayerPrefs.SetFloat("Increment", _increment.IncrementNumber);
        }
    }
    private void OnApplicationQuit()
    {
        string logoutString = DateTime.Now.ToString();
        Debug.Log($"Application quit at {logoutString} with increment = {_increment.IncrementNumber}!");
        PlayerPrefs.SetString("LAST_LOGIN", DateTime.Now.ToString());
        PlayerPrefs.SetFloat("Increment", _increment.IncrementNumber);
    }
}

