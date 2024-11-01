﻿using Microsoft.AspNetCore.Mvc;
using MsSqlServerHelpers.Models;
using MsSqlServerHelpers.Repos;

namespace MsSqlServerHelpers.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class RestuarantController(ILogger<RestuarantController> log, IRestuarantRepo repo) : ControllerBase
{
    private readonly ILogger<RestuarantController> logger = log;
    private readonly IRestuarantRepo restuarantRepo = repo;

    /// <summary>
    /// Get All Restuarants
    /// </summary>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet]
    public async Task<IResult> Get()
    {
        logger.LogInformation("Get all restuarants request received");
        Restuarant[] restuarants = await restuarantRepo.GetAllRestuarants();

        if(restuarants == null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Get all restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Get All Restuarants
    /// </summary>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet("query/all")]
    public async Task<IResult> GetByQuery()
    {
        logger.LogInformation("Get all restuarants request received");
        Restuarant[] restuarants = await restuarantRepo.GetAllRestuarantsUsingQuery();

        if (restuarants == null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Get all restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Find Restuarants using matching criteria from query strings
    /// </summary>
    /// <param name="search">Object containing properties for search parameters</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("find")]
    public async Task<IResult> Find([FromBody] SearchCriteria search)
    {
        logger.LogInformation("Find restuarants request received");
        Restuarant[] restuarants = await restuarantRepo.FindRestuarants(search.Name, search.Cuisine);

        if (restuarants == null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Find restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Find Restuarants using matching criteria from query strings
    /// </summary>
    /// <param name="search">Object containing properties for search parameters</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("query/find")]
    public async Task<IResult> FindByQuery([FromBody] SearchCriteria search)
    {
        logger.LogInformation("Find restuarants request received");
        Restuarant[] restuarants = await restuarantRepo.FindRestuarantsUsingQuery(search.Name, search.Cuisine);

        if (restuarants == null || restuarants.Length == 0)
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Find restuarants request complete...returning results");
        return TypedResults.Ok(restuarants);
    }

    /// <summary>
    /// Get a Restuarant using the provided id
    /// </summary>
    /// <param name="id">Unique Identifier for a restuarant</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpGet("{id}")]
    public async Task<IResult> Restuarant(string id)
    {
        logger.LogInformation("Get restuarant request received");
        Restuarant restuarant = await restuarantRepo.GetRestuarant(id);

        if (restuarant == null ||restuarant.Id.Equals(0))
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Get restuarant request complete...returning results");
        return TypedResults.Ok(restuarant);
    }

    /// <summary>
    /// Inserts a new restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost]
    public async Task<IResult> Post([FromBody] Restuarant restuarant)
    {
        logger.LogInformation("Add restuarant request received");
        bool success = await restuarantRepo.InsertRestuarant(restuarant);

        logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }

    /// <summary>
    /// Inserts a new restuarant
    /// </summary>
    /// <param name="restuarants">Restuarant array with many items to insert</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPost("bulk")]
    public async Task<IResult> PostMany([FromBody] Restuarant[] restuarants)
    {
        logger.LogInformation("Add restuarant request received");
        bool success = await restuarantRepo.InsertRestuarants(restuarants);

        logger.LogInformation("Add restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }

    /// <summary>
    /// Updates an existing restuarant
    /// </summary>
    /// <param name="restuarant">Restuarant object to update</param>
    /// <returns>Task of Typed Results via IResult</returns>
    [HttpPut]
    public async Task<IResult> Put([FromBody] Restuarant restuarant)
    {
        logger.LogInformation("Update restuarant request received");
        bool success = await restuarantRepo.UpdateRestuarant(restuarant);

        logger.LogInformation("Update restuarant request complete...returning results");
        return TypedResults.Ok(success);
    }
}
