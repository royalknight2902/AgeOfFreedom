using UnityEngine;
using System.Collections;

public class PlayConfig
{
	public const float DistanceBulletChase = 0.05f; //Khoảng cách để hủy đạn chase
	public const float TimeNextWaveCooldown = 30.0f; //Thời gian cooldown còn lại để hiển thị next wave
	public const float TowerSellDuration = 1.0f; //Thời gian bán trụ
	public const float TimeWaitInstructionAfterMission = 0.2f; //Thoi gian wait sau khi mission hien thi xong
	public const float AnchorShopPanelStart = -4.499985f;
	public const int PagesInstruction = 3;
    public const int BabyDragonIndexForListStart = 5;
    public const int BabyDragonIndexForListDistance = 10;

    public static Vector3 PositionTowerBuild = new Vector3(5, -25, 0);
    public static Vector3 PositionTowerSell = new Vector3(5, -10, 0);
    public static Vector3 PositionDragonTower = new Vector3(5, -10, 0);

	#region FOOTBAR TOWER BUILD PANEL - LABEL FONTSIZE, COLOR, TOWER ANCHOR, CHECK OK SIZE
	public const float AnchorTowerBuildCheckOK = 0.71f;

	//Label fontsize, color
	public static STowerBuildConfig TowerBuildConfig = new STowerBuildConfig(25, new Vector2(0, 0.25f),
		new Color((float)255 / 255, (float)243 / 255, (float)77 / 255),
			30, new Vector2(0, 0.4f), Color.white);

	public static SConfigShop TowerBuildAnchor = new SConfigShop(-0.42f, 0.135f);

	public const float StretchTowerBuildArchitect = 0.78f;
	public const float StretchTowerBuildIce = 0.78f;
	public const float StretchTowerBuildRock = 0.82f;
	public const float StretchTowerBuildFire = 0.96f;
    public const float StretchTowerBuildGold = 0.78f;
    public const float StretchTowerBuildPoison = 0.78f;

	public static Vector2 AnchorTowerBuildArchitect = Vector2.zero;
	public static Vector2 AnchorTowerBuildIce = new Vector2(-0.01f, 0.0f);
	public static Vector2 AnchorTowerBuildRock = Vector2.zero;
	public static Vector2 AnchorTowerBuildFire = Vector2.zero;
    public static Vector2 AnchorTowerBuildGold = Vector2.zero;
    public static Vector2 AnchorTowerBuildPoison = Vector2.zero;

    public static Color ColorTowerBuildDragonLabelCostForeground = Color.red;
    public static Color ColorTowerBuildDragonLabelCostOutline = Color.black;
	#endregion

	#region FOOTBAR TOWER UPGRADE PANEL - COLOR SQUARE, TOWER ICON SIZE
	public static Color ColorTowerUpgradeDefault = new Color((float)191 / 255, (float)191 / 255, (float)149 / 255);
	public static Color ColorTowerUpgradeSelected = new Color((float)251 / 255, (float)255 / 255, (float)0 / 255);

	public static SBulletTowerShop StretchTowerUpgradeArchitect1 = new SBulletTowerShop(new Vector2(0, -0.01f), 0.95f);
	public static SBulletTowerShop StretchTowerUpgradeArchitect2 = new SBulletTowerShop(new Vector2(0, -0.01f), 0.74f);
	public static SBulletTowerShop StretchTowerUpgradeArchitect3 = new SBulletTowerShop(new Vector2(0, -0.03f), 0.74f);
	public static SBulletTowerShop StretchTowerUpgradeIce1 = new SBulletTowerShop(new Vector2(0, -0.05f), 0.77f);
	public static SBulletTowerShop StretchTowerUpgradeIce2 = new SBulletTowerShop(new Vector2(0, -0.05f), 0.67f);
	public static SBulletTowerShop StretchTowerUpgradeIce3 = new SBulletTowerShop(new Vector2(0, 0), 0.86f);
	public static SBulletTowerShop StretchTowerUpgradeRock1 = new SBulletTowerShop(new Vector2(-0.03f, -0.05f), 0.68f);
	public static SBulletTowerShop StretchTowerUpgradeRock2 = new SBulletTowerShop(new Vector2(0.03f, -0.02f), 0.77f);
	public static SBulletTowerShop StretchTowerUpgradeRock3 = new SBulletTowerShop(new Vector2(0.03f, -0.02f), 0.78f);
	public static SBulletTowerShop StretchTowerUpgradeFire1 = new SBulletTowerShop(new Vector2(0.03f, -0.02f), 0.78f);
	public static SBulletTowerShop StretchTowerUpgradeFire2 = new SBulletTowerShop(new Vector2(0, -0.02f), 0.84f);
	public static SBulletTowerShop StretchTowerUpgradeFire3 = new SBulletTowerShop(new Vector2(0, -0.02f), 0.89f);
    public static SBulletTowerShop StretchTowerUpgradeGold = new SBulletTowerShop(new Vector2(0, -0.02f), 0.89f);
	#endregion

