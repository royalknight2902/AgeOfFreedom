using UnityEngine;
using System.Collections;

public enum EBranchGame
{
    IRON, //Kim
    PLANT, //Moc
    ICE, //Thuy
    FIRE, //Hoa
    EARTH, //Tho
}

public class GameConfig
{
    //public const int VersionCode = 4;
	//level
	public const int DiamondForNewPlayer = 10;
	public const int MaxCurrentEnemyLevel = 6;

	public const string GameName = "Age Of Freedom";
	public const string LinkRate = "market://details?id=com.d2m.ageoffreedom";
	public const string LinkMoreGame = "market://developer?id=Game+Is+Life+UIT";
	public const int UpdateNGUIAnchor = 4;
	public const int UpdateNGUIStretch = 4;

	public const int RenderQueueDefault = 3000;
	public const int RenderQueueBulletDown = 3010;
	public const int RenderQueueEnemy = 3200;
	public const int RenderQueueBulletEffect = 3300;
	public const int RenderQueueBulletUp = 3500;
    public const int RenderQueueSkill = 3550;
	public const int RenderQueueCollision = 3600;
	public const int RenderQueueExplosion = 3700;
	public const int RenderQueueRange = 3800;
    public const int RenderQueueFlag = 3810;
    public const int RenderQueueDragon = 3850;
    public const int RenderQueueUlti = 4090;
	//Path
	public const string DatabasePathPlay = "Database/XmlPlay";
	public const string DatabasePathTowerCost = "Database/XmlTowerCost";
	public const string DatabasePathItem = "Database/XmlItem";
    public const string DatabasePathDailyQuest = "Database/XmlDailyQuest";
    public const string DatabasePathAchievement = "Database/XmlAchievement";
    public const string DatabasePathDragonItem = "Database/XmlDragonItems";

    public const string DatabasePathSkill = "Database/SkillInfo";
    public const string DatabasePathObject = "Database/ObjectInfo";
	public const string DatabasePathEnemyInfo = "Database/EnemyInfo";
	public const string DatabasePathTowerInfo = "Database/TowerInfo";
    public const string DatabasePathTowerPassiveInfo = "Database/TowerPassiveInfo";
    public const string DatabasePathDragonHouse = "Database/DragonHouseInfo";
    public const string DatabasePathDragonPlayer = "Database/DragonPlayerInfo";
    public const string DatabasePathDragonConfig = "Database/DragonConfig";

	public const string PathMap = "Image/Map/map-";
	public const string PathTowerIcon = "Image/Tower/TowerBuildIcon/tower-";
    public const string PathHouseIcon = "Image/House/Icon/house-";
	public const string PathBulletIcon = "Image/Bullet/Bullet Icon/";
    public const string PathSkillDragonIcon = "Image/Dragon/Skill/dragon-";


	public const string ToastTowerBuyDefault = "You can't buy this tower!"; //Tower mac dinh duoc cho khi choi
	public const string ToastTowerBuyEnable = "One more tap to buy selected tower!"; //Chua mua
	public const string ToastTowerBuyUnable = "You has bought this tower!"; //Da~ mua

    public const string LocalIp = "127.0.0.1";
    public const int Port = 28000;
}
