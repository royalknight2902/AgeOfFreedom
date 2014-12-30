using UnityEngine;
using System.Collections;

public class PlayerEnemy : ObjectData
{
    public string id;
    public bool visible;

    public PlayerEnemy()
    {
        this.id = "Amy";
        this.visible = false;
    }

    public PlayerEnemy(string enemyID, bool isVisible)
    {
        this.id = enemyID;
        this.visible = isVisible;
    }
}
