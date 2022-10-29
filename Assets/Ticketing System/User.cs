using System.Collections;
using System.Text;
using Firebase.Database;
using Firebase.Extensions;

public class User
{
    private string userId { get; set; }
    private string name { get; set; }
    private string email { get; set; }
    private string passwd { get; set; }
    private ArrayList orderList { get; set; }
    private ArrayList ticketList { get; set; }
    private ArrayList paymentList { get; set; }
    private float balance { get; set; }

    public User()
    {
        orderList = new ArrayList();
        ticketList = new ArrayList();
        paymentList = new ArrayList();

    }

    public string GetPasswd()
    {
        return passwd;
    }

    public void SetPasswd(string passwd)
    {
        this.passwd = passwd;
    }

    public string GetUserId()
    {
        return userId;
    }
    public void SetUserId(string userId)
    {
        this.userId = userId;
    }
    public string GetName()
    {
        return name;
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public string GetEmail()
    {
        return email;
    }
    public void SetEmail(string email)
    {
        this.email = email;
    }
    public ArrayList GetOrderList()
    {
        return orderList;
    }
    public ArrayList GetTicketList()
    {
        return ticketList;
    }
    public ArrayList GetPaymentList()
    {
        return paymentList;
    }
    public float GetBalance()
    {
        return balance;
    }
    public void SetBalance(float balance)
    {
        this.balance = balance;
    }
    public void PayByBalance(float price)
    {
        balance = balance - price;
    }
    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("userId: " + userId + "\n");
        stringBuilder.Append("name: " + name + "\n");
        stringBuilder.Append("passwd: " + passwd + "\n");
        stringBuilder.Append("email: " + email + "\n");
        stringBuilder.Append("balance: " + balance + "\n");
        stringBuilder.Append("orderList:\n");
        foreach (var order in orderList)
        {
            stringBuilder.Append(order.ToString());
        }
        stringBuilder.Append("ticketList:\n");
        foreach (var ticket in ticketList)
        {
            stringBuilder.Append(ticket.ToString());
        }
        stringBuilder.Append("paymentList:\n");
        foreach (var payment in paymentList)
        {
            stringBuilder.Append(payment.ToString());
        }
        return stringBuilder.ToString();
    }

}
