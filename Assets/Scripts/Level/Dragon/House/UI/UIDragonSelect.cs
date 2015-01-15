using UnityEngine;
using System.Collections;

public class UIDragonSelect : MonoBehaviour {
    public EDragonBranch branch;

    void OnClick()
    {
        if (LevelManager.Instance.Objects.selectedDragon.transform.position != this.transform.position)
        {
            LevelManager.Instance.Objects.selectedDragon.transform.position = this.transform.position;

            PlayerInfo.Instance.dragonInfo.id = branch.ToString();
            PlayerInfo.Instance.dragonInfo.Save();

            LevelDragonManager.Instance.updateSelectedDragon(branch.ToString());
            SelectDragonController.Instance.updateAttribute(branch.ToString());
            SelectDragonController.Instance.updateSkill(branch.ToString());
            SelectDragonController.Instance.runResources();
        }
    }
}
