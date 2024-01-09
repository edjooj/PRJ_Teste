[System.Serializable]
public class IconInfo
{
    public string iconName;
    public bool haveIcon;

    public IconInfo(string name, bool have)
    {
        iconName = name;
        haveIcon = have;
    }
}
