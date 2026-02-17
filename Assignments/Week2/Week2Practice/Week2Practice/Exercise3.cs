using System;

public abstract class AnimeCharacterBase 
{
    public abstract string SpecialAttack();
}

public class Saiyan : AnimeCharacterBase
{
    public override string SpecialAttack()
    {
        return "Attack in action: Spirit Bomb";
    }
}

public class Ninja : AnimeCharacterBase
{
    public override string SpecialAttack()
    {
        return "Attack in action: Rasengan";
    }
}

public class Shinigami : AnimeCharacterBase
{
    public override string SpecialAttack()
    {
        return "Attack in action: Soul Reaper Slash";
    }
}
