using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsignFollow : MonoBehaviour
{
    [HideInInspector] public PlayerManager playerLegion;
    [HideInInspector] public EnemyManager enemyLegion;
    [HideInInspector] public StateController stateController;

    private TextMesh textMesh;
    private int count;

    void Start()
    {
        textMesh = GetComponent<TextMesh>();
        if (enemyLegion != null)
        {
            textMesh.color = Color.red;
        }
    }

    void Update()
    {
        if (stateController != null && stateController.isActiveAndEnabled)
        {
            Vector3 position = stateController.transform.position;
            position.y += 2;
            transform.position = position;
            transform.LookAt(-Camera.main.transform.position);

        }
        else
        {
            if (playerLegion != null)
            {
                SetFollowingStateController(playerLegion.getFllowingStateController());
            }
            else
            {
                SetFollowingStateController(enemyLegion.getFllowingStateController());
            }

        }

        if (playerLegion != null)
        {
            count = playerLegion.count;
        }
        else
        {
            count = enemyLegion.count;
        }

        if (count != 0)
            textMesh.text = "" + count;
        else
            textMesh.text = "";
    }

    public void SetFollowingStateController(StateController stateController)
    {
        this.stateController = stateController;
    }
}
