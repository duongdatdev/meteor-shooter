using MeteorShooter;
using NUnit.Framework;
using UnityEngine;

public class MeteorShooterTests
{
    [Test]
    public void DifficultyStartsAtZero() => Assert.AreEqual(0f, GameManager.CalculateDifficulty(0, 0f));

    [Test]
    public void DifficultyIncreasesWithScoreAndTime()
    {
        float early = GameManager.CalculateDifficulty(50, 10f);
        float later = GameManager.CalculateDifficulty(300, 60f);
        Assert.Greater(later, early);
    }

    [Test]
    public void DifficultyIsClamped() => Assert.AreEqual(1f, GameManager.CalculateDifficulty(5000, 500f));

    [Test]
    public void NewSessionStartsWithThreeLivesAndZeroScore()
    {
        GameObject go = new GameObject("GameManagerTest");
        GameManager manager = go.AddComponent<GameManager>();
        manager.InitializeSession();
        Assert.AreEqual(GameManager.StartingLives, manager.Lives);
        Assert.AreEqual(0, manager.Score);
        Object.DestroyImmediate(go);
    }

    [Test]
    public void ScoreIncrementsOncePerAward()
    {
        GameObject go = new GameObject("GameManagerTest");
        GameManager manager = go.AddComponent<GameManager>();
        manager.AddAsteroidScore();
        Assert.AreEqual(GameManager.PointsPerAsteroid, manager.Score);
        Object.DestroyImmediate(go);
    }
}
