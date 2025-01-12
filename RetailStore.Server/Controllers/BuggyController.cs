namespace RetailStore.Server.Controllers;

public class BuggyController : BaseApiController
{
    private readonly DataContext _context;
    public BuggyController(DataContext context)
    {
        _context = context;
    }

    [HttpGet("test-auth")]
    // [Authorize]
    public ActionResult<string> GetSecretText()
    {
        return Unauthorized("Not allowed");
    }

    [HttpGet("not-found")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(42);
        if (thing == null) return NotFound(new ApiResponse(404));

        return Ok();
    }

    [HttpGet("server-error")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);
        var thingToReturn = thing.ToString();

        return Ok();
    }

    [HttpGet("bad-request")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("bad-request/{id}")]
    public ActionResult GetNotFoundRequest(int id)
    {
        return Ok();
    }
}