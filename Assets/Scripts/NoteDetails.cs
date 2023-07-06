using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public enum Line
{
    Green, 
    Red,
    Yellow,
    Blue
}

[Serializable]
public struct NoteDetails: IComparable
{
    public float noteBeat;

    public Line line;

    public int CompareTo(object obj)
    {
        NoteDetails noteD = (NoteDetails)obj;

        if(noteD.noteBeat > noteBeat)
        {
            return -1;
        }
        else if(noteD.noteBeat < noteBeat)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
