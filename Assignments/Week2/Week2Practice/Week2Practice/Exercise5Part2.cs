using System;
using System.Collections.Generic;


public class AnimeEpisode
{
    public string Title { get; set; }
    public int EpisodeLength { get; set; }

    public AnimeEpisode(string title, int episodeLegth)
    {
        Title = title;
        EpisodeLength = episodeLegth;
    }
}

public class EpisodesValidator
{

    public bool isEpisodeValid(AnimeEpisode animeEpisode)
    {
        if (animeEpisode.Title == null || animeEpisode.Title == "")
        {
            return false;
        }
        else if (animeEpisode.EpisodeLength <= 0)
        {
            return false;
        }
        return true;
    }

}

public class EpisodesRepository
{
    public List<AnimeEpisode> episodes;
    public EpisodesRepository()
    {
        episodes = new List<AnimeEpisode>();
    }

    public void AddEpisode(AnimeEpisode animeEpisode)
    {
        episodes.Add(animeEpisode);
    }

    public List<AnimeEpisode> GetEpisodesList() { return episodes; }

}

public interface INotificationService
{
    void SendNotification(string message);
}

public class UserNotification : INotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine(message);
    }
}

public class AnimeEpisodesManager
{
    private EpisodesValidator episodesValidator;
    private EpisodesRepository episodesRepository;
    private INotificationService notificationService;

    public AnimeEpisodesManager(INotificationService notificationService)
    {
        this.episodesValidator = new EpisodesValidator();
        this.episodesRepository = new EpisodesRepository();
        this.notificationService = notificationService;
    }

    public void AddEpisode(AnimeEpisode animeEpisode)
    {
        if (episodesValidator.isEpisodeValid(animeEpisode))
        {
            episodesRepository.AddEpisode(animeEpisode);
            notificationService.SendNotification($"Your episode {animeEpisode.Title} was added");
        }
        else
        {
            notificationService.SendNotification($"The episode {animeEpisode.Title} is invalid");
        }
    }

}