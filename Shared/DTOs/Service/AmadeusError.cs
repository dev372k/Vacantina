namespace Shared.DTOs.Service;

public class Error
{
    public int Code { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public int Status { get; set; }
}

public  class AmadeusError

{
    public List<Error> Errors { get; set; } 
}

