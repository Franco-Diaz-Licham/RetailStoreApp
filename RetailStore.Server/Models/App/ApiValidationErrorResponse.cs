namespace RetailStore.Server.Models.App;

public class ApiValidationErrorResponse : ApiResponse
{
    public ApiValidationErrorResponse(IEnumerable<string> errors) : base(400)
    {
        ValidationErrors = errors;
    }

    public IEnumerable<string> ValidationErrors { get; set; }
}
