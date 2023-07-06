using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{

    private Vector3 finishPoint;
    private Vector3 deathPoint;

    private Conductor cond;

    private Vector2 startPos;
    private float beatOfThisNote;
    private float lerpDist;

    private bool skip;

    private void Start()
    {
        startPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (!skip)
            MoveBeforeCheckLine();
        else
            MoveAfterCheckLine();

    }

    public void SetPoints(Vector3 finishPoint, Vector3 deathPoint)
    {
        this.finishPoint = finishPoint;
        this.deathPoint = deathPoint;
    }

    public void SetConductor(Conductor cond)
    {
        this.cond = cond;
    }

    public void SetBeat(float beat)
    {
        beatOfThisNote = beat;
    }

    public void MoveBeforeCheckLine()
    {
       transform.position = Vector2.Lerp(
       startPos,
       finishPoint,
       (cond.beatsShownInAdvance - (beatOfThisNote - cond.songPosInBeats)) / cond.beatsShownInAdvance
       );

        if(transform.position == finishPoint)
        {
            skip = true;
            finishPoint = transform.position;
        }
    }

    public void MoveAfterCheckLine()
    {
        transform.position = Vector2.Lerp(
       finishPoint,
       new Vector3(finishPoint.x, finishPoint.y - (startPos.y - finishPoint.y)),
       (-beatOfThisNote + cond.songPosInBeats) / cond.beatsShownInAdvance
       );
    }
}
