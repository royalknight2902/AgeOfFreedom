using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EItemState
{
	HAND_OF_MIDAS,
	ATK,
	SPAWN_SHOOT,
	RANGE,
	TOWER, //khong xai, vi tuong trung cho atk, spawn shoot va range
}
public class ItemManager : Singleton<ItemManager>
{
	public float BonusCoin { get; set; }
	public float BonusATK {get; set;}
	public float BonusSpawnShoot { get; set; }
	public float BonusRange {get; set;}
	public float BonusTower {get; set;}

	[HideInInspector]
	public GameObject waveTemp;

	public Dictionary<string,GameObject> listItemBuff {get; set;}
	public ArrayList listItemState { get; set; }

    void Start()
    {
		listItemBuff = new Dictionary<string, GameObject> ();
		listItemState = new ArrayList ();

		initItemValue ();
    }

	void initItemValue()
	{
		foreach(System.Collections.Generic.KeyValuePair<string, ItemData> iterator in ReadDatabase.Instance.ItemInfo)
		{
			switch(iterator.Key)
			{
			case "HandOfMidas": BonusCoin = iterator.Value.ValueEffect; break;
			case "ATK+": BonusATK = iterator.Value.ValueEffect; break;
			case "SpawnShoot+": BonusSpawnShoot = iterator.Value.ValueEffect; break;
			case "Range+": BonusRange = iterator.Value.ValueEffect; break;
			case "Tower+": BonusTower = iterator.Value.ValueEffect; break;
			}
		}
	}

    public void enableItem(string id, EItemState state, int numberWave)
    {
		if(state != EItemState.TOWER)
		{
			if(!listItemState.Contains(state)) // neu chua co
			{
				listItemState.Add(state);
				showIconItemBuff(id, state, numberWave);
			}
			else // neu da~ co' thi cong^. don^`
				listItemBuff[id].GetComponent<ItemBuffController>().Waves += numberWave;
		}
		else
		{
			for(int i=1;i<=3;i++)
			{
				object[] values = getIdStateByIndex(i);
				EItemState stateTemp = (EItemState)values[1];

				if(!listItemState.Contains(stateTemp)) // neu chua co
				{
					listItemState.Add(stateTemp);
					showIconItemBuff(values[0].ToString(), stateTemp, numberWave);
				}
				else // neu da~ co' thi cong^. don^`
					listItemBuff[values[0].ToString()].GetComponent<ItemBuffController>().Waves += numberWave;
			}
		}
    }

	void showIconItemBuff(string id, EItemState state, int numberWave)
	{
		GameObject itemBuff = MonoBehaviour.Instantiate(Resources.Load<GameObject> ("Prefab/Effect/Item Buff")) as GameObject;
		itemBuff.transform.parent = PlayManager.Instance.itemBuffTemp.transform;
		itemBuff.transform.localScale = Vector3.one;
		
		//set dimension
		UISprite sprite = itemBuff.GetComponent<UISprite> ();
		Vector2 dimension = PlayConfig.getSizeItemBuff(id);
		sprite.keepAspectRatio = UIWidget.AspectRatioSource.Free;
		sprite.SetDimensions((int)dimension.x, (int)dimension.y);
		sprite.keepAspectRatio = UIWidget.AspectRatioSource.BasedOnHeight;
		
		itemBuff.GetComponent<UIStretch> ().container = waveTemp;
		
		//set anchor sprite
		UIAnchor anchor = itemBuff.GetComponent<UIAnchor> ();
		anchor.container = waveTemp;
		anchor.relativeOffset = new Vector2 (PlayConfig.AnchorItemBuffStart.x, PlayConfig.AnchorItemBuffStart.y
		                                     - listItemBuff.Count * PlayConfig.AnchorItemBuffDistanceY);
		
		//set anchor label
		itemBuff.transform.GetChild (0).GetComponent<UIAnchor> ().enabled = true;
		
		//set icon and wave
		ItemBuffController itemBuffController = itemBuff.GetComponent<ItemBuffController> ();
		itemBuffController.ID = id;
		itemBuffController.State = state;
		itemBuffController.Waves = numberWave;
		
		listItemBuff.Add (id, itemBuff);
	}

	public void resetItemBuff()
	{
		int i = 0;
		foreach(System.Collections.Generic.KeyValuePair<string, GameObject> iterator in listItemBuff)
		{
			//set anchor sprite
			UIAnchor anchor = iterator.Value.GetComponent<UIAnchor> ();
			anchor.relativeOffset = new Vector2 (PlayConfig.AnchorItemBuffStart.x, PlayConfig.AnchorItemBuffStart.y
			                                     - i * PlayConfig.AnchorItemBuffDistanceY);
			anchor.enabled = true;

			//set anchor label
			iterator.Value.transform.GetChild (0).GetComponent<UIAnchor> ().enabled = true;

			i++;
		}
	}

	object[] getIdStateByIndex(int index)
	{
		string s = "";
		EItemState state = EItemState.HAND_OF_MIDAS;
		switch(index)
		{
		case 0: s = "HandOfMidas"; state = EItemState.HAND_OF_MIDAS; break;
		case 1: s = "ATK+"; state = EItemState.ATK; break;
		case 2: s = "SpawnShoot+"; state = EItemState.SPAWN_SHOOT; break;
		case 3: s = "Range+"; state = EItemState.RANGE; break;
		}
		return new object[] {s, state};
	}

	public static EItemState getItemState(string ID)
	{
		EItemState state = EItemState.HAND_OF_MIDAS;
		switch(ID)
		{
		case "HandOfMidas": state = EItemState.HAND_OF_MIDAS; break;
		case "ATK+": state = EItemState.ATK; break;
		case "SpawnShoot+": state = EItemState.SPAWN_SHOOT; break;
		case "Range+": state = EItemState.RANGE; break;
		case "Tower+": state = EItemState.TOWER; break;
		}
		return state;
	}
}