	#region SHOP - TOWER SHOP PANEL, INFO PANEL
	public static SConfigShop TowerShopAnchor = new SConfigShop(-0.33f, 0.4f);
	public static SConfigShop InfoShopAnchor = new SConfigShop(-0.22f, 0.55f);

	public static Color ColorOff = new Color((float)107 / 255, (float)107 / 255, (float)107 / 255);
	public static Color ColorDiamond = new Color((float)177 / 255, (float)237 / 255, (float)253 / 255);
	public static Color ColorMoney = new Color((float)249 / 255, (float)252 / 255, (float)88 / 255);
	#endregion

	#region NEXT WAVE COUNTDOWN
	//Normal wave
	public static Color ColorNextWaveBackground = new Color((float)103 / 255, (float)206 / 255, (float)216 / 255);
	public static Color ColorNextWaveOutline = new Color((float)40 / 255, (float)99 / 255, (float)97 / 255);
	public static Color ColorNextWaveCountdownForeground = new Color((float)255 / 255, (float)222 / 255, (float)86 / 255);
	public static Color ColorNextWaveCountdownOutline = new Color((float)56 / 255, (float)94 / 255, (float)122 / 255);
	public static Color ColorNextWaveLabelOutline = new Color((float)56 / 255, (float)94 / 255, (float)122 / 255);

	//Boss wave
	public static Color ColorBossWaveBackground = new Color((float)255 / 255, (float)0 / 255, (float)0 / 255);
	public static Color ColorBossWaveOutline = new Color((float)116 / 255, (float)39 / 255, (float)39 / 255);
	public static Color ColorBossWaveCountdownForeground = new Color((float)255 / 255, (float)222 / 255, (float)86 / 255);
	public static Color ColorBossWaveCountdownOutline = new Color((float)56 / 255, (float)94 / 255, (float)122 / 255);
	public static Color ColorBossWaveLabelOutline = new Color((float)131 / 255, (float)52 / 255, (float)52 / 255);
	#endregion

    #region HOUSE - SIZE, BOX COLLIDER
    public static Vector2 PositionColliderDragonHouse1 = new Vector2(4, -7);
    public static Vector2 PositionColliderDragonHouse2 = new Vector2(4, -9);
    public static Vector2 PositionColliderDragonHouse3 = new Vector2(4, -9);
    public static Vector2 PositionColliderDragonHouse4 = new Vector2(4, -9);

    public static Vector2 SizeDragonHouse1 = new Vector2(67, 65);
    public static Vector2 SizeDragonHouse2 = new Vector2(59, 61);
    public static Vector2 SizeDragonHouse3 = new Vector2(88, 74);
    public static Vector2 SizeDragonHouse4 = new Vector2(88, 58);
    #endregion

    #region TOWER - NAME COLOR FOREGROUND, NAME COLOR OUTLINE, SIZE
    //Name color foreground
	public static Color ColorTowerArchitect = Color.white;
	public static Color ColorTowerFire = new Color((float)255 / 255, (float)107 / 255, (float)107 / 255);
	public static Color ColorTowerIce = new Color((float)159 / 255, (float)192 / 255, (float)255 / 255);
	public static Color ColorTowerRock = new Color((float)178 / 255, (float)157 / 255, (float)76 / 255);
    public static Color ColorTowerGold = new Color((float)255 / 255, (float)107 / 255, (float)107 / 255);
    public static Color ColorTowerPoison = new Color((float)255 / 255, (float)107 / 255, (float)107 / 255);

