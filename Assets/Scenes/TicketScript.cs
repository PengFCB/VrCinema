using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketScript : MonoBehaviour
{
    public ScrollRect scrollRectTicket;
    public RectTransform contentTransform;
    public RectTransform itemTransform;
    // Start is called before the first frame update
    void Start()
    {
        UpdateTicketList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTicketList()
    {
        scrollRectTicket = GetComponent<ScrollRect>();
        scrollRectTicket.content.DetachChildren();

        if(Global.user.GetTicketList().Count != 0)
        {
            ArrayList arr = new ArrayList();
            foreach (Ticket ticket in Global.user.GetTicketList())
            {
                if (ticket.IsUsed() == false)
                {
                    arr.Add(ticket);
                }
            }
            if (arr.Count != 0)
            {
                //UpdateTicketList();
                foreach(Ticket ticket in arr)
                {
                    Transform temp = Instantiate(itemTransform).transform;
                    temp.SetParent(contentTransform);
                    temp.localPosition = Vector3.zero;
                    temp.localRotation = Quaternion.identity;
                    temp.localScale = Vector3.one;
                    temp.GetComponentsInChildren<Text>()[0].text = Global.GetFilm(ticket.GetFilmId()).GetFilmName();
                    temp.GetComponent<Button>().onClick.AddListener(delegate() { TicketButton(ticket); });
                }
            }
            else
            {
                Transform temp = Instantiate(itemTransform).transform;
                temp.SetParent(contentTransform);
                temp.localPosition = Vector3.zero;
                temp.localRotation = Quaternion.identity;
                temp.localScale = Vector3.one;
                temp.GetComponentsInChildren<Text>()[0].text = "No Valid Ticket";
            }
        }
        else
        {
            Transform temp = Instantiate(itemTransform).transform;
            temp.SetParent(contentTransform);
            temp.localPosition = Vector3.zero;
            temp.localRotation = Quaternion.identity;
            temp.localScale = Vector3.one;
            temp.GetComponentsInChildren<Text>()[0].text = "No Valid Ticket";
        }

    }
    private void TicketButton(Ticket ticket)
    {
        Debug.Log("Click Ticket " + Global.GetFilm(ticket.GetFilmId()).GetFilmName());
        Global.movieUrl = Global.GetFilm(ticket.GetFilmId()).GetMediaUrl();
        Global.UseTicket(ticket.GetTicketId());
        UpdateTicketList();
    }
}
