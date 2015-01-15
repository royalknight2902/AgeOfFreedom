﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DragonItemsController))]
public class DragonItemAction : MonoBehaviour {

    [HideInInspector]
    public DragonItemsController dragonItemController;

	// Use this for initialization
	void Start () {
        dragonItemController = GetComponent<DragonItemsController>();
	}
}
