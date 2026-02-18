using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

public class Program 
{
    public static void Main() 
    {
        /* FANS 
       Fan fan1 = new Fan("Lucy", "Toradora");
       Fan fan2 = new Fan("Lucy", "Komi can't communicate");

       var fans = new HashSet<Fan>();
       fans.Add(fan1);
       fans.Add(fan2);

       Console.WriteLine($"The unic fans by name: {fans.Count}");
        */

        IRepository<Character> characters = new InMemoryRepository<Character>();

        characters.Add(new Character("pokemon", "cute pikachu"));
        characters.Add(new Character("komi", "komi can't communicate"));

        var allCharacters = characters.GetAll();

        foreach (var character in allCharacters)
        {
            Console.WriteLine($"Existing Characters: {character.Name}, {character.Description}");
        }




        /* anime warriors
        var animeWarriors = new List<AnimeCharacterBase>
        {
            new Saiyan(),
            new Ninja(),
            new Shinigami()
        };

        foreach (var warrior in animeWarriors)
        {
            Console.WriteLine(warrior.SpecialAttack());
        }
        */


        /* SOLID exercise
        INotificationService notificationService = new UserNotification();

        AnimeEpisodeManager episodesManager = new AnimeEpisodeManager(notificationService);

        AnimeEpisode episode1 = new AnimeEpisode("Big Opening", 70);
        AnimeEpisode episode2 = new AnimeEpisode("Gran Finale", 100);

        episodesManager.AddEpisode(episode1);
        episodesManager.AddEpisode(episode2);
        */
    }

}