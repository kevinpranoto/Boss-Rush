using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	private GameObject menu;
	private PlayerControls player;

	void Awake()
	{
		menu = GameObject.Find("Death Menu");
		menu.SetActive(false);
		player = GameObject.Find("Player").GetComponent<PlayerControls>();
	}

	// Update is called once per frame
	void Update() {
		if (player.isPlayerDead() && menu.activeInHierarchy == false)
		{
			Pause();
		}
	}

	public void Pause()
	{
		if (menu.activeInHierarchy == false)
		{
			menu.SetActive(true);
			Time.timeScale = 0;
		}
		else
		{
			menu.SetActive(false);
			Time.timeScale = 1;
		}
	}
}
