
public class Order
{
    private string userId;
    private string filmId;
    private string orderId;
    private Payment payment;
    private int orderStatus; //0:done 1:pending 2:canceled

    public Order(string userId, string filmId, string orderId)
    {
        this.userId = userId;
        this.filmId = filmId;
        this.orderId = orderId;
        orderStatus = 1;
    }
    public Order(string userId, string filmId, string orderId, int orderStatus)
    {
        this.userId = userId;
        this.filmId = filmId;
        this.orderId = orderId;
        this.orderStatus = orderStatus;
    }

    public string GetUserId()
    {
        return userId;
    }

    public string GetFilmId()
    {
        return filmId;
    }

    public string GetOrderId()
    {
        return orderId;
    }
    public void SetOrderStatus(int orderStatus)
    {
        this.orderStatus = orderStatus;
    }

    public int GetOrderStatus()
    {
        return orderStatus;
    }

    public override string ToString()
    {
        return "userId: " + userId + " filmId: " + filmId + " orderId: " + orderId +
            " payment: " + payment + " orderStatus " + orderStatus + "\n";
    }
}
