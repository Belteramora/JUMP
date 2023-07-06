using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    //текущая позиция в песне (в ударах)
    [HideInInspector]
    public float songPosInBeats;

    public float bpm;

    public float beatsShownInAdvance;

    public float firstBeatOffset;

    public SimpleGenerator greenTest;
    public SimpleGenerator redTest;
    public SimpleGenerator yellowTest;
    public SimpleGenerator blueTest;

    //сохранение всех позиций нот в ударах
    public NoteTrack noteTrack;

    [HideInInspector]
    public List<NoteDetails> notes;

    //индекс ноты, которую нужно создать следующей
    [HideInInspector]
    public int nextIndex = 0;

    //текущая позиция в песне (в секундах)
    private float songPosition;
    
    //длительность удара
    private float secPerBeat;

    //сколько времени (в секундах) прошло после начала песни
    private float dsptimesong;

    //количество ударов в минуту

    private AudioSource audioSource;

    private void Start()
    {
        StartMusic();
    }

    public void StartMusic()
    {
        Clear();

        audioSource = GetComponent<AudioSource>();

        notes = new List<NoteDetails>(noteTrack.notes);

        notes.Sort();

        //вычисление количества секунд в одном ударе
        //объявление bpm выполняется ниже
        secPerBeat = 60f / bpm;


        //запись времени начала песни
        dsptimesong = (float)AudioSettings.dspTime;

        //начало песни
        audioSource.Play();
    }


    private void Update()
    {
        //вычисление позиции в секундах
        songPosition = (float)(AudioSettings.dspTime - dsptimesong - firstBeatOffset);

        //вычисление позиции в ударах
        songPosInBeats = songPosition / secPerBeat;

        if (nextIndex < notes.Count && notes[nextIndex].noteBeat < songPosInBeats + beatsShownInAdvance)
        {
            //Instantiate( /* префаб ноты */ );

            switch (notes[nextIndex].line) 
            {
                case Line.Green:
                    greenTest.SpawnNote(this);
                    break;
                case Line.Red:
                    redTest.SpawnNote(this);
                    break;
                case Line.Yellow:
                    yellowTest.SpawnNote(this);
                    break;
                case Line.Blue:
                    blueTest.SpawnNote(this);
                    break;

            }

            //инициализация полей ноты

            nextIndex++;
        }
    }


    public void Restart()
    {

        audioSource.time = 0;

        StartMusic();

        nextIndex = 0;
    }


    private void Clear()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("GreenNote"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("RedNote"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("YellowNote"))
        {
            Destroy(go);
        }

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("BlueNote"))
        {
            Destroy(go);
        }

    }
}
