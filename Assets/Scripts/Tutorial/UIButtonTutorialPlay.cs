using UnityEngine;
using System.Collections;

public enum ETutorialPlay
{
    TARGET,
    HEART,
    COIN,
    WAVE,
    SHOP,
    PAUSE,
    OPTION,
    GUIDE,
    SKIP,
    TIME_SPEED,

    TOWER_ARCHITECT,
    TOWER_ICE,
    TOWER_POISON,
    TOWER_ROCK,
    TOWER_FIRE,
    TOWER_GOLD,
    TOWER_LOCK,
    TOWER_UNLOCK,

    TOWER_SELL,
    TOWER_UPGRADE,
    TOWER_DAMAGE,
    TOWER_SPAWN_SHOOT,
    TOWER_TIME_BUILD,
    TOWER_TYPE_BULLET,

    TOWER_SHOP,
    ITEM_SHOP,

    RESUME,
    RESTART,
    TO_MENU,

    TOWER_GUIDE,
    ENEMY_GUIDE,

    SKIP_NOT_TIME_SCALE,
}

public class UIButtonTutorialPlay : Singleton<UIButtonTutorialPlay>
{

    public ETutorialPlay type = ETutorialPlay.HEART;

    UITutorialAll uiTutorialAll;
    GameObject arrowTutorialAll;
    GameObject contentTutorialAll;
    GameObject buttonSkip;

    int countUpdate;

