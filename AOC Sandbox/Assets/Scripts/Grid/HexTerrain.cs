using UnityEngine;
using System;

public class HexTerrain : MonoBehaviour
{
	/// <summary>
	/// temporary class used to prevent crash, to redo
	/// </summary>
	public event Action OnMouseEnterAction;
	public event Action OnMouseExitAction;
	private void OnMouseEnter()
	{
		Debug.Log("Mouse enter");
		OnMouseEnterAction?.Invoke();
	}

	private void OnMouseExit()
	{
		Debug.Log("Mouse exit");
		OnMouseExitAction?.Invoke();
	}
}