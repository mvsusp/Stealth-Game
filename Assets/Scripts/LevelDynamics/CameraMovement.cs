using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
    public float smooth = 1.5f;

    private Transform player;
    private Vector3 relCameraPos;
    private float relCameraPosMag;
    private Vector3 newPos;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).transform;
        relCameraPos = transform.position - player.position;
        relCameraPosMag = relCameraPos.magnitude - 0.5f;
    }

    void FixedUpdate()
    {
        var stdPos = player.position + relCameraPos;
        var abovePos = player.position + Vector3.up * relCameraPosMag;
        var checkPoints = new Vector3[5];
        checkPoints[0] = stdPos;
        checkPoints[1] = Vector3.Lerp(stdPos, abovePos, 0.25f);
        checkPoints[2] = Vector3.Lerp(stdPos, abovePos, 0.5f);
        checkPoints[3] = Vector3.Lerp(stdPos, abovePos, 0.75f);
        checkPoints[4] = abovePos;

        foreach (var checkPoint in checkPoints)
        {
            Debug.DrawLine(player.position, checkPoint, Color.green);

            if (ViewingPosCheck(checkPoint))
            {
                break;
            }
        }

        transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
        SmoothLookAt();
    }

    bool ViewingPosCheck(Vector3 checkPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(checkPos, player.position - checkPos, out hit, relCameraPosMag))
        {
            Debug.DrawLine(checkPos, player.position, Color.yellow);

            if (hit.transform != player)
            {
                return false;
            }
        }
        newPos = checkPos;
        return true;
    }

    void SmoothLookAt()
    {
        var relPlayerPosition = player.position - transform.position;
        var lookRotation = Quaternion.LookRotation(relPlayerPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, smooth * Time.deltaTime);
    }
}