	//Name color outline
	public static Color ColorTowerArchitectOutline = new Color((float)109 / 255, (float)109 / 255, (float)109 / 255);
	public static Color ColorTowerFireOutline = new Color((float)148 / 255, (float)56 / 255, (float)56 / 255);
	public static Color ColorTowerIceOutline = new Color((float)34 / 255, (float)35 / 255, (float)255 / 255);
	public static Color ColorTowerRockOutline = new Color((float)96 / 255, (float)85 / 255, (float)41 / 255);
    public static Color ColorTowerGoldOutline = new Color((float)96 / 255, (float)85 / 255, (float)41 / 255);
    public static Color ColorTowerPoisonOutline = new Color((float)96 / 255, (float)85 / 255, (float)41 / 255);

	//Size
	public static Vector2 SizeTowerArchitect1 = new Vector2(50, 44);
	public static Vector2 SizeTowerArchitect2 = new Vector2(51, 47);
	public static Vector2 SizeTowerArchitect3 = new Vector2(70, 60);
	public static Vector2 SizeTowerFire1 = new Vector2(70, 70);
	public static Vector2 SizeTowerFire2 = new Vector2(61, 68);
	public static Vector2 SizeTowerFire3 = new Vector2(101, 113);
	public static Vector2 SizeTowerIce1 = new Vector2(57, 47);
	public static Vector2 SizeTowerIce2 = new Vector2(57, 53);
	public static Vector2 SizeTowerIce3 = new Vector2(59, 63);
	public static Vector2 SizeTowerRock1 = new Vector2(74, 46);
	public static Vector2 SizeTowerRock2 = new Vector2(83, 52);
	public static Vector2 SizeTowerRock3 = new Vector2(66, 63);
    public static Vector2 SizeTowerGold1 = new Vector2(52, 55);
    public static Vector2 SizeTowerGold2 = new Vector2(52, 55);
    public static Vector2 SizeTowerGold3 = new Vector2(52, 55);
    public static Vector2 SizeTowerPoison1 = new Vector2(70, 60);
    public static Vector2 SizeTowerPoison2 = new Vector2(80, 78);   
    public static Vector2 SizeTowerPoison3 = new Vector2(80, 78);
	#endregion

	#region BULLET - SIZE FOR SHOP, SIZE FOR TOWER BUILD PANEL
	//Size for shop
	public static SBulletTowerShop ShopBulletArchitect1 = new SBulletTowerShop(new Vector2(78, 39), 1.41f);
	public static SBulletTowerShop ShopBulletArchitect2 = new SBulletTowerShop(new Vector2(128, 64), 0.94f);
	public static SBulletTowerShop ShopBulletArchitect3 = new SBulletTowerShop(new Vector2(128, 64), 0.89f);
	public static SBulletTowerShop ShopBulletRock1 = new SBulletTowerShop(new Vector2(38, 38), 1.34f);
	public static SBulletTowerShop ShopBulletRock2 = new SBulletTowerShop(new Vector2(32, 32), 1.15f);
	public static SBulletTowerShop ShopBulletRock3 = new SBulletTowerShop(new Vector2(32, 32), 1.34f);
	public static SBulletTowerShop ShopBulletFire1 = new SBulletTowerShop(new Vector2(36, 18), 0.63f);
	public static SBulletTowerShop ShopBulletFire2 = new SBulletTowerShop(new Vector2(66, 33), 1.19f);
	public static SBulletTowerShop ShopBulletFire3 = new SBulletTowerShop(new Vector2(54, 54), 1.92f);
	public static SBulletTowerShop ShopBulletIce1 = new SBulletTowerShop(new Vector2(16, 16), 1.51f);
	public static SBulletTowerShop ShopBulletIce2 = new SBulletTowerShop(new Vector2(76, 38), 1.35f);
	public static SBulletTowerShop ShopBulletIce3 = new SBulletTowerShop(new Vector2(50, 50), 2.01f);
    public static SBulletTowerShop ShopBulletPoison1 = new SBulletTowerShop(new Vector2(76, 38), 1.35f);
    public static SBulletTowerShop ShopBulletPoison2 = new SBulletTowerShop(new Vector2(76, 38), 1.35f);
    public static SBulletTowerShop ShopBulletPoison3 = new SBulletTowerShop(new Vector2(76, 38), 1.35f);

