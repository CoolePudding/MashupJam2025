using UnityEngine;

[System.Serializable]
public class ResourceManager : MonoBehaviour
{
    [Header("Resource Values")]
    public int scraps;
    public int food;
    public int gasoline;
    public int trainHP;

    [Header("Capacities")]
    public int maxScraps = 999;
    public int maxFood = 50;
    public int maxGasoline = 20;
    public int maxTrainHP = 100;

    public void InitializeResources()
    {
        scraps = 10;
        food = 10;
        gasoline = 5;
        trainHP = maxTrainHP;
    }

    public bool ConsumeGasoline(int amount)
    {
        if (gasoline >= amount)
        {
            gasoline -= amount;
            return true;
        }
        return false;
    }

    public bool ConsumeFood(int amount)
    {
        if (food >= amount)
        {
            food -= amount;
            return true;
        }
        return false;
    }

    public void RepairTrain(int amount)
    {
        trainHP = Mathf.Min(trainHP + amount, maxTrainHP);
    }

    public void DamageTrain(int amount)
    {
        trainHP -= amount;
        if (trainHP <= 0)
        {
            Debug.Log("Train destroyed! Game Over!");
            //trigger GameOver logic here
        }
    }

    public void AddScraps(int amount)
    {
        scraps = Mathf.Min(scraps + amount, maxScraps);
    }

    public bool SpendScraps(int amount)
    {
        if (scraps >= amount)
        {
            scraps -= amount;
            return true;
        }
        return false;
    }
}
