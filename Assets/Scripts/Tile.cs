using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static bool isHovered = false;

    public Transform defenderPrefab;

    [SerializeField] private Color hoverColor;

    private Material tileMat;
    private Color defaultColor;
    private static Vector3 tilePosition = Vector3.zero;
    private bool hasDefender = false;

    public Vector3 GetTilePosition()
    {
        return tilePosition;
    }

    void Start()
    {
        tileMat = GetComponent<MeshRenderer>().material;
        defaultColor = tileMat.color;
        hoverColor = hoverColor.HexToColor("#FFFFC8");
    }

    private void OnMouseEnter()
    {
        isHovered = true;
        tilePosition = new Vector4(transform.localPosition.x, -0.1f, transform.localPosition.z);
        if (!hasDefender)
            tileMat.color = hoverColor;
    }

    private void OnMouseExit()
    {
        isHovered = false;
        tileMat.color = defaultColor;
    }

    private void OnMouseDown()
    {
        if (!hasDefender)
        {
            hasDefender = true;
            Vector3 spawnPosition = GetTilePosition();
            Transform defender = Instantiate(defenderPrefab, spawnPosition, defenderPrefab.rotation);
            defender.SetParent(GameObject.Find("DefenderSpawner").transform);
            print("Defender Installed");
        }
        else
            print("Already Has a Defender");
    }
}
