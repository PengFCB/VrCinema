using System;
using System.Collections;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global
{
    public static string currentUser = "";
    public static string movieUrl = "";
    public static ArrayList filmList = new ArrayList();
    public static User user;
    public static string instanceUrl = "https://elec5620-43fef-default-rtdb.firebaseio.com";
    public static DatabaseReference reference = FirebaseDatabase.GetInstance("https://elec5620-43fef-default-rtdb.firebaseio.com").RootReference;
    public static string playFilmId = "";
    public static void UpdateFilmList()
    {
        FirebaseDatabase.GetInstance("https://elec5620-43fef-default-rtdb.firebaseio.com").
            GetReference("Films").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                // Handle the error...
                Debug.Log("Database Error");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...
                ArrayList newList = new ArrayList();
                foreach (var childSnapshot in snapshot.Children)
                {
                    Film film = new Film(
                        childSnapshot.Child("filmId").Value.ToString(),
                        childSnapshot.Child("filmName").Value.ToString(),
                        childSnapshot.Child("filmType").Value.ToString(),
                        childSnapshot.Child("timeLength").Value.ToString(),
                        float.Parse(childSnapshot.Child("price").Value.ToString()),
                        childSnapshot.Child("description").Value.ToString(),
                        childSnapshot.Child("posterUrl").Value.ToString(),
                        childSnapshot.Child("mediaUrl").Value.ToString());
                    newList.Add(film);
                    Debug.Log("Successfully add film id " + film.ToString());
                }
                filmList = newList;
                Debug.Log("Update Film List Finish");
            }
        });


        
    }

    public static void GetUserFromDatabase()
    {
        Global.user = new User();
        reference.Child("Users").Child(Global.currentUser)
      .GetValueAsync().ContinueWithOnMainThread(task => {
          if (task.IsFaulted)
          {
              // Handle the error...
              Debug.Log("Database Error...");
          }
          else if (task.IsCompleted)
          {
              DataSnapshot snapshot = task.Result;
              // Do something with snapshot...
              //informationText.text = "User: " + Global.currentUser + "\nEmail: " + snapshot.Child("email").Value.ToString();
              //Global.user.userId.set
              Global.user.SetUserId(currentUser);
              Global.user.SetName(currentUser);
              Global.user.SetEmail(snapshot.Child("email").Value.ToString());
              Global.user.SetPasswd(snapshot.Child("passwd").Value.ToString());
              Global.user.SetBalance(float.Parse(snapshot.Child("balance").Value.ToString()));
              
              foreach (DataSnapshot snap in snapshot.Child("orderList").Children)
              {
                  string orderId = snap.Child("orderId").Value.ToString();
                  string filmId = snap.Child("filmId").Value.ToString();
                  //Payment
                  int orderStatus = int.Parse(snap.Child("orderStatus").Value.ToString());
                  Global.user.GetOrderList().Add(new Order(currentUser, filmId, orderId, orderStatus));
              }
              foreach (DataSnapshot snap in snapshot.Child("ticketList").Children)
              {
                  string ticketId = snap.Child("ticketId").Value.ToString();
                  string filmId = snap.Child("filmId").Value.ToString();
                  bool isUsed = bool.Parse(snap.Child("isUsed").Value.ToString());
                  Global.user.GetTicketList().Add(new Ticket(currentUser, filmId, ticketId, isUsed));
              }
              
              
              
              
              
              //PaymentList

              Debug.Log("Successful Get User's data from database!\n" + Global.user.ToString());
          }
      });
    }
    /**
    public static void WriteUserToDatabase()
    {
        DatabaseReference userReference = Global.reference.Child("Users").Child(Global.currentUser);
        
    }
    **/
    public static Order CreateNewOrder(string filmId)
    {
        DatabaseReference orderReference = Global.reference.Child("Users")
            .Child(Global.currentUser).Child("orderList");
        string orderId = Global.currentUser + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
        Order order = new Order(Global.currentUser, filmId, orderId);
        Global.user.GetOrderList().Add(order);
        orderReference.Child(orderId).Child("userId").SetValueAsync(Global.currentUser);
        orderReference.Child(orderId).Child("orderId").SetValueAsync(orderId);
        orderReference.Child(orderId).Child("filmId").SetValueAsync(filmId);
        orderReference.Child(orderId).Child("orderStatus").SetValueAsync(1);
        Debug.Log("Order Created!");
        return order;
    }
    public static Ticket PayOrderByBalance(string orderId)
    {
        DatabaseReference userReference = Global.reference.Child("Users")
            .Child(Global.currentUser);
        Order order = Global.GetOrder(orderId);
        if(order.GetOrderStatus() != 1)
        {
            Debug.Log("Order cannot be paid!");
            return null;
        }
        float price = Global.GetFilmPrice(order.GetFilmId());
        user.PayByBalance(price);
        userReference.Child("balance").SetValueAsync(user.GetBalance());
        order.SetOrderStatus(0);
        userReference.Child("orderList").Child(orderId).Child("orderStatus").SetValueAsync(0);
        return NewTicket(order.GetFilmId());
    }

    public static Ticket NewTicket(string filmId)
    {
        DatabaseReference ticketReference = Global.reference.Child("Users")
            .Child(Global.currentUser).Child("ticketList");

        string ticketId = Global.currentUser + DateTime.UtcNow.ToString("yyyyMMddHHmmssffff");
        Ticket ticket = new Ticket(currentUser, filmId, ticketId, false);
        user.GetTicketList().Add(ticket);
        ticketReference.Child(ticketId).Child("userId").SetValueAsync(currentUser);
        ticketReference.Child(ticketId).Child("filmId").SetValueAsync(filmId);
        ticketReference.Child(ticketId).Child("ticketId").SetValueAsync(ticketId);
        ticketReference.Child(ticketId).Child("isUsed").SetValueAsync(false);
        return ticket;
    }

    public static void UseTicket(string ticketId)
    {
        DatabaseReference ticketReference = Global.reference.Child("Users")
            .Child(Global.currentUser).Child("ticketList").Child(ticketId);
        Ticket ticket = Global.GetTicket(ticketId);
        if (ticket.IsUsed())
        {
            Debug.Log("Ticket was used!");
            return;
        }
        ticket.use();
        ticketReference.Child("isUsed").SetValueAsync(true);
        Global.playFilmId = ticket.GetFilmId();
        SceneManager.LoadScene("Demo");//LoadScenesToWathcingRoom!!!
    }

    public static Film GetFilm(string filmId)
    {
        foreach (Film film in Global.filmList)
        {
            if (film.GetFilmId().Equals(filmId))
                return film;
        }
        return null;
    }
    
    public static float GetFilmPrice(string filmId)
    {
        foreach(Film film in Global.filmList)
        {
            if (film.GetFilmId().Equals(filmId))
                return film.GetPrice();
        }
        return -1;
    }

    public static Order GetOrder(string orderId)
    {
        foreach(Order order in Global.user.GetOrderList())
        {
            if (order.GetOrderId().Equals(orderId))
                return order;
        }
        return null;
    }

    public static Ticket GetTicket(string ticketId)
    {
        foreach (Ticket ticket in Global.user.GetTicketList())
        {
            if (ticket.GetTicketId().Equals(ticketId))
                return ticket;
        }
        return null;
    }

}
