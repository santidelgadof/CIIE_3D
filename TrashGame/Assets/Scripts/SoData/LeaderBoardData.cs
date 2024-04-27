using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//THIS CLASS IS NOT NECESSARY NOW

[CreateAssetMenu]
public class LeaderBoardData : ScriptableObject
{
    [SerializeField]
    private List<(string, float)> mValue = new();

    public List<(string, float)> Lblist
    {
        get { return mValue; }
        set { mValue = value; }
    }

    [SerializeField]
    public void AddNewScore(string name, float score)
    {
        // Ordena en orden descendiente (5, 4, 3...) las puntuaciones
        mValue = Lblist;
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

}