	//Size for tower build panel
	public static SBulletAnchor BuildBulletArchitect1 = new SBulletAnchor(new Vector2(-0.26f, 0), new Vector2(128, 64), 1.22f);
	public static SBulletAnchor BuildBulletArchitect2 = new SBulletAnchor(new Vector2(-0.29f, 0), new Vector2(128, 64), 1.09f);
	public static SBulletAnchor BuildBulletArchitect3 = new SBulletAnchor(new Vector2(-0.29f, 0), new Vector2(128, 64), 1.09f);
	public static SBulletAnchor BuildBulletRock1 = new SBulletAnchor(new Vector2(-0.29f, 0.02f), new Vector2(32, 32), 1.46f);
	public static SBulletAnchor BuildBulletRock2 = new SBulletAnchor(new Vector2(-0.29f, 0.02f), new Vector2(32, 40), 1.3f);
	public static SBulletAnchor BuildBulletRock3 = new SBulletAnchor(new Vector2(-0.29f, 0.06f), new Vector2(32, 32), 1.27f);
	public static SBulletAnchor BuildBulletFire1 = new SBulletAnchor(new Vector2(-0.3f, -0.07f), new Vector2(32, 16), 0.75f);
	public static SBulletAnchor BuildBulletFire2 = new SBulletAnchor(new Vector2(-0.28f, 0.07f), new Vector2(128, 64), 1.37f);
	public static SBulletAnchor BuildBulletFire3 = new SBulletAnchor(new Vector2(-0.29f, 0.07f), new Vector2(64, 64), 2.05f);
	public static SBulletAnchor BuildBulletIce1 = new SBulletAnchor(new Vector2(-0.29f, 0.07f), new Vector2(16, 16), 2.02f);
	public static SBulletAnchor BuildBulletIce2 = new SBulletAnchor(new Vector2(-0.25f, 0.01f), new Vector2(64, 32), 1.45f);
	public static SBulletAnchor BuildBulletIce3 = new SBulletAnchor(new Vector2(-0.29f, 0.09f), new Vector2(64, 64), 1.74f);
    public static SBulletAnchor BuildBulletPoison1 = new SBulletAnchor(new Vector2(-0.29f, 0.07f), new Vector2(64, 32), 1.45f);
    public static SBulletAnchor BuildBulletPoison2 = new SBulletAnchor(new Vector2(-0.29f, 0.07f), new Vector2(64, 32), 1.45f);
    public static SBulletAnchor BuildBulletPoison3 = new SBulletAnchor(new Vector2(-0.29f, 0.07f), new Vector2(64, 32), 1.45f);
	#endregion

	#region ITEM - SIZE FOR SHOP, ITEM BUFF
	public static Vector2 SizeShopItemHOM =  new Vector2(86,64);
	public static Vector2 SizeShopItemATK =  new Vector2(68,64);
	public static Vector2 SizeShopItemSpawnShoot =  new Vector2(36,64);
	public static Vector2 SizeShopItemRange =  new Vector2(57,64);
	public static Vector2 SizeShopItemTower =  new Vector2(55,64);

	public static Vector2 SizeItemBuffHOM = new Vector2 (51, 52);
	public static Vector2 SizeItemBuffATK = new Vector2 (52, 52);
	public static Vector2 SizeItemBuffSpawnShoot = new Vector2 (52, 52);
	public static Vector2 SizeItemBuffTRange = new Vector2 (48, 52);

	public static Color ColorShopItemName = new Color((float)224 / 255, (float)255 / 255, (float)82 / 255);
	public static Color ColorShopItemNameOutline = new Color((float)58 / 255, (float)58 / 255, (float)58 / 255);
	public static Color ColorShopItemValue = Color.white;
	public static Color ColorShopItemValueOutline = new Color((float)38 / 255, (float)116/ 255, (float)132 / 255);

	//Item buff anchor
	public static Vector2 AnchorItemBuffStart = new Vector2 (0.06f, -0.31f);
	public const float AnchorItemBuffDistanceY = 0.83f;
	#endregion

	#region GUIDE PANEL - VALUE ANCHOR GUIDE SELECTED, GRID, ENEMY BORDER COLOR, ENEMY LEVEL COLOR
	public const float AnchorGuideSelectedTower = 0f;
	public const float AnchorGuideSelectedEnemy = 0f;
	public const float AnchorGuideSelectedNote = 0f;

	public const float AnchorTowerGuideInfoStartX = -0.03f;
	public const float AnchorTowerGuideInfoDistance = 1.1f;
	public const float AnchorTowerGuideInfoStartY = -0.12f;

	public static SGuideConfig GridGuideTower = new SGuideConfig(3, 80.0f, 90.0f);
	public static SGuideConfig GridGuideEnemy = new SGuideConfig(6, 40.0f, 40.0f);

