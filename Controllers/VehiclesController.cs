using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Core;
using System.Collections.Generic;
using vega.Core.Models;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        //El controller ya no está amarrado al EF. Solo usa interfaces (Dep.Injection). El EF quedó encapsulado en el repository + UnitOfWork.
        //Esto  facilita además las pruebas unitarias del controller, ya que se le puede proveer fake repositories, que no se comuniquen con la BD.
        private readonly IMapper mapper;       
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper, IVehicleRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;            
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody]SaveVehicleResource saveVehicleResource)
        {            
            //Input validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Business rule validation
            // if (true)
            // {
            //     ModelState.AddModelError("propertyName","errorDetail");
            //     return BadRequest(ModelState);
            // }

            //Validation necessary if it will be a public API, but here it will only be used by our angular form. 
            // var model = await context.Models.FindAsync(vehicleResource.ModelId);
            // if (model == null)
            // {
            //     ModelState.AddModelError("ModelId","Invalid ModelId");
            //     return BadRequest(ModelState);
            // }

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(saveVehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            repository.Add(vehicle);
            await unitOfWork.CompleteAsync();

            //Para devolver el vehiculo completo (saveVehicleResource solo tiene Ids)
            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody]SaveVehicleResource vehicleResource)
        {
            //Input validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Para devolver el vehiculo completo (saveVehicleResource solo tiene Ids)
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await unitOfWork.CompleteAsync();

            //Refetch el vehiculo completo
            vehicle = await repository.GetVehicle(vehicle.Id);

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound();

            repository.Remove(vehicle);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id);

            if (vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

        [HttpGet]
        public async Task<QueryResultResource<VehicleResource>> GetVehicles(VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var queryResult = await repository.GetVehicles(filter);

            return mapper.Map<QueryResult<Vehicle>, QueryResultResource<VehicleResource>>(queryResult);
        }
    }
}