namespace Infrastructure.Contracts
{
    public interface IEntityService
    {
        string Message { get; set; }
        string StackTrace { get; set; }
    }
}
