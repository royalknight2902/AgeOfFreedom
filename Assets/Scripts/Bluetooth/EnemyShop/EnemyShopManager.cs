using UnityEngine;
using System.Collections;

public class EnemyShopManager: Singleton<EnemyShopManager> {

	public GameObject tempListEnemyShop;

	[HideInInspector]
	public System.Collections.Generic.Dictionary<string, GameObject> listItems = new System.Collections.Generic.Dictionary<string, GameObject>();
	

	void Awake()
	{

	}
	
	void Start()
	{
		initalize();

	}
	
	void OnEnable()
	{

		updateAttribute(PlayerInfo.Instance.dragonInfo.id);
	}
	
	void initalize()
	{
		#region Enemy 
		AutoDestroy.destroyChildren(tempListEnemyShop, null);
		//Debug.Log(ReadDatabase.Instance.DragonItemInfo.Count);
		int i = 0;

		GameObject EnemyBefore = null;
		float rowItemBefore = 0f;

		#region Code cũ
		foreach (System.Collections.Generic.KeyValuePair<string,EnemyData> iterator in ReadDatabase.Instance.EnemyInfo)
		{
			GameObject enemyObj = Instantiate(Resources.Load<GameObject>("Prefab/Bluetooth/Enemy Bluetooth")) as GameObject;
			enemyObj.transform.parent = tempListEnemyShop.transform;
			enemyObj.transform.localScale = Vector3.one;
			
			//set ID
			enemyObj.GetComponent<EnemyBluetoothController>().ID = iterator.Key;
			


			
			//Anchor
			UIAnchor uiAnchor = enemyObj.GetComponent<UIAnchor>();
			
			//Stretch
			UIStretch uiStretch = enemyObj.GetComponent<UIStretch>();
			
			if (i == 0) // Phan tu dau neo theo cha
			{
				uiAnchor.container = tempListEnemyShop;
				uiStretch.container = tempListEnemyShop;

				enemyObj.GetComponent<UIDragScrollView>().scrollView = tempListEnemyShop.GetComponent<UIScrollView>();
			}
			else // cac phan tu sau noi duoi voi nhau
			{
				uiAnchor.container = EnemyBefore;
				uiAnchor.relativeOffset.y = -0.52f;
				uiAnchor.side = UIAnchor.Side.Center;
				//  dragonItem.GetComponent<UIWidget>().pivot = UIWidget.Pivot.Top;
				
				
				uiStretch.container = EnemyBefore;
				uiStretch.relativeSize = Vector2.one;
				enemyObj.GetComponent<UIDragScrollView>().scrollView = tempListEnemyShop.GetComponent<UIScrollView>();
			}
			
			
			#endregion
			
		

			
			enemyObj.transform.GetChild(0).gameObject.GetComponent<UILabel>().text = iterator.Value.Name;
		//	enemyObj.transform.GetChild(1).gameObject.GetComponent<UILabel>().text = bonusText;

			enemyObj.transform.GetChild(1).gameObject.GetComponent<UITexture>().mainTexture = Resources.Load<Texture>("Image/Enemy/00 Guide Icon/" + iterator.Value.Name.ToLower());
			Transform info = enemyObj.transform.GetChild (2).gameObject.transform;
			info.GetChild(0).gameObject.GetComponentInChildren<UILabel>().text = iterator.Value.HP.ToString();
			info.GetChild(1).gameObject.GetComponentInChildren<UILabel>().text = iterator.Value.DEF.ToString();
			info.GetChild(2).gameObject.GetComponentInChildren<UILabel>().text = iterator.Value.Coin.ToString();
			info.GetChild(3).gameObject.GetComponentInChildren<UILabel>().text = iterator.Value.Region.ToString();
			info.GetChild(4).gameObject.GetComponentInChildren<UILabel>().text = iterator.Value.Speed.ToString();

			enemyObj.SetActive(true);
			EnemyBefore = enemyObj;
		//	rowItemBefore = row;
			
			listItems.Add(iterator.Key, enemyObj);
			
			i++;
		}

		tempListEnemyShop.GetComponent<UIPanel>().clipOffset = Vector2.one;
		#endregion
		
	
	}
	
	public void updateAttribute(string branch)
	{
		DragonPlayerData data = ReadDatabase.Instance.DragonInfo.Player[branch];

	}
	

	
	public void equipItemForDragon(string itemName)
	{
		DragonItemData itemData = loadInfoItem(itemName);
	}
	
	DragonItemData loadInfoItem(string itemName)
	{
		foreach (System.Collections.Generic.KeyValuePair<string, DragonItemData> iterator in ReadDatabase.Instance.DragonInfo.Item)
		{
			if (iterator.Value.Name == itemName)
				return iterator.Value;
		}
		return null;
	}
}