	public static Color ColorGuideEnemyBorderSelected = new Color((float)255 / 255, (float)227 / 255, (float)0 / 255);

	//Enemy name color theo level
	public static Color ColorGuideEnemyNameLV123 = new Color((float)6 / 255, (float)218 / 255, (float)25 / 255);
	public static Color ColorGuideEnemyNameLV123Outline = new Color((float)33 / 255, (float)84 / 255, (float)29 / 255);
	public static Color ColorGuideEnemyNameLV4 = new Color((float)90 / 255, (float)210 / 255, (float)227 / 255);
	public static Color ColorGuideEnemyNameLV4Outline = new Color((float)22 / 255, (float)96 / 255, (float)107 / 255);
	public static Color ColorGuideEnemyNameLV5 = new Color((float)214 / 255, (float)142 / 255, (float)52 / 255);
	public static Color ColorGuideEnemyNameLV5Outline = new Color((float)66 / 255, (float)54 / 255, (float)16 / 255);
	public static Color ColorGuideEnemyNameLV6 = new Color((float)227 / 255, (float)60 / 255, (float)60 / 255);
	public static Color ColorGuideEnemyNameLV6Outline = new Color((float)112 / 255, (float)5 / 255, (float)5 / 255);
	#endregion

	#region EFFECT PANEL
	//BURNING
	public static Color ColorEffectPanelBurnForeground = new Color((float)163 / 255, (float)67 / 255, (float)24 / 255);
	public static Color ColorEffectPanelBurnOutline = new Color((float)253 / 255, (float)95 / 255, (float)9 / 255);
	public static Color ColorEffectPanelBurnNameGradientTop = new Color((float)249 / 255, (float)230 / 255, (float)70 / 255);
	public static Color ColorEffectPanelBurnNameGradientBottom = new Color((float)229 / 255, (float)134 / 255, (float)57 / 255);
	public static Color ColorEffectPanelBurnNameGradientOutline = new Color((float)24 / 255, (float)16 / 255, (float)5 / 255);

	//SLOW
	public static Color ColorEffectPanelSlowForeground = new Color((float)24 / 255, (float)123 / 255, (float)163 / 255);
	public static Color ColorEffectPanelSlowOutline = new Color((float)9 / 255, (float)132 / 255, (float)253 / 255);
	public static Color ColorEffectPanelSlowNameGradientTop = new Color((float)36 / 255, (float)255 / 255, (float)237 / 255);
	public static Color ColorEffectPanelSlowNameGradientBottom = new Color((float)156 / 255, (float)251 / 255, (float)255 / 255);
	public static Color ColorEffectPanelSlowNameGradientOutline = new Color((float)10 / 255, (float)50 / 255, (float)60 / 255);
	#endregion

	#region TUTORIAL PANEL
	public const string TextInstructionPage1 = "[000000]- Tap (target) to build a tower.\n\n"
			+ "- Select tower to sell or upgrade.\n\n"
			+ "- Unlock hidden tower by purchasing on (shop) \n\n"
			+ "- You will get 1 (diamond) if you kill all enemies on 1 wave.\n\n"
			+ "- Some towers have special effect like:\n"
			+ "+ burn (burn), slow  (slow), tap the icon to more infomation\n\n[-]";

	public const string TextInstructionPage2 = "[000000]- Increase tower's attributes by purchasing items on (shop)\n\n"
			+"- Tap (info) for detail information of game.\n\n"
			+"- Tap (pause) to pause game.\n\n"
			+"- Tap (option) to set option game.\n\n"
			+"- Tap (time) to set time speed game.[-]";

	public const string TextInstructionPage3 = "[000000][b][i][ae3030]* Icon notes:[/i][-][/b]\n"
			+"          (hp) Health Point (HP)               (speed)  Speed\n\n"
			+"           (atk) Attack (ATK)                      (region)  Region\n\n"
			+"           (def) Defense (DEF)                   (boss)  Boss\n\n"
			+"           (spawnshoot) Spawn Shoot                     (diamond)  Diamond\n\n"
			+"           (timebuild) Build Time                           (gold)   Gold\n[-]";
#endregion

	//Color of slow effect
	public static Color ColorSlowEffect = new Color((float)230 / 255, (float)255 / 255, (float)0 / 255);

