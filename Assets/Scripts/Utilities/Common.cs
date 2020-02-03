using System;

public enum CollectibleTypesEnum
{
    NONE,
    TYPE_X,
    TYPE_Y,
    TYPE_Z,
    TYPE_W,
    TYPE_Q
}

public enum MountableTypesEnum
{
    NONE,
    TYPE_A,
    TYPE_B,
    TYPE_C,
    TYPE_D,
    TYPE_E
}

public enum PlayerMovementModes
{
    IDLE,
    FORWARD,
    REVERSE
}

public class Utilities
{
    public static T GetRandomEnumValue<T>(T t)
    {
        Array values = Enum.GetValues(typeof(T));
        T randomEnumType = (T)values.GetValue(UnityEngine.Random.Range(1, values.Length));

        return randomEnumType;
    }
}

public class GameConstants
{
    //Players health constants
    public const float PlayerMaxHealth = 100.0f;
    public const float healthStaminaAmount_Type_A = -20f;
    public const float healthStaminaAmount_Type_B = 10f;

    //Players speed 
    public const float accelerationAmount = 0.025f;
    public const float deAccelerationAmount = 0.01f;
    public const float maxSpeedAmount = 10.0f;
    public const float minSpeedAmount = -10.0f;
    public const float defaultPlayerSpeedAmount = 5.0f;
    public const float playerControlledAmountSpeed = 2;

    //Inventory constants
    public static int MaxInventoryList = 8;
}