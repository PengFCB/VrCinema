using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using System.IO;


public class ScreenController : MonoBehaviour
{
    public TMP_InputField MovieTicket;
    public VideoPlayer MovieScreen;
    public string MoviePath;
    private bool IsPlay;
    void Start()
    {
        IsPlay = true;
        GetMovieUrl();
    }

    // Update is called once per frame
    void Update()
    {
        MoviePlayStatus();
        
    }
    public void FindMovieUrl(TMP_InputField MovieTicket)
    {
        MoviePath = "D:/5620-unity/video/" + MovieTicket.text + ".mp4";
        if (File.Exists(MoviePath))
        {
            ChangeMovie(MoviePath);
        }
        else
        {
            Debug.LogError("��Ƶ������");
        }
    }
    void ChangeMovie(string MoviePath)
    {
        MovieScreen.url = MoviePath;
    }
    public void GetMovieUrl()
    {
        //MoviePath = "https://firebasestorage.googleapis.com/v0/b/elec5620-43fef.appspot.com/o/2713.MP4?alt=media&token=b875382d-9349-4988-92db-c061ff0c03a1";
        MoviePath = Global.movieUrl;
        ChangeMovie(MoviePath);
    }
    void MoviePlayStatus()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsPlay)
            {
                IsPlay = true;
                MovieScreen.Play();
                Debug.Log("����������Ƶ");
            }
            else
            {            
                IsPlay = false;
                MovieScreen.Pause();
                Debug.Log("��ͣ");            
            }
        }        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MovieScreen.Stop();
            Debug.Log("ֹͣ");
        }
    }
}
