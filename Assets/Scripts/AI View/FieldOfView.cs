using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FieldOfView : MonoBehaviour
{
    [Header("View Parameter")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    [Header("Alert Parameter")]
    public float alertRadius;
    [Range(0, 360)]
    public float alertAngle;

    [Header("Report Parameter")]
    public float reportRadius;

    public GameObject playerGO;

    public LayerMask targetMask;
    public LayerMask obstructionMask;
    public LayerMask npcMask;

    public bool isPlayerSight;
    public bool isPlayerSeen;
    public bool onRange;

    public Collider[] npcChecks;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
            AlertViewCheck();
            ReportOther();

        }
    }

    private void Update()
    {
        changeColor();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    isPlayerSight = true;
                else
                    isPlayerSight = false;
            }
            else
                isPlayerSight = false;
        }
        else if (isPlayerSight)
            isPlayerSight = false;
    }

    private void AlertViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, alertRadius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < alertAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    isPlayerSeen = true;
                else
                    isPlayerSeen = false;
            }
            else
                isPlayerSeen = false;
        }
        else if (isPlayerSeen)
            isPlayerSeen = false;
    }

    private void ReportOther()
    {
        npcChecks = Physics.OverlapSphere(transform.position, reportRadius, npcMask);

        for (int i = 0; i < npcChecks.Length; i++)
        {

            if (npcChecks[i].transform.gameObject != this.gameObject)
            {
                bool Hit = Physics.Raycast(transform.position, npcChecks[i].transform.position);
                Debug.DrawRay(transform.position, npcChecks[i].transform.localPosition);

                if (Hit)
                {
                    npcChecks[i].GetComponent<FieldOfView>().onRange = true;
                }
                else
                {
                    npcChecks[i].GetComponent<FieldOfView>().onRange = false;
                }
            }
        }

        if (isPlayerSeen)
        {
            for (int i = 0; i < npcChecks.Length; i++)
            {
                npcChecks[i].GetComponent<FieldOfView>().isPlayerSeen = true;
            }
        }
    }

    private void changeColor()
    {
        Material cube = new Material(this.GetComponent<Renderer>().material);
        gameObject.GetComponent<Renderer>().material = cube;
        if (isPlayerSeen)
            cube.color = Color.red;
        else
            cube.color = Color.green;


    }
}
