using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSave : MonoBehaviour
{

    public InputField InputOptionA;
    public Slider SliderOptionB;
    

    private static string OPTION_A_PREF_KEY = "OptionA_Key";
    private static string OPTION_B_PREF_KEY = "OptionB_Key";

    void Start()
    {
        LoadPreferences();
    }


    /// <summary>
    /// Save some basic preferences from a settings menu using 
    /// PlayerPrefs
    /// </summary>
    public void SavePreferences()
    {
        // Simple Key -> value pairs, like a dictionary

        // Save the text from the input field for the option
        PlayerPrefs.SetString(OPTION_A_PREF_KEY, InputOptionA.text);

        // Save the float value from the slider, to a preference at key B
        PlayerPrefs.SetFloat(OPTION_B_PREF_KEY, SliderOptionB.value);

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Load in some saved data from PlayerPRefs
    /// </summary>
    private void LoadPreferences()
    {
        // You should check if you HAVE the key first, so that you can ensure
        // your data is right
        if(PlayerPrefs.HasKey(OPTION_A_PREF_KEY))
        {
            InputOptionA.text = PlayerPrefs.GetString(OPTION_A_PREF_KEY);
        }

        if (PlayerPrefs.HasKey(OPTION_B_PREF_KEY))
        {
            SliderOptionB.value = PlayerPrefs.GetFloat(OPTION_B_PREF_KEY);
        }
    }

}