	public static Vector2 getSizeItem(string ID)
	{
		Vector2 size = Vector2.zero;
		switch(ID)
		{
		case "HandOfMidas":
			size = SizeShopItemHOM;
			break;
		case "ATK+":
			size = SizeShopItemATK;
			break;
		case "SpawnShoot+":
			size = SizeShopItemSpawnShoot;
			break;
		case "Range+":
			size = SizeShopItemRange;
			break;
		case "Tower+":
			size = SizeShopItemTower;
			break;
		}
		return size;
	}

	public static Vector2 getSizeItemBuff(string ID)
	{
		Vector2 size = Vector2.zero;
		switch(ID)
		{
		case "HandOfMidas":
			size = SizeItemBuffHOM;
			break;
		case "ATK+":
			size = SizeItemBuffATK;
			break;
		case "SpawnShoot+":
			size = SizeItemBuffSpawnShoot;
			break;
		case "Range+":
			size = SizeItemBuffTRange;
			break;
		}
		return size;
	}

	public static object[] getTowerBuildReso(ETower type)
	{
		float stretch = 0.0f;
		Vector2 vector = Vector2.zero;
		switch (type)
		{
			#region ARCHITECT
		case ETower.ARCHITECT:
				stretch = PlayConfig.StretchTowerBuildArchitect;
				vector = PlayConfig.AnchorTowerBuildArchitect;
				break;
			#endregion
			#region ROCK
		case ETower.ROCK:
			stretch = PlayConfig.StretchTowerBuildRock;
			vector = PlayConfig.AnchorTowerBuildRock;
			break;
			#endregion
			#region ICE
		case ETower.ICE:
			stretch = PlayConfig.StretchTowerBuildIce;
			vector = PlayConfig.AnchorTowerBuildIce;
				break;
			#endregion
			#region FIRE
		case ETower.FIRE:
			stretch = PlayConfig.StretchTowerBuildFire;
			vector = PlayConfig.AnchorTowerBuildFire;
				break;
            #endregion
            #region GOLD
        case ETower.GOLD:
                stretch = PlayConfig.StretchTowerBuildGold;
                vector = PlayConfig.AnchorTowerBuildGold;
                break;
			#endregion
            #region POISON
        case ETower.POISON:
                stretch = PlayConfig.StretchTowerBuildPoison;
                vector = PlayConfig.AnchorTowerBuildPoison;
                break;
            #endregion
		}
		return new object[] {stretch, vector};
	}

	public static Vector2 getTowerIconSize(STowerID ID)
	{
		Vector2 vector = Vector2.zero;
		switch (ID.Type)
		{
			#region ARCHITECT
			case ETower.ARCHITECT:
				switch (ID.Level)
				{
					case 1:
						vector = PlayConfig.SizeTowerArchitect1;
						break;
					case 2:
						vector = PlayConfig.SizeTowerArchitect2;
						break;
					case 3:
						vector = PlayConfig.SizeTowerArchitect3;
						break;
				}
				break;
			#endregion
			#region ROCK
			case ETower.ROCK:
				switch (ID.Level)
				{
					case 1:
						vector = PlayConfig.SizeTowerRock1;
						break;
					case 2:
						vector = PlayConfig.SizeTowerRock2;
						break;
					case 3:
						vector = PlayConfig.SizeTowerRock3;
						break;
				}
				break;
			#endregion
			#region ICE
			case ETower.ICE:
				switch (ID.Level)
				{
					case 1:
						vector = PlayConfig.SizeTowerIce1;
						break;
					case 2:
						vector = PlayConfig.SizeTowerIce2;
						break;
					case 3:
						vector = PlayConfig.SizeTowerIce3;
						break;
				}
				break;
			#endregion
			#region FIRE
			case ETower.FIRE:
				switch (ID.Level)
				{
					case 1:
						vector = PlayConfig.SizeTowerFire1;
						break;
					case 2:
						vector = PlayConfig.SizeTowerFire2;
						break;
					case 3:
						vector = PlayConfig.SizeTowerFire3;
						break;
				}
				break;
			#endregion
            #region GOLD
            case ETower.GOLD:
                switch (ID.Level)
                {
                    case 1:
                        vector = PlayConfig.SizeTowerGold1;
                        break;
                    case 2:
                        vector = PlayConfig.SizeTowerGold2;
                        break;
                    case 3:
                        vector = PlayConfig.SizeTowerGold3;
                        break;
                }
                break;
            #endregion
            #region POISON
            case ETower.POISON:
                switch (ID.Level)
                {
                    case 1:
                        vector = PlayConfig.SizeTowerPoison1;
                        break;
                    case 2:
                        vector = PlayConfig.SizeTowerPoison2;
                        break;
                    case 3:
                        vector = PlayConfig.SizeTowerPoison3;
                        break;
                }
                break;
            #endregion  
            #region DRAGON HOUSE
            case ETower.DRAGON:
                switch (ID.Level)
                {
                    case 1:
                        vector = PlayConfig.SizeDragonHouse1;
                        break;
                    case 2:
                        vector = PlayConfig.SizeDragonHouse2;
                        break;
                    case 3:
                        vector = PlayConfig.SizeDragonHouse3;
                        break;
                    case 4:
                        vector = PlayConfig.SizeDragonHouse4;
                        break;
                }
                break;
            #endregion
        }
		return vector;
	}

