using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * MouseController
 * ----------------
 * Handles mouse input and triggers actions based on left, right, and middle mouse clicks.
 * Uses raycasting to detect objects hit by the mouse click.
 *
 * Key Features:
 * - Listens for mouse button clicks (left, right, middle).
 * - Casts a ray from the camera to detect objects.
 * - Triggers corresponding actions with the hit information.
 *
 * Integration in Unity:
 * - Attach this script to a GameObject in the scene.
 * - Subscribe to OnLeftMouseClick, OnRightMouseClick, or OnMiddleMouseClick to handle mouse interactions.
 */
public class MouseController : Singleton<MouseController>
{
    // * Actions triggered when a mouse button is clicked on an object
    public Action<RaycastHit> OnLeftMouseClick;
    public Action<RaycastHit> OnRightMouseClick;
    public Action<RaycastHit> OnMiddleMouseClick;

    void Update()
    {
        // * Check for left, right, and middle mouse button clicks
        if (Input.GetMouseButtonDown(0))
        {
            CheckMouseClick(0);
        }
        if (Input.GetMouseButtonDown(1))
        {
            CheckMouseClick(1);
        }
        if (Input.GetMouseButtonDown(2))
        {
            CheckMouseClick(2);
        }
    }

    void CheckMouseClick(int mouseButton)
    {
        // * Cast a ray from the camera to detect objects under the mouse cursor
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // * Invoke the corresponding action based on the mouse button pressed
            if (mouseButton == 0)
                OnLeftMouseClick?.Invoke(hit);
            else if (mouseButton == 1)
                OnRightMouseClick?.Invoke(hit);
            else if (mouseButton == 2)
                OnMiddleMouseClick?.Invoke(hit);
        }
    }
}
