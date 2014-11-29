using UnityEngine;
using System.Collections;

public class MainMenuActions : MonoBehaviour {

	public GameObject SettingsMenu;
	public GameObject MainMenu;
	public GameObject HelpMenu;
	public GameObject CreditsMenu;

	public void EnableHelpMenu()
	{
		HelpMenu.SetActive(true);
		MainMenu.SetActive(false);
	}
	
	public void EnableSettingsMainMenu()
	{
		SettingsMenu.SetActive(true);
		MainMenu.SetActive(false);
	}
	
	public void DisableSettingsMainMenu()
	{
		SettingsMenu.SetActive(false);
		MainMenu.SetActive(true);
	}

	public void GoBackCredits()
	{
		CreditsMenu.SetActive(false);
	}

	public void ActivateCredits()
	{
		CreditsMenu.SetActive(true);
	}
}
