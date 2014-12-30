using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct SWave
{
    // id
    public int WaveID;
    // container enemy for wave;
    public System.Collections.Generic.List<SEnemyWave> Enemies;
    // time show of this wave
    public float TimeWave;
    // Time show of this wave enemy
    public float TimeEnemy;
    // Total enemy of wave
    public int TotalEnemy;

    //has boss
    public bool hasBoss;

    public SWave(int totalEnemy)
    {
        this.Enemies = new List<SEnemyWave>();
        this.TimeEnemy = 0.0f;
        this.TimeWave = 0.0f;
        this.TotalEnemy = totalEnemy;
        this.WaveID = 1;
        this.hasBoss = false;
    }
}