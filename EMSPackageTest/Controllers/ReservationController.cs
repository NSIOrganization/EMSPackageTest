using EMSPackage.Application.Client;
using EMSPackage.Application.Models;
using EMSPackage.Sdk.Dto;
using EMSPackage.Sdk.Dto.Reservation;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace EMSPackageTest.Controllers;

public class ReservationController : ControllerBase
{
    [HttpPost("CreateReservation")]
    public async Task<IActionResult> Create([FromBody] EmsCreateReservationDto reservation)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5191"),
        };

        var api = RestService.For<IEmsApi>(httpClient);
        var client = new EmsSdkClient(api);
        
        var headers = new Dictionary<string, string> 
        {
            {"X-De-Username", "nikola"},
            {"X-De-Password", "123"}
        };
        
        var result = await client.CreateReservationAsync(new EmsReservationRequestModel(reservation.ReservationDate, reservation.Price, reservation.hasCupon, reservation.EventIds, reservation.UserId, "http://localhost:5191", headers));
        return Ok(result);
    }
}