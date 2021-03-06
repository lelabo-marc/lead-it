﻿using UnityEngine;
using System.Collections;

public class ColliderState : MonoBehaviour
{
	public GameObject	CountryController;
	public GameObject	LeadButton;
	public Camera		MainCamera;
	public GameObject	Pays;
	public GameObject	Title;
	public GameObject	Map;

    // Pon = PreviousObjectName
    // Pos = PreviousObjectSelected
    string Pon = "";
    GameObject ConfirmButton;

    void Start()
    {
        ConfirmButton = GameObject.Find("Validate_button");
        if (ConfirmButton)
            NGUITools.SetActive(ConfirmButton, false);
    }

    public void CountrySelected()
    {
		CountryController.SetActive (true);
		MainCamera.transform.position = GameObject.Find (Pon).transform.position;
		MainCamera.transform.Translate (0, 0, -10);
		MainCamera.orthographicSize = 2.6f;
		LeadButton.SetActive (false);
		GameObject.DestroyImmediate (LeadButton);
        Debug.Log("Country selected : " + Pon);
		Country data = Pays.GetComponent<Country> ();
		GameObject Mapchild = Map.transform.FindChild (Pon).gameObject;
		CountryResources Mapdata = Mapchild.GetComponent<CountryResources> ();
		data.setName(Pon);
		data.setBudget(Mapdata.Budget);
		data.setPopularity (Mapdata.Popularity);
		data.setProfit (Mapdata.Profit);
		data.setExpense (Mapdata.Expense);
		data.ExpenseInc = Mapdata.ExpenseInc;
		Title.GetComponent<UILabel> ().text = Pon;
		Title.GetComponent<UILabel> ().UpdateNGUIText ();
    }

    void Update()
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5f;
        Vector2 vect = Camera.main.ScreenToWorldPoint(mousePosition);

        Collider2D[] col = Physics2D.OverlapPointAll(vect);
        if (Input.GetMouseButtonDown(0) && LeadButton != null)
        {
            // Désactive la surbrillance du dernier object selectionné
            
            if (col.Length > 0)
            {
                GameObject selected = col[0].collider2D.gameObject;
                GameObject Pos = GameObject.Find(Pon);

                if (Pos)
                    Pos.renderer.enabled = false;

                selected.renderer.enabled = true;
                var color = selected.GetComponent<TweenColor>();

                if (color)
                    color.Toggle();

                if (ConfirmButton)
                    NGUITools.SetActive(ConfirmButton, true);

                Pon = selected.name;
            }
        }
    }

}
