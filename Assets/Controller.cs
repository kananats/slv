using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public int[] accumulatedRounds;

    public Form current;
    public Form target;
    public InputField result;

    void Start()
    {
        instance = this;

        current.level = 1;
        current.percentage = 0;

        target.level = 10;
        target.percentage = 0;
    }

    public void Calculate()
    {
        int currentRound = SkillLevelToRound(current.level, current.percentage);
        int targetRound = SkillLevelToRound(target.level, target.percentage);

        int diff = targetRound - currentRound;

        if (diff < 0)
            diff = 0;

        result.text = diff.ToString();
    }

    private int SkillLevelToRound(int level, int percentage)
    {
        return (int)(accumulatedRounds[level - 1] + percentage * 0.01f * (accumulatedRounds[level] - accumulatedRounds[level - 1]));
    }
}