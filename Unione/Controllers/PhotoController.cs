using Microsoft.AspNetCore.Mvc;
using Unione.Models;
using Microsoft.EntityFrameworkCore;

namespace Unione.Controllers
{
    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
        private readonly AppDBContext _dbContext;

        public PhotoController(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPhotos()
        {
            var photos = await _dbContext.Photos.ToListAsync();
            return Ok(photos);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromBody] PhotoSubmitModel photoSubmitted)
        {
            if (photoSubmitted == null)
            {
                return BadRequest("Photo data is required.");
            }

            var user = await _dbContext.Users.FindAsync(photoSubmitted.UserId);

            // If the user is not found, return a NotFound status with a message
            if (user == null)
            {
                return NotFound($"User with ID {photoSubmitted.UserId} not found.");
            }

            PhotoModel photoModel = new PhotoModel();
            photoModel.FilePath = photoSubmitted.FilePath;
            photoModel.User = user;


            // Add the photoSubmitted to the database
            await _dbContext.Photos.AddAsync(photoModel);
            await _dbContext.SaveChangesAsync();

            // Return the created photo with HTTP 201 status code
            return CreatedAtAction(nameof(GetPhotoById), new { id = photoModel.Id }, photoModel);
        }

        // GET: api/photo/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhotoById(int id)
        {
            var photo = await _dbContext.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }
            return Ok(photo);
        }
    }
}

