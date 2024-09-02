using Microsoft.AspNetCore.Mvc;
using OnlineBankPortal.Models;
using OnlineBankPortal.Data;

namespace OnlineBankPortal.Controllers.BusinessLayer
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        // POST: api/UserProfile
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserProfile userProfile)
        {
            if (userProfile == null)
            {
                return BadRequest("Invalid user profile data.");
            }

            bool created = UserProfileManager.CreateUserProfile(userProfile);

            if (created)
            {
                return Ok("User profile created successfully.");
            }
            else
            {
                return StatusCode(500, "Internal server error while creating user profile.");
            }
        }

        // GET: api/UserProfile/Username/{username}
        [HttpGet("Username/{username}")]
        public IActionResult GetUserProfileByUsername(string username)
        {
            UserProfile userProfile = UserProfileManager.GetUserProfileByUsername(username);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            else
            {
                return NotFound("User profile not found.");
            }
        }


        // GET: api/UserProfile/Email/{email}
        [HttpGet("Email/{email}")]
        public IActionResult GetUserProfileByEmail(string email)
        {
            UserProfile userProfile = UserProfileManager.GetUserProfileByEmail(email);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }
            else
            {
                return NotFound("User profile not found.");
            }
        }

        // PUT: api/UserProfile
        [HttpPut]
        public IActionResult UpdateUserProfile([FromBody] UserProfile userProfile)
        {




            if (userProfile == null)
            {
                return BadRequest("Invalid user profile data.");
            }

            bool updated = UserProfileManager.UpdateUserProfile(userProfile);

            if (updated)
            {
                return Ok("User profile updated successfully.");
            }
            else
            {
                return StatusCode(500, "Internal server error while updating user profile.");
            }
        }

        // DELETE: api/UserProfile/{userId}
        [HttpDelete("{userId}")]
        public IActionResult DeleteUserProfile(int userId)
        {
            bool deleted = UserProfileManager.DeleteUserProfile(userId);

            if (deleted)
            {
                return Ok("User profile deleted successfully.");
            }
            else
            {
                return NotFound("User profile not found or unable to delete.");
            }
        }

		/*[HttpGet("profile-picture/{userId}")]
		public IActionResult GetUserProfileProfilePicture(int userId)
		{
			UserProfile userProfile = UserProfileManager.GetUserProfileById(userId);

			if (userProfile != null)
			{
			
				// Resolve the tilde (~) to an absolute path.
				var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "pics", userProfile.ProfilePictureUrl);


				Console.WriteLine("Image Path: " + imagePath);
				Console.WriteLine("User: " + userProfile.FullName);

				if (System.IO.File.Exists(imagePath))
				{
					// Read the image file into a byte array.
					var imageBytes = System.IO.File.ReadAllBytes(imagePath);

					// Determine the MIME type of the image based on the file extension.
					// You might need to add more MIME types depending on the types of images you support.
					string mimeType = "image/jpeg"; // Default to JPEG
					if (imagePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
					{
						mimeType = "image/png";
					}
					else if (imagePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
					{
						mimeType = "image/jpg";
					}

					// Return the image data along with the appropriate Content-Type header.
					return File(imageBytes, mimeType);
				}
			}

			// If the user or image doesn't exist, return a placeholder image or a default response.
			return NotFound("User profile or profile picture not found.");
		}

	
*/

	
}
}