	public static Color[] getColorTowerName(STowerID ID)
	{
		Color[] colors = new Color[2];
		switch (ID.Type)
		{
			case ETower.ARCHITECT:
				colors[0] = PlayConfig.ColorTowerArchitect;
				colors[1] = PlayConfig.ColorTowerArchitectOutline;
				break;
			case ETower.ROCK:
				colors[0] = PlayConfig.ColorTowerRock;
				colors[1] = PlayConfig.ColorTowerRockOutline;
				break;
			case ETower.ICE:
				colors[0] = PlayConfig.ColorTowerIce;
				colors[1] = PlayConfig.ColorTowerIceOutline;
				break;
			case ETower.FIRE:
				colors[0] = PlayConfig.ColorTowerFire;
				colors[1] = PlayConfig.ColorTowerFireOutline;
				break;
            case ETower.GOLD:
                colors[0] = PlayConfig.ColorTowerGold;
                colors[1] = PlayConfig.ColorTowerGoldOutline;
                break;
            case ETower.POISON:
                colors[0] = PlayConfig.ColorTowerPoison;
                colors[1] = PlayConfig.ColorTowerPoisonOutline;
                break;
		}
		return colors;
	}

	public static string getAttackType(string s)
	{
		int index = s.IndexOf('_');
		return s.Substring(0, index) + " - " + s.Substring(index + 1, s.Length - index - 1);
	}

	public static string[] getBulletType(string s)
	{
		int index = s.IndexOf('_');
		string text = s.Substring(index + 1, s.Length - index - 1);
		return new string[] { s.Substring(0, index), text.Equals("ALL") ? "LAND & AIR" : text };
	}

	public static string getTowerIcon(STowerID towerID)
	{
		return (GameConfig.PathTowerIcon + towerID.Type.ToString() + "-" + towerID.Level);
	}

	public static object[] getBulletShop(STowerID id)
	{
		SBulletTowerShop config = new SBulletTowerShop(Vector2.zero, 1);

		string s = "bullet-";
		switch (id.Type)
		{
			#region ARCHITECT
			case ETower.ARCHITECT:
				s += "architect";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.ShopBulletArchitect1;
						break;
					case 2:
						config = PlayConfig.ShopBulletArchitect2;
						break;
					case 3:
						config = PlayConfig.ShopBulletArchitect3;
						break;
				}
				break;
			#endregion
			#region ROCK
			case ETower.ROCK:
				s += "rock";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.ShopBulletRock1;
						break;
					case 2:
						config = PlayConfig.ShopBulletRock2;
						break;
					case 3:
						config = PlayConfig.ShopBulletRock3;
						break;
				}
				break;
			#endregion
			#region ICE
			case ETower.ICE:
				s += "ice";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.ShopBulletIce1;
						break;
					case 2:
						config = PlayConfig.ShopBulletIce2;
						break;
					case 3:
						config = PlayConfig.ShopBulletIce3;
						break;
				}
				break;
			#endregion
			#region FIRE
			case ETower.FIRE:
				s += "fire";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.ShopBulletFire1;
						break;
					case 2:
						config = PlayConfig.ShopBulletFire2;
						break;
					case 3:
						config = PlayConfig.ShopBulletFire3;
						break;
				}
				break;
