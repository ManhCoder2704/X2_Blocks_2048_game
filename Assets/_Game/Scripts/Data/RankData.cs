using System;
using System.Numerics;
[Serializable]
public class RankData
{
    public string name;
    public BigInteger score;
    public string country;

    public RankData(string name, BigInteger score, string country)
    {
        this.name = name;
        this.score = score;
        this.country = country;
    }
}
