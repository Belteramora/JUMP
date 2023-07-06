using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newNoteTrack", menuName = "NoteTrack")]
public class NoteTrack : ScriptableObject
{
    public List<NoteDetails> notes;
}
