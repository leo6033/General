
[System.Serializable]
/*
 * 目前只保存了兵团的名字和图标（string内容为Resources文件夹下png文件名称）
 * 有一个思路是在Arms对象中存储兵种预制件的名称（因为需要序列化保存），加载游戏时通过名称找到对应预制件生成兵团
 * 问题在于我不知道兵团的生成机制
 * ——4/24  孙天宇
 */
public class Arms
{
    public string name;
    public string icon;
    public int selectedIndex;

    private string type;    // 兵种类型
    private string LegionPrefebPath;    // 预制体路径
    public Arms(string name, string icon)
    {
        this.name = name;
        this.icon = "UI/" + icon;
        selectedIndex = -1;

        type = "normal";
        LegionPrefebPath = "Perfabs/Character/normal/" + type + " player";
    }

    public string getLegionPrefebPath()
    {
        return LegionPrefebPath;
    }
}
