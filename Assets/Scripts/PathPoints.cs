using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoints : MonoBehaviour
{
    public static PathPoints Instance;
    [SerializeField] public int totalPoints;

    private Transform[] pathPointsPosition;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        totalPoints = transform.childCount;
        pathPointsPosition = new Transform[totalPoints];
        FillPointsPosition();
    }

    private void FillPointsPosition()
    {
        for (int i = 0; i < transform.childCount; i++)
            pathPointsPosition[i] = transform.GetChild(i).transform;
    }

    public Transform GetPointPosition(int index)
    {
        if (index < totalPoints)
            return pathPointsPosition[index];
        else
        {
            return null;
        }

    }

}