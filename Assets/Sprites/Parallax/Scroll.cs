using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

	public float velocidad = 0f;
	private bool enMovimiento = true;
	private float tiempoInicio = 0f;

	// Use this for initialization
	void Start () {
	}

	void PersonajeHaMuerto(){
		enMovimiento = false;
	}

	void PersonajeEmpiezaACorrer(){
		enMovimiento = true;
		tiempoInicio = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(enMovimiento){
			GetComponent<Renderer>().material.mainTextureOffset = new Vector2(((Time.time - tiempoInicio) * velocidad) % 1, 0);
		}
	}
}
