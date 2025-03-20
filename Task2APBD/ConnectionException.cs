namespace ConsoleApp1;

public class ConnectionException:Exception
{
    public ConnectionException() : base("Connection failed: Network must contain 'MD Ltd.'") { }
    
}