#endregion
            #region POISON
            case ETower.POISON:
                s += "poison";
                switch (id.Level)
                {
                    case 1:
                        config = PlayConfig.ShopBulletPoison1;
                        break;
                    case 2:
                        config = PlayConfig.ShopBulletPoison2;
                        break;
                    case 3:
                        config = PlayConfig.ShopBulletPoison3;
                        break;
                }
                break;
			#endregion
		}
		s += "-" + id.Level.ToString();
		return new object[] { s, config };
	}

	public static object[] getBulletBuild(STowerID id)
	{
		SBulletAnchor config = new SBulletAnchor(Vector2.zero, Vector2.zero, 1);

		string s = "bullet-";
		switch (id.Type)
		{
			#region ARCHITECT
			case ETower.ARCHITECT:
				s += "architect";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.BuildBulletArchitect1;
						break;
					case 2:
						config = PlayConfig.BuildBulletArchitect2;
						break;
					case 3:
						config = PlayConfig.BuildBulletArchitect3;
						break;
				}
				break;
			#endregion
			#region ROCK
			case ETower.ROCK:
				s += "rock";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.BuildBulletRock1;
						break;
					case 2:
						config = PlayConfig.BuildBulletRock2;
						break;
					case 3:
						config = PlayConfig.BuildBulletRock3;
						break;
				}
				break;
			#endregion
			#region ICE
			case ETower.ICE:
				s += "ice";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.BuildBulletIce1;
						break;
					case 2:
						config = PlayConfig.BuildBulletIce2;
						break;
					case 3:
						config = PlayConfig.BuildBulletIce3;
						break;
				}
				break;
			#endregion
			#region FIRE
			case ETower.FIRE:
				s += "fire";
				switch (id.Level)
				{
					case 1:
						config = PlayConfig.BuildBulletFire1;
						break;
					case 2:
						config = PlayConfig.BuildBulletFire2;
						break;
					case 3:
						config = PlayConfig.BuildBulletFire3;
						break;
				}
				break;
			#endregion
            #region POISON
            case ETower.POISON:
                s += "poison";
                switch (id.Level)
                {
                    case 1:
                        config = PlayConfig.BuildBulletPoison1;
                        break;
                    case 2:
                        config = PlayConfig.BuildBulletPoison2;
                        break;
                    case 3:
                        config = PlayConfig.BuildBulletPoison3;
                        break;
                }
                break;
            #endregion
		}
		s += "-" + id.Level.ToString();
		return new object[] { s, config };
	}

	public static Color[] getColorEnemyName(int level)
	{
		Color[] colors = new Color[2];
		if (level <= 3)
		{
			colors[0] = ColorGuideEnemyNameLV123;
			colors[1] = ColorGuideEnemyNameLV123Outline;
		}
		else if (level == 4)
		{
			colors[0] = ColorGuideEnemyNameLV4;
			colors[1] = ColorGuideEnemyNameLV4Outline;
		}
		else if (level == 5)
		{
			colors[0] = ColorGuideEnemyNameLV5;
			colors[1] = ColorGuideEnemyNameLV5Outline;
		}
		else if (level == 6)
		{
			colors[0] = ColorGuideEnemyNameLV6;
			colors[1] = ColorGuideEnemyNameLV6Outline;
		}
		return colors;
	}

	public static string getSpeedString(float speed)
	{
		string s = "";
		if (speed <= 10)
			s = "VERY SLOW";
		else if (speed <= 12)
			s = "SLOW";
		else if (speed <= 15)
			s = "NORMAL";
		else if (speed <= 17)
			s = "FAST";
		else
			s = "VERY FAST";
		return s;
	}

	public static Color[] getColorEffectPanel(EBulletEffect type)
	{
		Color[] colors = new Color[5];
		switch (type)
		{
			case EBulletEffect.SLOW:
				colors[0] = ColorEffectPanelSlowForeground;
				colors[1] = ColorEffectPanelSlowOutline;
				colors[2] = ColorEffectPanelSlowNameGradientTop;
				colors[3] = ColorEffectPanelSlowNameGradientBottom;
				colors[4] = ColorEffectPanelSlowNameGradientOutline;
				break;
			case EBulletEffect.BURN:
				colors[0] = ColorEffectPanelBurnForeground;
				colors[1] = ColorEffectPanelBurnOutline;
				colors[2] = ColorEffectPanelBurnNameGradientTop;
				colors[3] = ColorEffectPanelBurnNameGradientBottom;
				colors[4] = ColorEffectPanelBurnNameGradientOutline;
				break;
		}
		return colors;
	}

	public static string getTextInstruction(int currentPage)
	{
		string text = "";
		switch (currentPage) 
		{
		case 1: text = TextInstructionPage1; break;
		case 2: text = TextInstructionPage2; break;
		case 3: text = TextInstructionPage3; break;
		}
		return text;
	}
}