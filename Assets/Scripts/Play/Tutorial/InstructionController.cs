using UnityEngine;
using System.Collections;

public class InstructionController : MonoBehaviour {
	public UILabel labelText;
	public UILabel labelPages;
	public GameObject ToggleStartup;

	public int currentPage { get; set; }

	public void setPage()
	{
		labelPages.text = currentPage + " / " + PlayConfig.PagesInstruction;
	}

	public void setText()
	{
		labelText.text = PlayConfig.getTextInstruction (currentPage);
	}
}
