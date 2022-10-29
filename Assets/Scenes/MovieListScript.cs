using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovieListScript : MonoBehaviour
{
    public GameObject orderList;
    public GameObject ticketList;
    public GameObject profile;

    public Text buyingFilmTitle;
    public Text buyingFilmPrice;
    public Button buyingFilmBuyButton;
    //public Image buyingImage
    //public ScrollRect scrollRectOrder;
    //public ScrollRect scrollRectTicket;

    Film selectedFilm;
    public static int index = 0;
    private ScrollRect scrollRect;
    private float[] rateArr;
    //获取Content的RectTransform
    private RectTransform contentTransform;
    //设置添加的预制体
    public RectTransform itemTransform;
    // Use this for initialization
    // Start is called before the first frame update
    void Start()
    {
        bool flag = true;
        Film film1 = (Film)Global.filmList[0];
        scrollRect = GetComponent<ScrollRect>();
        contentTransform = transform.Find("Viewport").Find("Content").GetComponent<RectTransform>();
        _ = transform.Find("Viewport").Find("Content").Find("MovieItem").
            Find("preview").Find("Image").GetComponentsInChildren<Text>()[0].text = film1.GetFilmName();

        transform.Find("Viewport").Find("Content").Find("MovieItem").
            Find("preview").GetComponentsInChildren<Text>()[2].text = film1.GetTimeLength();

        Image image = transform.Find("Viewport").Find("Content").Find("MovieItem").
            Find("preview").GetComponentsInChildren<Image>()[0];
        SetImageFromUrl(film1.GetPosterUrl(), image);
        film1.SetPoster(image.sprite);

        transform.Find("Viewport").Find("Content").Find("MovieItem").
            Find("preview").GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate () { BuyButton(film1); });

        index++;
        foreach (Film film in Global.filmList)
        {
            if (flag)
            {
                flag = false;
                continue;
            }
            Transform temp = Instantiate(itemTransform).transform;
            temp.SetParent(contentTransform);
            temp.localPosition = Vector3.zero;
            temp.localRotation = Quaternion.identity;
            temp.localScale = Vector3.one;
            Text[] title = temp.Find("preview").Find("Image").GetComponentsInChildren<Text>();
            Image image1 = temp.Find("preview").GetComponentsInChildren<Image>()[0];
            SetImageFromUrl(film.GetPosterUrl(), image1);
            film.SetPoster(image1.sprite);
            temp.Find("preview").GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate () { BuyButton(film); });
            title[0].text = film.GetFilmName();
            temp.Find("preview").GetComponentsInChildren<Text>()[2].text = film.GetTimeLength();
            index++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        /**
        //按A键添加子物体
        if (Input.GetKeyDown(KeyCode.A))
        {
            Transform temp = Instantiate(itemTransform).transform;
            temp.SetParent(contentTransform);
            temp.localPosition = Vector3.zero;
            temp.localRotation = Quaternion.identity;
            temp.localScale = Vector3.one;
            Text[] title = temp.Find("preview").Find("Image").GetComponentsInChildren<Text>();
            int i = index;
            temp.Find("preview").GetComponentsInChildren<Button>()[0].onClick.AddListener(delegate () { BuyButton(i.ToString()); });
            title[0].text = index.ToString();
            index++;
            

        }
        **/
    }

    private void BuyButton(Film film)
    {
        //buyingImage.sprite = film.GetPoster();
        //string title = index.ToString();
        Debug.Log(film.GetFilmName());
        buyingFilmTitle.text = film.GetFilmName();
        buyingFilmPrice.text = "$" + film.GetPrice();
        buyingFilmBuyButton.onClick.RemoveAllListeners();
        buyingFilmBuyButton.onClick.AddListener(delegate () { BuyingBuyButton(film); });
        
        //SetImageFromUrl(film.GetPosterUrl(), buyingFilmImage);
    }
    private void BuyingBuyButton(Film film)
    {
        Debug.Log("Buying BuyButton " + film.GetFilmName());
        Order order = Global.CreateNewOrder(film.GetFilmId());
        //UpdateOrderList();
        Ticket ticket = Global.PayOrderByBalance(order.GetOrderId());
        //UpdateTicketList();
        UpdateUserInterface();
        SceneManager.LoadScene("CinemaSystem");


    }

    public void SetImageFromUrl(string url, Image image)
    {
        StartCoroutine(DownloadImage(url, image)); //balanced parens CAS
    }

    IEnumerator DownloadImage(string MediaUrl, Image image)
    {      
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            image.sprite = Sprite.Create(((DownloadHandlerTexture)request.downloadHandler).texture,
                new Rect(0, 0, ((DownloadHandlerTexture)request.downloadHandler).texture.width, ((DownloadHandlerTexture)request.downloadHandler).texture.height), new Vector2(0, 0));
        
    }


    private void UpdateUserInterface()
    {
        UpdateOrderList();
        UpdateTicketList();
        UpdateProfile();
    }
    private void UpdateTicketList()
    {
        ticketList.GetComponent<TicketScript>().UpdateTicketList();
    }

    private void UpdateOrderList()
    {
        orderList.GetComponent<HistoryOrder>().UpdateOrderList();
    }
    private void UpdateProfile()
    {
        profile.GetComponent<ProfileScript>().UpdateProfile();
    }

}
