using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    private static string KEY_CURRENT_LEVEL = "CurrentLevel";
    private static string KEY_CURRENT_PERCENTAGE = "CurrentPercentage";
    private static string KEY_TARGET_LEVEL = "TargetLevel";

    public int[] accumulatedRounds;

    public CurrentSkillLevel currentSkillLevel;
    public TargetSkillLevel targetSkillLevel;
    public InputField result;

    void Start()
    {
        instance = this;

        Load();

        Calculate();
    }

    public void Calculate()
    {
        int currentRound = SkillLevelToRound(currentSkillLevel.level, currentSkillLevel.percentage);
        int targetRound = SkillLevelToRound(targetSkillLevel.level);

        int diff = targetRound - currentRound;

        if (diff < 0)
            diff = 0;

        result.text = diff.ToString();

        Save();
    }

    private int SkillLevelToRound(int level, int percentage = 0)
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

        currentSkillLevel.level = PlayerPrefs.GetInt(KEY_CURRENT_LEVEL);
        currentSkillLevel.percentage = PlayerPrefs.GetInt(KEY_CURRENT_PERCENTAGE);

        targetSkillLevel.level = PlayerPrefs.GetInt(KEY_TARGET_LEVEL);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, currentSkillLevel.level);
        PlayerPrefs.SetInt(KEY_CURRENT_PERCENTAGE, currentSkillLevel.percentage);

        PlayerPrefs.SetInt(KEY_TARGET_LEVEL, targetSkillLevel.level);
    }
}