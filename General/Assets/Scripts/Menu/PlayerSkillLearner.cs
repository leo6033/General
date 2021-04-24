using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * PlayerSkill类保存了技能信息，是一种持久化保存方式，详见PlayerSkill类
 */
public class PlayerSkillLearner : MonoBehaviour
{
    public List<GameObject> Traces;
    public List<GameObject> Level1;
    public List<GameObject> Level2;
    public List<GameObject> Level3;
    public GameObject Level4;
    public GameObject SkillLearnedCanvas;

    PlayerSkill skill;
    int skillTreeNum = 0;
    int[][] skillTree;
    public int trace = 0;
    List<List<GameObject>> levels;

    private void init()
    {
        PlayerSkill p = new PlayerSkill();
        int[][] red = new int[4][];
        int[][] yellow = new int[4][];
        int[][] blue = new int[4][];
        for(int i = 0; i < 4; i++)
        {
            int[] temp = new int[4];
            for(int j = 0; j < 3; j++)
            {
                temp[j] = 0;
                //temp[j] = (int)(Random.value * 4);
            }
            temp[3] = 0;
            //temp[3] = (int)(Random.value * 2);

            red[i] = temp;
        }
        for (int i = 0; i < 4; i++)
        {
            int[] temp = new int[4];
            for (int j = 0; j < 3; j++)
            {
                temp[j] = 0;
                //temp[j] = (int)(Random.value * 4);
            }
            temp[3] = (int)(Random.value * 2);

            yellow[i] = temp;
        }
        for (int i = 0; i < 4; i++)
        {
            int[] temp = new int[4];
            for (int j = 0; j < 3; j++)
            {
                temp[j] = 0;
                //temp[j] = (int)(Random.value * 4);
            }
            temp[3] = (int)(Random.value * 2);

            blue[i] = temp;
        }
        p.RedTree = red;
        p.YellowTree = yellow;
        p.BlueTree = blue;
        p.SkillLevel = 4;

        SaveSystem.SavePlayerSkill(p);

    }

    private void Start()
    {
        init();
        skill = SaveSystem.LoadPlayerSkill();

        skillTree = skill.RedTree;
        levels = new List<List<GameObject>>();
        levels.Add(Level1);
        levels.Add(Level2);
        levels.Add(Level3);
        SwitchTrace(trace);
    }

    public void SwitchTree(int i)
    {
        skillTreeNum = i;
        skill = SaveSystem.LoadPlayerSkill();
        switch (skillTreeNum)
        {
            case 0:
                skillTree = skill.RedTree;break;
            case 1:
                skillTree = skill.YellowTree;break;
            case 2:
                skillTree = skill.BlueTree;break;
        }
        SwitchTrace(trace);
    }
    public void SwitchTrace(int i)
    {
        trace = i;
        string text = "";
        Color c = Color.white;
        switch (skillTreeNum)
        {
            case 0:
                text = "无敌";
                c = Color.red;
                break;
            case 1:
                text = "吸血";
                c = Color.green;
                break;
            case 2:
                text = "行军";
                c = Color.blue;
                break;

        }
        for(int j = 0; j < Traces.Count; j++)
        {
            Traces[j].transform.GetChild(0).GetComponent<Text>().text = text;
            if (j == trace)
            {
                Traces[j].GetComponent<RawImage>().color = c;
                continue;
            }
            Traces[j].GetComponent<RawImage>().color = Color.white;
        }
        showTrace();
    }

    private void showTrace()
    {
        for (int j = 0; j < levels.Count; j++)//3级技能
        {
            List<GameObject> l = levels[j];//3个可选择的技能方向图标列表
            for (int i = 0; i < l.Count; i++)
            {
                GameObject t = l[i];
                if(j > skill.SkillLevel)//如果技能等级未到，全是灰的
                {
                    t.GetComponent<RawImage>().color = Color.gray;
                    t.transform.GetChild(0).GetComponent<Text>().text = "";
                    continue;
                }

                int choice = skillTree[trace][j] - 1;//当前选择的技能方向

                if (choice == -1)//-1表示未选择
                {
                    t.GetComponent<RawImage>().color = Color.white;
                    t.transform.GetChild(0).GetComponent<Text>().text = PlayerSkillInformation.SKILLINFORMATIONS[skillTreeNum,trace,j,i];
                    continue;
                }

                if (choice == i)//如果已选择把当前置蓝，其他置灰
                {
                    t.GetComponent<RawImage>().color = Color.blue;
                    t.transform.GetChild(0).GetComponent<Text>().text = PlayerSkillInformation.SKILLINFORMATIONS[skillTreeNum, trace, j, i];
                }
                else
                {
                    t.GetComponent<RawImage>().color = Color.gray;
                    t.transform.GetChild(0).GetComponent<Text>().text = "";
                }
            }
        }

        if(skill.SkillLevel >= 3)
        {
            if (skillTree[trace][3] != 0)
            {
                Level4.GetComponent<RawImage>().color = Color.blue;
                Level4.transform.GetChild(0).GetComponent<Text>().text = PlayerSkillInformation.SKILLINFORMATIONS[skillTreeNum, trace, 3, 0];
            }
            else
            {
                Level4.GetComponent<RawImage>().color = Color.white;
                Level4.transform.GetChild(0).GetComponent<Text>().text = PlayerSkillInformation.SKILLINFORMATIONS[skillTreeNum, trace, 3, 0];
            }
        }
        else
        {
            Level4.GetComponent<RawImage>().color = Color.gray;
            Level4.transform.GetChild(0).GetComponent<Text>().text = "";
        }
    }

    
    public void LearnSkill(string skillNum)
    {
        int levelNum = skillNum[0]-'0';
        int chooseNum = skillNum[1] - '0';

        if (skillTree[trace][levelNum] != 0) return;

        Debug.Log(skillTreeNum +" "+trace+" "+levelNum + " " + chooseNum);
        skill = SaveSystem.LoadPlayerSkill();

        switch (skillTreeNum)
        {
            case 0:
                skill.RedTree[trace][levelNum] = chooseNum;
                break;
            case 1:
                skill.YellowTree[trace][levelNum] = chooseNum;
                break;
            case 2:
                skill.BlueTree[trace][levelNum] = chooseNum;
                break;
        }

        SaveSystem.SavePlayerSkill(skill);

        PlayerData player = SaveSystem.LoadPlayer();
        Skill s = new Skill();
        s.num = "" + skillTreeNum + trace + levelNum + chooseNum;
        s.name = "new";
        s.icon = "1";
        player.ALLSkills.Add(s);
        Debug.Log("add");
        SaveSystem.SavePlayer(player);

        SkillLearnedCanvas.GetComponent<SkillsShowManager>().showSkills();
        SwitchTree(skillTreeNum);
    }
}
