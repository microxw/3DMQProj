using UnityEngine;
using System.Collections;


public class ScoreInfo
{
    // 父
    public int oyaRon;
    public int oyaTsumo;
    // 子
    public int koRon;
    public int koTsumo;

    public ScoreInfo(ScoreInfo score)
    {
        this.oyaRon = score.oyaRon;
        this.oyaTsumo = score.oyaTsumo;
        this.koRon = score.koRon;
        this.koTsumo = score.koTsumo;
    }

    public ScoreInfo(int oyaRon, int oyaTsumo, int koRon, int koTsumo)
    {
        this.oyaRon = oyaRon;
        this.oyaTsumo = oyaTsumo;
        this.koRon = koRon;
        this.koTsumo = koTsumo;
    }

    public override string ToString()
    {
        return string.Format("[ScoreInfo] oyaRon = {0}, oyaTsumo = {1}, koRon = {2}, koTsumo = {3}", oyaRon, oyaTsumo, koRon, koTsumo);
    }
}
