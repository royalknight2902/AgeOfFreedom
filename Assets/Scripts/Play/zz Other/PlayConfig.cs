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
    public const int BabyDragonIndexForListStart = 75;
    public const int BabyDragonIndexForListDistance = 20;

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

	#region FOOTBAR TOWER UPGRADE PANEL - COLOR SQUARE, TOWER ICON STRETCH
	public static Color ColorTowerUpgradeDefault = new Color((float)191 / 255, (float)191 / 255, (float)149 / 255);
	public static Color ColorTowerUpgradeSelected = new Color((float)251 / 255, (float)255 / 255, (float)0 / 255);

    public static SAnchor StretchTowerUpgradeArchitect1 = new SAnchor(new Vector2(0, -0.01f), 0.95f);
    public static SAnchor StretchTowerUpgradeArchitect2 = new SAnchor(new Vector2(0, -0.01f), 0.74f);
    public static SAnchor StretchTowerUpgradeArchitect3 = new SAnchor(new Vector2(0, -0.03f), 0.74f);
    public static SAnchor StretchTowerUpgradeIce1 = new SAnchor(new Vector2(0, -0.05f), 0.77f);
    public static SAnchor StretchTowerUpgradeIce2 = new SAnchor(new Vector2(0, -0.05f), 0.67f);
    public static SAnchor StretchTowerUpgradeIce3 = new SAnchor(new Vector2(0, 0), 0.86f);
    public static SAnchor StretchTowerUpgradeRock1 = new SAnchor(new Vector2(-0.03f, -0.05f), 0.68f);
    public static SAnchor StretchTowerUpgradeRock2 = new SAnchor(new Vector2(0.03f, -0.02f), 0.77f);
    public static SAnchor StretchTowerUpgradeRock3 = new SAnchor(new Vector2(0.03f, -0.02f), 0.78f);
    public static SAnchor StretchTowerUpgradeFire1 = new SAnchor(new Vector2(0.03f, -0.02f), 0.78f);
    public static SAnchor StretchTowerUpgradeFire2 = new SAnchor(new Vector2(0, -0.02f), 0.84f);
    public static SAnchor StretchTowerUpgradeFire3 = new SAnchor(new Vector2(0, -0.02f), 0.89f);
    public static SAnchor StretchTowerUpgradeGold = new SAnchor(new Vector2(0, -0.02f), 0.89f);
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

    #region HOUSE - BOX COLLIDER POSITION
    public static Vector2 PositionColliderDragonHouse1 = new Vector2(4, -7);
    public static Vector2 PositionColliderDragonHouse2 = new Vector2(4, -9);
    public static Vector2 PositionColliderDragonHouse3 = new Vector2(4, -9);
    public static Vector2 PositionColliderDragonHouse4 = new Vector2(4, -9);
    #endregion

    #region TOWER - NAME COLOR FOREGROUND, NAME COLOR OUTLINE
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
	#endregion

	#region BULLET - SIZE FOR SHOP, SIZE FOR TOWER BUILD PANEL
	//Size for shop
    public const float StretchShopBulletArchitect1 = 1.41f;
    public const float StretchShopBulletArchitect2 = 0.94f;
    public const float StretchShopBulletArchitect3 = 0.89f;
    public const float StretchShopBulletRock1 = 1.34f;
    public const float StretchShopBulletRock2 = 1.15f;
    public const float StretchShopBulletRock3 = 0.63f;
    public const float StretchShopBulletFire1 = 1.19f;
    public const float StretchShopBulletFire2 = 1.92f;
    public const float StretchShopBulletFire3 = 1.92f;
    public const float StretchShopBulletIce1 = 1.51f;
    public const float StretchShopBulletIce2 = 1.35f;
    public const float StretchShopBulletIce3 = 2.01f;
    public const float StretchShopBulletPoison1 = 1.35f;
    public const float StretchShopBulletPoison2 = 1.35f;
    public const float StretchShopBulletPoison3 = 1.35f;

	//Size for tower build panel
    public static SAnchor BuildBulletArchitect1 = new SAnchor(new Vector2(-0.26f, 0), 1.22f);
    public static SAnchor BuildBulletArchitect2 = new SAnchor(new Vector2(-0.29f, 0), 1.09f);
    public static SAnchor BuildBulletArchitect3 = new SAnchor(new Vector2(-0.29f, 0), 1.09f);
    public static SAnchor BuildBulletRock1 = new SAnchor(new Vector2(-0.29f, 0.02f), 1.46f);
    public static SAnchor BuildBulletRock2 = new SAnchor(new Vector2(-0.29f, 0.02f), 1.3f);
    public static SAnchor BuildBulletRock3 = new SAnchor(new Vector2(-0.29f, 0.06f), 1.27f);
    public static SAnchor BuildBulletFire1 = new SAnchor(new Vector2(-0.3f, -0.07f), 0.75f);
    public static SAnchor BuildBulletFire2 = new SAnchor(new Vector2(-0.28f, 0.07f), 1.37f);
    public static SAnchor BuildBulletFire3 = new SAnchor(new Vector2(-0.29f, 0.07f), 2.05f);
    public static SAnchor BuildBulletIce1 = new SAnchor(new Vector2(-0.29f, 0.07f), 2.02f);
    public static SAnchor BuildBulletIce2 = new SAnchor(new Vector2(-0.25f, 0.01f), 1.45f);
    public static SAnchor BuildBulletIce3 = new SAnchor(new Vector2(-0.29f, 0.09f), 1.74f);
    public static SAnchor BuildBulletPoison1 = new SAnchor(new Vector2(-0.29f, 0.07f), 1.45f);
    public static SAnchor BuildBulletPoison2 = new SAnchor(new Vector2(-0.29f, 0.07f), 1.45f);
    public static SAnchor BuildBulletPoison3 = new SAnchor(new Vector2(-0.29f, 0.07f), 1.45f);
	#endregion

	#region ITEM - ITEM BUFF
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
        float stretch = -1;
		string s = "bullet-";
		switch (id.Type)
		{
			#region ARCHITECT
			case ETower.ARCHITECT:
				s += "architect";
				switch (id.Level)
				{
					case 1:
                        stretch = PlayConfig.StretchShopBulletArchitect1;
						break;
					case 2:
                        stretch = PlayConfig.StretchShopBulletArchitect2;
						break;
					case 3:
                        stretch = PlayConfig.StretchShopBulletArchitect3;
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
                        stretch = PlayConfig.StretchShopBulletRock1;
                        break;
                    case 2:
                        stretch = PlayConfig.StretchShopBulletRock2;
                        break;
                    case 3:
                        stretch = PlayConfig.StretchShopBulletRock3;
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
                        stretch = PlayConfig.StretchShopBulletIce1;
                        break;
                    case 2:
                        stretch = PlayConfig.StretchShopBulletIce2;
                        break;
                    case 3:
                        stretch = PlayConfig.StretchShopBulletIce3;
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
                        stretch = PlayConfig.StretchShopBulletFire1;
                        break;
                    case 2:
                        stretch = PlayConfig.StretchShopBulletFire2;
                        break;
                    case 3:
                        stretch = PlayConfig.StretchShopBulletFire3;
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
                        stretch = PlayConfig.StretchShopBulletPoison1;
                        break;
                    case 2:
                        stretch = PlayConfig.StretchShopBulletPoison2;
                        break;
                    case 3:
                        stretch = PlayConfig.StretchShopBulletPoison3;
                        break;
                }
                break;
			#endregion
		}
		s += "-" + id.Level.ToString();
        return new object[] { s, stretch };
	}

	public static object[] getBulletBuild(STowerID id)
	{
        SAnchor config = new SAnchor(Vector2.zero, 1);

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