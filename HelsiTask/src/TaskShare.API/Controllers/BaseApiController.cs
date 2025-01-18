using Microsoft.AspNetCore.Mvc;

namespace TaskShare.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}")]
public abstract class BaseApiController : ControllerBase;