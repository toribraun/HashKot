﻿using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private int cost = 1;
    private SpriteRenderer sprite;
    
    public void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var unit = collider.GetComponent<HashKot>();
        if (unit)
        {
            unit.totalCollectedPoint += cost;
            unit.UpdatePoints(cost);
            Destroy(gameObject);
        }
    }
}
