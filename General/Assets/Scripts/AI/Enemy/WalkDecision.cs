using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/Enemy/Walk")]
public class WalkDecision : Decision
{
    // 用于敌人 AI 在前往房子的过程中做决策
    public override bool Decide(StateController controller)
    {
        bool encounter = Walk(controller);
        return encounter;
    }

    private bool Walk(StateController controller)
    {
        Collider[] objects = Physics.OverlapSphere(controller.transform.position, controller.stats.attackRange, 1<<8);
        foreach (Collider c in objects)
        {
            if(c.tag == "House")
            {
                controller.attackObject = c;
                return true;
            }
            else if (c.tag == "Player" && isInVision(controller, c))
            {
                // TODO: 设置攻击目标
                controller.attackObject = c;
                return true;
            }
        }
        return false;
    }

    private bool isInVision(StateController controller, Collider collider)
    {
        GameObject target = collider.gameObject;
        //计算与目标距离
        float distance = Vector3.Distance(controller.transform.position, target.transform.position);

        Vector3 mVec = controller.transform.rotation * Vector3.forward;//当前朝向
        Vector3 tVec = target.transform.position - controller.transform.position;//与目标连线的向量

        //计算两个向量间的夹角
        float angle = Mathf.Acos(Vector3.Dot(mVec.normalized, tVec.normalized)) * Mathf.Rad2Deg;
        //Debug.Log(angle + "  " + distance + "   " + controller.stats.attackRange);
        if (distance < controller.stats.attackRange)// && angle <= 180)
        {
            return true;
            //Ray DetectRay = new Ray(controller.transform.position, tVec.normalized * controller.stats.attackRange);
            //RaycastHit hitInfo;
            //if (Physics.Raycast(DetectRay, out hitInfo, controller.stats.attackRange))
            //{
            //    if (hitInfo.collider == collider)
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }
        return false;
    }
}
