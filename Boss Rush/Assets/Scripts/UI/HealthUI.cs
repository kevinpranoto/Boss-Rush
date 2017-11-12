using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    private PlayerControls player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerControls>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "HP: " + player.health;
	}
}
