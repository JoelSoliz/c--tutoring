using System;
using System.Collections.Generic;


public class AnimeEpisodes
{
    public string Title { get; set; }
    public int EpisodeLength { get; set; }

    public AnimeEpisodes(string title, int episodeLegth)
    {
        Title = title;
        EpisodeLength = episodeLegth;
    }
}

public class AnimeEpisodesOrchestator
{
    public List<AnimeEpisodes> episodes;

    public AnimeEpisodesOrchestator()
    {
        episodes = new List<AnimeEpisodes>();
    }

    public bool isEpisodeValid(AnimeEpisodes animeEpisode)
    {
        if (animeEpisode.Title == null || animeEpisode.Title == "") {
            return false;
        } else if (animeEpisode.EpisodeLength <= 0) 
        { 
            return false;
        }
        return true;
    }

    public void AddEpisode(AnimeEpisodes animeEpisode) {
        if (isEpisodeValid(animeEpisode)){
            episodes.Add(animeEpisode);
            sendNotification("Episode added successfully");
        } else {
            sendNotification("Invalid Episode!");
        }
    }

    public void sendNotification(string message) {
        Console.WriteLine(message);
    }
}