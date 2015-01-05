using UnityEngine;
using System.Collections;

public enum ELevelDragonHouseButton
{ 
    CANCEL_DRAGON_PANEL,
    CANCEL_BAG_PANEL,
}

public class UIDragonHouse : MonoBehaviour {

    public ELevelDragonHouseButton type;

    void OnClick()
    {
        switch (type)
        {
            case ELevelDragonHouseButton.CANCEL_DRAGON_PANEL:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToCancelDragonPanel(0.2f));
                break;
            case ELevelDragonHouseButton.CANCEL_BAG_PANEL:
                audio.volume = (float)PlayerInfo.Instance.userInfo.volumeSound / 100;
                audio.PlayScheduled(0.5f);

                StartCoroutine(waitToCancelBagPanel(0.2f));
                break;
        }
    }

    IEnumerator waitToCancelDragonPanel(float time)
    {
        yield return new WaitForSeconds(time);
        if (LevelPanel.Instance.Dragon.activeSelf)
        {
            LevelPanel.Instance.Dragon.SetActive(false);

        }
    }
    IEnumerator waitToCancelBagPanel(float time)
    {
        yield return new WaitForSeconds(time);
        if (LevelPanel.Instance.Bag.activeSelf)
        {
            LevelPanel.Instance.Bag.SetActive(false);

        }
    }
}
