using UnityEngine;
using System.Collections;

public enum EUIDragonItemsButton
{
    BACK,
    NEXT,
}

public class UIDragonItemsButton : MonoBehaviour
{
    public EUIDragonItemsButton type;

    void OnClick()
    {
        switch (type)
        {
            case EUIDragonItemsButton.BACK:
                EDragonBranch currentBranch = (EDragonBranch)Extensions.GetEnum(EDragonBranch.FIRE.GetType(), PlayerInfo.Instance.dragonInfo.id);
                EDragonBranch branch = currentBranch.Previous();

                PlayerInfo.Instance.dragonInfo.id = branch.ToString();
                PlayerInfo.Instance.dragonInfo.Save();

                DragonItemsManager.Instance.updateAttribute(branch.ToString());
                DragonItemsManager.Instance.runResources();
                break;
            case EUIDragonItemsButton.NEXT:
                currentBranch = (EDragonBranch)Extensions.GetEnum(EDragonBranch.FIRE.GetType(), PlayerInfo.Instance.dragonInfo.id);
                branch = currentBranch.Next();

                PlayerInfo.Instance.dragonInfo.id = branch.ToString();
                PlayerInfo.Instance.dragonInfo.Save();

                DragonItemsManager.Instance.updateAttribute(branch.ToString());
                DragonItemsManager.Instance.runResources();
                break;
        }
    }
}
