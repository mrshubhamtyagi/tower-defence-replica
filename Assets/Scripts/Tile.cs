using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color hoverColor;

    private Material tileMat;
    private Color defaultColor;

    private Vector2 tilePosition = Vector2.zero;

    public Vector2 GetTilePosition()
    {
        return tilePosition;
    }

    void Start()
    {
        tileMat = GetComponent<MeshRenderer>().material;
        defaultColor = tileMat.color;
        hoverColor = hoverColor.HexToColor("#FFFFC8");
    }


    void Update()
    {

    }


    private void OnMouseEnter()
    {
        tileMat.color = hoverColor;
        tilePosition = new Vector2(transform.localPosition.x, transform.localPosition.z);
    }

    private void OnMouseExit()
    {
        tileMat.color = defaultColor;
    }
}
