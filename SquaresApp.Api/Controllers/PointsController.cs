﻿using Microsoft.AspNetCore.Mvc;
using SquaresApp.Core.Services.Points;
using PointModel = SquaresApp.Models.Points.PointModel;

namespace SquaresApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;

        public PointsController(IPointsService pointsService)
        {
            _pointsService = pointsService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportBatch(List<PointModel> points, CancellationToken cancellationToken)
        {
            var batchId = await _pointsService.ImportList(points, cancellationToken);
            return Ok(batchId);
        }

        [HttpPost]
        [Route("{batchId}")]
        public async Task<IActionResult> Add(Guid batchId, PointModel point, CancellationToken cancellationToken)
        {
            await _pointsService.Add(batchId, point, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("{batchId}")]
        public async Task<IActionResult> Delete(Guid batchId, PointModel point, CancellationToken cancellationToken)
        {
            await _pointsService.Delete(batchId, point, cancellationToken);
            return Ok();
        }
    }
}
