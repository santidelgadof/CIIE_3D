using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class LeaderBoardData : ScriptableObject
{
    [SerializeField]
    private List<(string, float)> mValue = new List<(string, float)>();

    public List<(string, float)> Lblist
    {
        get { return mValue; }
        set { mValue = value; }
    }

    [SerializeField]
    public void AddNewScore(string name, float score)
    {
        Debug.Log("añadiendo" + name + " " + score);
        // Ordena en orden descendiente (5, 4, 3...) las puntuaciones
        if (mValue == null)
        {
            mValue.Add((name, score));
        }
        else
        {
            mValue.Add((name, score));
            mValue = mValue.OrderByDescending(x => x.Item2).ToList();
            if (mValue.Count > 5)
            {
                //Toma los primeros 5 elementos de la lista y los guarda
                mValue = mValue.Take(5).ToList();
            }

        }
        Lblist = mValue;
    }

    [SerializeField]
    public List<(string, float)> getListofScores()
    {
        return mValue;
    }

}
