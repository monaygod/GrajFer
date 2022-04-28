using Infrastructure.Application.Query;
using Microsoft.AspNetCore.Mvc;

namespace Service.GameRepository.Application.Queries.DownloadGame;

public class DownloadGameQueryResult : QueryResultBase
{
    public FileContentResult File { get; set; }
}