using UnityEngine;      
using UnityEngine.UI;

using System.Runtime.Serialization.Formatters.Binary;   // Binary formatter
using System.IO;                                        // File


/// <summary>
/// A basic example of how to set up game saving for Unity project
/// a more in depth tutorial can be found here: 
/// https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
/// </summary>
/// <author>Ben Hoffman</author>
public class GameSave : MonoBehaviour
{
    public InputField InputOptionA;
    public Slider SliderOptionB;
    

    public GameSaveData CurrentData;

    private static string OPTION_A_PREF_KEY = "OptionA_Key";
    private static string OPTION_B_PREF_KEY = "OptionB_Key";
    private static string SAVE_FILE_NAME = "/gameSave.dat";

    void Start()
    {
        // Start is a good place to load in preferences
        LoadPreferences();

        
    }

    #region Player Preferences

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
    public void LoadPreferences()
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

    #endregion

    #region Binary Formatter

    /// <summary>
    /// Saves the current game data object.
    /// </summary>
    public void SaveBin()
    {
        SaveGameData(CurrentData);
    }

    /// <summary>
    /// Loads in the current game data object
    /// </summary>
    public void LoadBin()
    {
        CurrentData = LoadGameData();
    }

    /// <summary>
    /// Use a binary formatter to save some game data to a file
    /// </summary>
    public static void SaveGameData(in GameSaveData aData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        Debug.Log("Persistent data path: " + Application.persistentDataPath);

        FileStream file = File.Open(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.OpenOrCreate);

        try
        {
            bf.Serialize(file, aData);
        }
        catch(System.Exception e)
        {
            Debug.LogErrorFormat("Serialization exception : {}", e.Message);
        }
        finally
        {
            file.Close();        
        }
    }

    /// <summary>
    /// Use a binary formatter to Deserialize the save game data if it exists
    /// </summary>
    /// <returns>Loaded save data from the SAVE FILE</returns>
    public static GameSaveData LoadGameData()
    {
        GameSaveData data = null;

        if ( File.Exists(Application.persistentDataPath + SAVE_FILE_NAME) )
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open);
            
            try
            {
                data = (GameSaveData)bf.Deserialize(file); 
            }
            catch(System.Exception e)
            {
                Debug.LogErrorFormat("Serialization exception : {}", e.Message);
            }
            finally
            {
                file.Close();
            }
        }

        return data;
    }

    #endregion

}


/// <summary>
/// A simple data container class
/// </summary>
[System.Serializable]   // <---- Makes it possible for the binary formatter to work
public class GameSaveData
{
    public float currentHealth;
    public int currentCheckpointID;
    public bool randomFlag;
}