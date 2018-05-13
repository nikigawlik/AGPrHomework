using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LetterTextAnimation : MonoBehaviour {
	public float letterDelay = 2f;
	public float initialDelay = 3f;
	public float underscoreDelay = .5f;
	public bool doUnderscoreAnim = true;
	
	private Text text;
	private string finalString;
	private string currentString;
	private float timer = 0;
	private bool underscore = false;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		finalString = text.text;
		text.text = "";
		currentString = "";

		timer = initialDelay;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if(timer <= 0) {
			if(currentString.Length < finalString.Length) {
				currentString = finalString.Substring(0, currentString.Length + 1);
				timer = letterDelay;
			} 
			else if(doUnderscoreAnim) {
				if(underscore) {
					currentString = finalString;
				} else {
					currentString = finalString + "_";
				}
				underscore = !underscore;	
				timer = underscoreDelay;
			}
			text.text = currentString;
		}
	}
}
