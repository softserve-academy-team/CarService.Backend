using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using CarService.Api.Services;
using Microsoft.AspNetCore.Authorization;
using CarService.Api.Models.DTO;

namespace CarService.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize]
        [HttpPost]
        [Route("create_review")]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreationDto reviewCreationDto)
        {
            string email = User.Identity.Name;

            return Ok(await _reviewService.CreateReview(reviewCreationDto, email));
        }

        [Authorize]
        [HttpPut]
        [Route("save_photo/{reviewId}")]
        public async Task<IActionResult> UploadPhoto(int reviewId, [FromForm] IFormFile file)
        {
            string email = User.Identity.Name;

            await _reviewService.UploadPhoto(file, reviewId, email);

            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("save_video/{reviewId}")]
        public async Task<IActionResult> UploadVideo(int reviewId, [FromForm] IFormFile file)
        {
            string email = User.Identity.Name;

            await _reviewService.UploadVideo(file, reviewId, email);

            return Ok();
        }
    }
}