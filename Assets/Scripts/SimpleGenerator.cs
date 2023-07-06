using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleGenerator : MonoBehaviour
{
    public float spawnTime;
    public string noteTag;
    public float movingSpeed;

    public RectTransform checkLine;

    [SerializeField]
    private GameObject notePrefab;

    private GameObject note;

    private Image image;
    private RectTransform rectTransform;
    private Vector3 spawnPoint;
    private Vector3 finishPoint;
    private Vector3 deathPoint;

    private float lastSpawnTime;

    private bool isSpawned;

    #region Unity Callback Functions

    void Start()
    {
        // Ссылки на нужные компоненты
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        // Установка первоначального времени спауна
        lastSpawnTime = Time.time;

        // Установка точки спауна по 'y' на основе углов канваса
        Vector3[] corners = GetCorners(rectTransform.parent.GetComponent<RectTransform>());
        spawnPoint.y = corners[1].y + notePrefab.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        deathPoint.y = corners[3].y;


        corners = GetCorners(checkLine);

        Debug.Log(corners[0] + " " + corners[1]);

        finishPoint.y = corners[1].y - Mathf.Abs((corners[0].y - corners[1].y) / 2);

        // Установка точки спауна по 'x' на основе углов линии
        corners = GetCorners(rectTransform);
        spawnPoint.x = corners[2].x - Mathf.Abs(corners[2].x - corners[1].x) / 2;
        spawnPoint.z = 0;

        deathPoint.x = corners[2].x - Mathf.Abs(corners[2].x - corners[1].x) / 2;

        finishPoint.x = corners[2].x - Mathf.Abs(corners[2].x - corners[1].x) / 2;

        Debug.Log(deathPoint + gameObject.name);
        // Непосредственный спаун ноты, на основе префаба, а также задание ей тэга и цвета

        isSpawned = true;
        
    }


    void Update()
    {

    }

    #endregion

    #region AdditionFunctions

    private Vector3[] GetCorners(RectTransform rect)
    {
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);

        return corners;
    }

    public void SpawnNote(Conductor cond)
    {
        note = Instantiate(notePrefab, spawnPoint, notePrefab.transform.rotation);
        Note noteScript = note.GetComponent<Note>();
        noteScript.SetPoints(finishPoint, deathPoint);
        noteScript.SetConductor(cond);
        noteScript.SetBeat(cond.notes[cond.nextIndex].noteBeat);
        note.tag = noteTag;
        note.GetComponent<SpriteRenderer>().color = new Color(image.color.r, image.color.g, image.color.b, 255);
    }

    #endregion

}
