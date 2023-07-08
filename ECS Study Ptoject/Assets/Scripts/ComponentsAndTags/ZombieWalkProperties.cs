using Unity.Entities;

public struct ZombieWalkProperties : IComponentData, IEnableableComponent
{
    public float WalkSpeed;
    public float WalkAmplitude;
    public float WalkFrequency;

}

public struct ZombieEatProperties : IComponentData, IEnableableComponent
{
    public float EatDamagePerSecond;
    public float EatEAmplitude;
    public float EatFrequency;
}

public struct ZombieTimer : IComponentData
{
    public float Value;
}

public struct ZombieHeading : IComponentData
{
    public float Value;
}

public struct NewZombieTag : IComponentData
{

}