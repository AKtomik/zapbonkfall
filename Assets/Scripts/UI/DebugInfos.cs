using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DebugInfos : MonoBehaviour
{
	TextMeshProUGUI textComponent;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		textComponent = GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update()
	{
		bool doToggleDebug = Input.GetKey(KeyCode.B);
		if (doToggleDebug) textComponent.enabled = !textComponent.enabled;
		if (!textComponent.enabled) return;
		Vector2 movementVector = InputManager.ReadMovement2D();
		textComponent.text = @$"debug
		{Math.Round(movementVector.x, 2)} / {Math.Round(movementVector.y, 2)}
		so cool!
		";
	}
}
