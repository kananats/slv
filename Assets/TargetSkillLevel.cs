using UnityEngine;
using UnityEngine.UI;

public class TargetSkillLevel : MonoBehaviour
{
    public InputField levelField;

    protected int _level;

    public virtual int level
    {
        get
        {
            return _level;
        }

        set
        {
            _level = value;

            if (_level >= 15)
                _level = 15;

            else if (level < 1)
                _level = 1;

            levelField.text = level.ToString();
        }
    }

    public virtual void EV_EndEdit()
    {
        try
        {
            int level = int.Parse(levelField.text);

            this.level = level;
        }

        catch (System.Exception)
        {
            levelField.text = level.ToString();
        }

        Controller.instance.Calculate();
    }
}
