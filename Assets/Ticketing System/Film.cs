using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Film
{
    private string filmId;
    private string filmName;
    private string filmType;
    private string timeLength;
    private float price;
    private string description;
    private string posterUrl;
    private Sprite poster;
    private string mediaUrl;

    public Film(string filmId, string filmName, string filmType,
        string timeLength, float price, string description, string posterUrl, string mediaUrl)
    {
        this.filmId = filmId;
        this.filmName = filmName;
        this.filmType = filmType;
        this.timeLength = timeLength;
        this.price = price;
        this.description = description;
        this.posterUrl = posterUrl;
        this.mediaUrl = mediaUrl;
    }
    public string GetMediaUrl()
    {
        return mediaUrl;
    }
    public string GetPosterUrl()
    {
        return posterUrl;
    }
    public string GetFilmId()
    {
        return filmId;
    }
    public string GetFilmName()
    {
        return filmName;
    }
    public string GetFilmType()
    {
        return filmType;
    }
    public string GetTimeLength()
    {
        return timeLength;
    }
    public float GetPrice()
    {
        return price;
    }
    public Sprite GetPoster()
    {
        return poster;
    }
    public void SetPoster(Sprite sprite)
    {
        poster = sprite;
    }
    public override string ToString()
    {
        return filmId + " " + filmName + " " + filmType + " " + timeLength + " " +
            price.ToString() + " " + description + " " + posterUrl;
    }

    
}