    void Start()
    {
        countUpdate = 0;

        uiTutorialAll = PlayManager.Instance.tutorial.GetComponent<UITutorialAll>();
        arrowTutorialAll = uiTutorialAll.arrow;
        contentTutorialAll = uiTutorialAll.content;

        int len = contentTutorialAll.transform.childCount;
        for (int i = 0; i < len; i++)
        {
            if (contentTutorialAll.transform.GetChild(i).name == "Skip")
            {
                buttonSkip = contentTutorialAll.transform.GetChild(i).gameObject;
                break;
            }
        }

        if (type == ETutorialPlay.TARGET)
        {
            arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTarget.x, TutorialDetailConfig.AnchorArrowTarget.y);
            arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTarget.z);
            contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTarget.x, TutorialDetailConfig.AnchorContentTarget.y);
            contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTarget.z);
            uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTarget;
        }

    }

    void Update()
    {
        if (countUpdate <= 6)
        {
            countUpdate++;
        }
        else if (countUpdate == 7)
        {
            arrowTutorialAll.GetComponent<UIAnchor>().enabled = false;
            contentTutorialAll.GetComponent<UIAnchor>().enabled = false;
            countUpdate++;
        }

    }

    void OnClick()
    {
        switch (type)
        {
            #region TUTORIAL GAME PLAY

            case ETutorialPlay.TARGET:
                type = ETutorialPlay.HEART;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowHeart.x, TutorialDetailConfig.AnchorArrowHeart.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowHeart.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentHeart.x, TutorialDetailConfig.AnchorContentHeart.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentHeart.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentHeart;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.HEART:
                type = ETutorialPlay.COIN;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowCoin.x, TutorialDetailConfig.AnchorArrowCoin.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowCoin.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentCoin.x, TutorialDetailConfig.AnchorContentCoin.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentCoin.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentCoin;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.COIN:
                type = ETutorialPlay.WAVE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowWave.x, TutorialDetailConfig.AnchorArrowWave.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowWave.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentWave.x, TutorialDetailConfig.AnchorContentWave.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentWave.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentWave;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.WAVE:
                type = ETutorialPlay.SHOP;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowShop.x, TutorialDetailConfig.AnchorArrowShop.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowShop.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentShop.x, TutorialDetailConfig.AnchorContentShop.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentShop.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentShop;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;


                countUpdate = 0;
                break;
            case ETutorialPlay.SHOP:
                type = ETutorialPlay.PAUSE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowPause.x, TutorialDetailConfig.AnchorArrowPause.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowPause.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentPause.x, TutorialDetailConfig.AnchorContentPause.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentPause.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentPause;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.PAUSE:
                type = ETutorialPlay.OPTION;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowOption.x, TutorialDetailConfig.AnchorArrowOption.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowOption.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentOption.x, TutorialDetailConfig.AnchorContentOption.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentOption.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentOption;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.OPTION:
                type = ETutorialPlay.GUIDE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowGuide.x, TutorialDetailConfig.AnchorArrowGuide.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowGuide.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentGuide.x, TutorialDetailConfig.AnchorContentGuide.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentGuide.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentGuide;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.GUIDE:
                type = ETutorialPlay.TIME_SPEED;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTimeSpeed.x, TutorialDetailConfig.AnchorArrowTimeSpeed.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTimeSpeed.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTimeSpeed.x, TutorialDetailConfig.AnchorContentTimeSpeed.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTimeSpeed.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTimeSpeed;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;

            case ETutorialPlay.TIME_SPEED:
                type = ETutorialPlay.SKIP;

                PlayManager.Instance.tutorial.SetActive(false);
                if (PlayManager.Instance.startBallte == null)
                    PlayManager.Instance.initStartBattle();
                break;

            #endregion


            #region TUTORIAL TOWER BUILD
            case ETutorialPlay.TOWER_UNLOCK:
                type = ETutorialPlay.TOWER_LOCK;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerLock.x, TutorialDetailConfig.AnchorArrowTowerLock.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerLock.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerLock.x, TutorialDetailConfig.AnchorContentTowerLock.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerLock.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerLock;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;

            case ETutorialPlay.TOWER_LOCK:
                Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                type = ETutorialPlay.SKIP;

                PlayManager.Instance.tutorial.SetActive(false);
                break;
            #endregion


            #region TUTORIAL UPGRADE
            case ETutorialPlay.TOWER_SELL:
                type = ETutorialPlay.TOWER_UPGRADE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerUpgrade.x, TutorialDetailConfig.AnchorArrowTowerUpgrade.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerUpgrade.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerUpgrade.x, TutorialDetailConfig.AnchorContentTowerUpgrade.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerUpgrade.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerUpgrade;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TOWER_UPGRADE:
                type = ETutorialPlay.TOWER_DAMAGE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerDamage.x, TutorialDetailConfig.AnchorArrowTowerDamage.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerDamage.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerDamage.x, TutorialDetailConfig.AnchorContentTowerDamage.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerDamage.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerDamage;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TOWER_DAMAGE:
                type = ETutorialPlay.TOWER_SPAWN_SHOOT;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowSpawnShoot.x, TutorialDetailConfig.AnchorArrowSpawnShoot.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowSpawnShoot.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentSpawnShoot.x, TutorialDetailConfig.AnchorContentSpawnShoot.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentSpawnShoot.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentSpawnShoot;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TOWER_SPAWN_SHOOT:
                type = ETutorialPlay.TOWER_TIME_BUILD;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTimeBuild.x, TutorialDetailConfig.AnchorArrowTimeBuild.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTimeBuild.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTimeBuild.x, TutorialDetailConfig.AnchorContentTimeBuild.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTimeBuild.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTimeBuild;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TOWER_TIME_BUILD:
                type = ETutorialPlay.TOWER_TYPE_BULLET;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTypeBullet.x, TutorialDetailConfig.AnchorArrowTypeBullet.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTypeBullet.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTypeBullet.x, TutorialDetailConfig.AnchorContentTypeBullet.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTypeBullet.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTypeBullet;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TOWER_TYPE_BULLET:
                Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                type = ETutorialPlay.SKIP;

                PlayManager.Instance.tutorial.SetActive(false);
                break;
            #endregion


            #region TUTORIAL SHOP GAME
            case ETutorialPlay.TOWER_SHOP:
                type = ETutorialPlay.ITEM_SHOP;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowItemShop.x, TutorialDetailConfig.AnchorArrowItemShop.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowItemShop.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentItemShop.x, TutorialDetailConfig.AnchorContentItemShop.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentItemShop.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentItemShop;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.ITEM_SHOP:
                //Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

                PlayManager.Instance.tutorial.SetActive(false);
                break;
            #endregion


            #region TUTORIAL OPTION GAME
            case ETutorialPlay.RESUME:
                type = ETutorialPlay.RESTART;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowRestart.x, TutorialDetailConfig.AnchorArrowRestart.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowRestart.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentRestart.x, TutorialDetailConfig.AnchorContentRestart.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentRestart.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentRestart;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.RESTART:
                type = ETutorialPlay.TO_MENU;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowToMenu.x, TutorialDetailConfig.AnchorArrowToMenu.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowToMenu.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentToMenu.x, TutorialDetailConfig.AnchorContentToMenu.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentToMenu.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentToMenu;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.TO_MENU:
                //Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

                PlayManager.Instance.tutorial.SetActive(false);
                break;
            #endregion


            #region TUTORIAL GUIDE GAME
            case ETutorialPlay.TOWER_GUIDE:
                type = ETutorialPlay.ENEMY_GUIDE;

                arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowEnemyGuide.x, TutorialDetailConfig.AnchorArrowEnemyGuide.y);
                arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowEnemyGuide.z);
                contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentEnemyGuide.x, TutorialDetailConfig.AnchorContentEnemyGuide.y);
                contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentEnemyGuide.z);
                uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentEnemyGuide;
                arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
                contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

                countUpdate = 0;
                break;
            case ETutorialPlay.ENEMY_GUIDE:
                //Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

                PlayManager.Instance.tutorial.SetActive(false);
                break;
            #endregion

            case ETutorialPlay.SKIP:
                PlayManager.Instance.tutorial.SetActive(false);
                if (PlayManager.Instance.startBallte == null)
                    PlayManager.Instance.initStartBattle();
                Time.timeScale = PlayerInfo.Instance.userInfo.timeScale;
                break;

            case ETutorialPlay.SKIP_NOT_TIME_SCALE:
                PlayManager.Instance.tutorial.SetActive(false);
                break;
        }
    }

    public void StartTutorialBuildTower()
    {
        Time.timeScale = 0.0f;
        type = ETutorialPlay.TOWER_UNLOCK;
        buttonSkip.GetComponent<UIButtonTutorialPlay>().type = ETutorialPlay.SKIP;

        arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerUnlock.x, TutorialDetailConfig.AnchorArrowTowerUnlock.y);
        arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerUnlock.z);
        contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerUnlock.x, TutorialDetailConfig.AnchorContentTowerUnlock.y);
        contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerUnlock.z);
        uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerUnlock;
        arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
        contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

        countUpdate = 0;
    }

    public void startTutorialUpgrade()
    {
        Time.timeScale = 0.0f;
        type = ETutorialPlay.TOWER_SELL;
        buttonSkip.GetComponent<UIButtonTutorialPlay>().type = ETutorialPlay.SKIP;

        arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerSell.x, TutorialDetailConfig.AnchorArrowTowerSell.y);
        arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerSell.z);
        contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerSell.x, TutorialDetailConfig.AnchorContentTowerSell.y);
        contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerSell.z);
        uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerSell;
        arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
        contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

        countUpdate = 0;
    }

    public void startTutorialShop()
    {
        Time.timeScale = 0.0f;
        type = ETutorialPlay.TOWER_SHOP;
        buttonSkip.GetComponent<UIButtonTutorialPlay>().type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

        arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerShop.x, TutorialDetailConfig.AnchorArrowTowerShop.y);
        arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerShop.z);
        contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentTowerShop.x, TutorialDetailConfig.AnchorContentTowerShop.y);
        contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentTowerShop.z);
        uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentTowerShop;
        arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
        contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

        countUpdate = 0;
    }

    public void startTutorialOption()
    {
        Time.timeScale = 0.0f;
        type = ETutorialPlay.RESUME;
        buttonSkip.GetComponent<UIButtonTutorialPlay>().type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

        arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowResume.x, TutorialDetailConfig.AnchorArrowResume.y);
        arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowResume.z);
        contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentResume.x, TutorialDetailConfig.AnchorContentResume.y);
        contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentResume.z);
        uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentResume;
        arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
        contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

        countUpdate = 0;
    }

    public void startTutorialGuide()
    {
        Time.timeScale = 0.0f;
        type = ETutorialPlay.TOWER_GUIDE;
        buttonSkip.GetComponent<UIButtonTutorialPlay>().type = ETutorialPlay.SKIP_NOT_TIME_SCALE;

        arrowTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorArrowTowerGuide.x, TutorialDetailConfig.AnchorArrowTowerGuide.y);
        arrowTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorArrowTowerGuide.z);
        contentTutorialAll.GetComponent<UIAnchor>().relativeOffset = new Vector2(TutorialDetailConfig.AnchorContentEnemyGuide.x, TutorialDetailConfig.AnchorContentEnemyGuide.y);
        contentTutorialAll.transform.localRotation = Quaternion.Euler(0, 0, TutorialDetailConfig.AnchorContentEnemyGuide.z);
        uiTutorialAll.labelContent.text = TutorialDetailConfig.ContentEnemyGuide;
        arrowTutorialAll.GetComponent<UIAnchor>().enabled = true;
        contentTutorialAll.GetComponent<UIAnchor>().enabled = true;

        countUpdate = 0;
    }
}
