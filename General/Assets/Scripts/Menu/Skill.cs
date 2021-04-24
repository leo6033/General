[System.Serializable]

/*
 * 目前只保存了技能的名字、图标（string内容为Resources文件夹下png文件名称）和编号
 * 有一个思路是：技能的编号可以唯一地指定技能，可以通过某种映射形式（map或多维数组-类似PlayerSkillInformation类中的一样）确定技能效果，在游戏中进行使用
 * 问题在于现在我不太清楚这个技能要怎么在游戏中使用，我也不知道技能按键或者游戏中的图标是什么样的，所以没办法实装
 * 最好是搞个配置文件，使num对应技能名称、图标和效果
 * ——4/24  孙天宇
 */
public class Skill
{
    public string name;
    public string icon;
    public string num;//四位数 第一位012表示红黄蓝三个技能树， 第二位0123表示四个技能，第三位0123表示技能等级，第四位123表示当前等级所选择方向
}
