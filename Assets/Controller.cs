using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    private static string KEY_CURRENT_LEVEL = "CurrentLevel";
    private static string KEY_CURRENT_PERCENTAGE = "CurrentPercentage";
    private static string KEY_TARGET_LEVEL = "TargetLevel";
    private static string KEY_TARGET_PERCENTAGE = "TargetPercentage";

    public int[] accumulatedRounds;

    public Form current;
    public Form target;
    public InputField result;

    void Start()
    {
        instance = this;

        Load();

        Calculate();
    }

    public void Calculate()
    {
        int currentRound = SkillLevelToRound(current.level, current.percentage);
        int targetRound = SkillLevelToRound(target.level, target.percentage);

        int diff = targetRound - currentRound;

        if (diff < 0)
            diff = 0;

        result.text = diff.ToString();

        Save();
    }

    private int SkillLevelToRound(int level, int percentage)
    {
        return (int)(accumulatedRounds[level - 1] + percentage * 0.01f * (accumulatedRounds[level] - accumulatedRounds[level - 1]));
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(KEY_CURRENT_LEVEL))
            PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, 1);

        if (!PlayerPrefs.HasKey(KEY_CURRENT_PERCENTAGE))
            PlayerPrefs.SetInt(KEY_CURRENT_PERCENTAGE, 0);

        if (!PlayerPrefs.HasKey(KEY_TARGET_LEVEL))
            PlayerPrefs.SetInt(KEY_TARGET_LEVEL, 10);

        if (!PlayerPrefs.HasKey(KEY_TARGET_PERCENTAGE))
            PlayerPrefs.SetInt(KEY_TARGET_PERCENTAGE, 0);

        current.level = PlayerPrefs.GetInt(KEY_CURRENT_LEVEL);
        current.percentage = PlayerPrefs.GetInt(KEY_CURRENT_PERCENTAGE);

        target.level = PlayerPrefs.GetInt(KEY_TARGET_LEVEL);
        target.percentage = PlayerPrefs.GetInt(KEY_TARGET_PERCENTAGE);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, current.level);
        PlayerPrefs.SetInt(KEY_CURRENT_PERCENTAGE, current.percentage);

        PlayerPrefs.SetInt(KEY_TARGET_LEVEL, target.level);
        PlayerPrefs.SetInt(KEY_TARGET_PERCENTAGE, target.percentage);
    }
}