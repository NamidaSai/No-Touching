using UnityEngine;

public class AIStats : MonoBehaviour
{
    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 3f;
    [SerializeField] float minMoveForce = 12f;
    [SerializeField] float maxMoveForce = 24f;
    [SerializeField] int minHealth = 25;
    [SerializeField] int maxHealth = 75;
    [SerializeField] int minDamage = 5;
    [SerializeField] int maxDamage = 15;
    [SerializeField] int minScore = 5;
    [SerializeField] int maxScore = 25;

    float scale;
    float moveForce;
    int health;
    int damage;
    int score;

    private void Start()
    {
        InitStats();
    }

    private void InitStats()
    {
        scale = InitFloat(minScale, maxScale);
        InitRemainingStats();
    }

    private void InitRemainingStats()
    {
        moveForce = ReMap(scale, minScale, maxScale, maxMoveForce, minMoveForce);
        health = Mathf.RoundToInt(ReMap(scale, minScale, maxScale, minHealth, maxHealth));
        damage = Mathf.RoundToInt(ReMap(scale, minScale, maxScale, minDamage, maxDamage));
        score = Mathf.RoundToInt(ReMap(scale, minScale, maxScale, minScore, maxScore));
    }

    private float InitFloat(float min, float max)
    {
        float value = Random.Range(min, max);
        return value;
    }

    private float ReMap(float aValue, float aLow, float aHigh, float bLow, float bHigh)
    {
        float normal = Mathf.InverseLerp(aLow, aHigh, aValue);
        float bValue = Mathf.Lerp(bLow, bHigh, normal);

        return bValue;
    }

    public float GetScale()
    {
        return scale;
    }

    public float GetMoveForce()
    {
        return moveForce;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetDamage()
    {
        return damage;
    }

    public int GetScore()
    {
        return score;
    }
}