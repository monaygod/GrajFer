using System;
using Infrastructure.Application.Query;

namespace Service.GameRepository.Application.Queries.DownloadGame;

public class DownloadGameQuery: QueryBase<DownloadGameQueryResult>
{
    public Guid Id { get; set; }
}