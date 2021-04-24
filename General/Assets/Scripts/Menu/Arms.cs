
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
}
