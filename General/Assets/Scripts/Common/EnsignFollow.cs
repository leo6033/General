using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsignFollow : MonoBehaviour
{
    [HideInInspector] public PlayerManager Legion;
    [HideInInspector] public StateController stateController;

    private TextMesh textMesh;

    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    void Update()
    {
        if (stateController != null && stateController.isActiveAndEnabled)
        {
            Vector3 position = stateController.transform.position;
            position.y = 2;
            transform.position = position;
            transform.LookAt(-Camera.main.transform.position);
        }
        else
        {
            SetFollowingStateController(Legion.getFllowingStateController());
        }

        if (Legion.count != 0)
            textMesh.text = "" + Legion.count;
        else
            textMesh.text = "";
    }

    public void SetFollowingStateController(StateController stateController)
    {
        this.stateController = stateController;
    }
}
