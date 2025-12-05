using CarParkAPI.Data;
using CarParkAPI.Models;
using CarParkAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarParkAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarParkController : ControllerBase
    {
        readonly AppDbContext _context;

        public CarParkController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("/parking")]
        public async Task<ActionResult<ParkedVehicle_Dto>> PostParking([FromBody] VehicleParkRequest_Dto body)
        {
            bool alreadyParked = await _context.Vehicles
                .AnyAsync(v => v.Reg == body.VehicleReg);

            if (alreadyParked)
                return BadRequest(new { message = $"Vehicle with registration: {body.VehicleReg} is already parked." });

            // TODO validate registration

            var nextFreeSpace = await _context.GetParkingSpacesOrdered()
                .Where(ps => ps.ParkedVehicle == null)
                .FirstOrDefaultAsync();

            if (nextFreeSpace == null)
                return BadRequest(new { message = "No free parking spaces are currently available." });

            var vehicle = new Vehicle
            {
                Reg = body.VehicleReg,
                VehicleType = (VehicleType)body.VehicleType,
                TimeIn = DateTime.Now,
                ParkingSpace = nextFreeSpace
            };

            nextFreeSpace.ParkedVehicle = vehicle;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle.GetParkedInfo());
        }

        [HttpGet("/parking")]
        public async Task<ActionResult<ParkingSpacesAvailability_Dto>> GetParking()
        {
            int totalSpaces = await _context.ParkingSpaces
                .CountAsync();

            int occupied = await _context.ParkingSpaces
                .CountAsync(ps => ps.ParkedVehicle != null);

            var response = new ParkingSpacesAvailability_Dto
            {
                AvailableSpaces = totalSpaces - occupied,
                OccupiedSpaces = occupied
            };

            return Ok(response);
        }

        [HttpPost("/parking/exit/{reg}")]
        public async Task<ActionResult<ParkingCharge_Dto>> PostParkingExit(string reg)
        {
            // TODO validate reg

            var vehicleToExit = await _context.Vehicles
                .Include(v => v.ParkingSpace)
                .FirstOrDefaultAsync(v => v.Reg == reg);

            if (vehicleToExit == null)
            {
                return NotFound($"Vehicle with registration: {reg}, can't exit, " +
                    $"because it was never parked in the first place.");
            }

            var space = vehicleToExit.ParkingSpace;

            var timeOfExit = DateTime.Now;

            _context.Vehicles.Remove(vehicleToExit);
            space.ParkedVehicle = null;

            await _context.SaveChangesAsync();

            return Ok(new ParkingCharge_Dto
            {
                VehicleReg = vehicleToExit.Reg,
                VehicleCharge = 500,
                TimeIn = vehicleToExit.TimeIn,
                TimeOut = timeOfExit
            });
        }
    }    
}
