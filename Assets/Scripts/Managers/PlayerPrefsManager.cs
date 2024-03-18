using UnityEngine;

public static class PlayerPrefsManager
{
    public static int lastLevelLoaded
    {
        get { return PlayerPrefs.GetInt("lastLevelLoaded"); }
        set { PlayerPrefs.SetInt("lastLevelLoaded", value); }
    }
    
    public static float highScore
    {
        get { return PlayerPrefs.GetFloat("highScore"); }
        set { PlayerPrefs.SetFloat("highScore", value); }
    }
}
