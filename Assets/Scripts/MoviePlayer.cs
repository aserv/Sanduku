﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoviePlayer : MonoBehaviour {

	public MovieTexture movie;

	// Use this for initialization
	void Start () {
		//MovieTexture movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
		GetComponent<RawImage>().texture = movie as MovieTexture;
		movie.loop = true;
		movie.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
