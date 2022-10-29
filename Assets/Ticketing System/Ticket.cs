
public class Ticket
{
    private string userId { get; set; }
    private string filmId { get; set; }
    private string ticketId { get; set; }
    private bool isUsed = false;

    public Ticket(string userId, string filmId, string ticketId, bool isUsed)
    {
        this.userId = userId;
        this.filmId = filmId;
        this.ticketId = ticketId;
        this.isUsed = isUsed;
    }
    public string GetTicketId()
    {
        return ticketId;
    }
    public string GetFilmId()
    {
        return filmId;
    }
    public bool IsUsed()
    {
        return isUsed;
    }
    public void use()
    {
        isUsed = true;
    }

    public override string ToString()
    {
        return "ticketId: " + ticketId + " filmId: " + filmId + " userId: " + userId +
            " isUsed: " + isUsed + "\n";
    }
}
