using System;
using System.Collections.Generic;

public class Fan(string name, string favoriteAnime)
{
    public string Name { get; set; } = name;
    public string FavoriteAnime { get; set; } = favoriteAnime;

    public override bool Equals(object fanObject) {
        return fanObject is Fan fan && Name == fan.Name; //retorna true 
    }

    public override int GetHashCode() 
    {
        return this.Name.GetHashCode(); //the identity is the Name
    }
}

// Fan fan1 = new Fan("Lucy", "Toradora");
// Fan fan2 = new Fan("Lucy", "Komi can't communicate");

//var fans = new HashSet<Fan>();
//fans.Add(fan1);
//fans.Add(fan2);
