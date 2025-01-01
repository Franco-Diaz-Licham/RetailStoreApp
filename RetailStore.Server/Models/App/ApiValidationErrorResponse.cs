namespace RetailStore.Server.Models.App;

public class ApiValidationErrorResponse : ApiResponse
{
    public ApiValidationErrorResponse(IEnumerable<string> errors) : base(400)
    {
        Errors = errors;
    }

    public IEnumerable<string> Errors { get; set; }
}
