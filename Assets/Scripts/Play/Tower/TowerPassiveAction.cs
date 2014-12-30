using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TowerPassiveController))]
public class TowerPassiveAction : TowerAction {

    TowerPassiveController towerPassiveController;
    bool startBonus;

    public override void Start()
    {
        base.Start();
        towerPassiveController = GetComponent<TowerPassiveController>();
    }

    public override void Update()
    {

        if (isActivity && WaveController.Instance.isGameStart && !startBonus)
        {
            startBonus = true;
            StartCoroutine(CheckBonusGold());
            
        }
    }
    IEnumerator CheckBonusGold()
    {

        while (true)
        {
            yield return new WaitForSeconds(towerPassiveController.passiveAttribute.UpdateTime);
            bonusGold();
            yield return 0;
        }
    }
    public void bonusGold()
    {

        PlayInfo.Instance.Money += towerPassiveController.passiveAttribute.Value;

        GameObject temp = new GameObject();
        temp.transform.parent = PlayManager.Instance.Temp.LabelInfo.transform;
        temp.transform.position = this.transform.position;
        temp.transform.localScale = Vector3.one;
        temp.name = "Temp Gold Bonus";

       GameObject goldBonus = Instantiate(PlayManager.Instance.modelPlay.GoldBonus) as GameObject;
       goldBonus.transform.parent = temp.transform;
       goldBonus.transform.localPosition = Vector3.zero;
       goldBonus.transform.localScale = Vector3.one;
       goldBonus.GetComponent<Animator>().SetBool("isMove", true);

       UILabel label = goldBonus.GetComponent<UILabel>();
       label.text = "+" + towerPassiveController.passiveAttribute.Value.ToString();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        //base.OnTriggerEnter(other);
    }
    protected override void OnTriggerExit(Collider other)
    {
        //base.OnTriggerExit(other);
    }
}
