using Microsoft.AspNetCore.Mvc;
using SocialMedia.Interfaces;
using SocialMedia.Models;
using System.Security.Claims;

namespace SocialMedia.Controllers
{
	public class MediaController : Controller
	{
		private IDataAccessLayer<Profile> DALProfile;
		private IDataAccessLayer<Post> DALPost;

		public MediaController(IDataAccessLayer<Profile> DALProfile, IDataAccessLayer<Post> DALPost)
		{
			this.DALProfile = DALProfile;
			this.DALPost = DALPost;
		}

		private Profile GetMatchingProfile()
		{
			string currentPlayer = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (currentPlayer == null)
			{
				Console.WriteLine("Not Signed In");
				return null;
			}
			Profile data = DALProfile.GetAll().FirstOrDefault(x => x.Username.Equals(currentPlayer));
			if (data != default(Profile))
			{
				return data;
			}

			Profile p = new Profile("Unnamed", "/images/CatPfp.png", 0,0, "Undecided");
			p.Username = currentPlayer;
			DALProfile.Add(p);
			return p;
		}

		public IActionResult MyPage(int id)
		{
            Profile p = DALProfile.Get(id);
			if (p == null || p.Username == null)
			{
				Profile matching = GetMatchingProfile();
				if(matching==null) return RedirectToPage("/Account/Login", new { area = "Identity" });
                else return View(GetProfileWithPosts(GetMatchingProfile()));
            }
			return View(GetProfileWithPosts(p));
		}

        private ProfileWithPosts GetProfileWithPosts(Profile profile)
        {
            return new ProfileWithPosts(profile, DALPost.GetAll().Where(x => x.ReceiverId == profile.Username));
        }

        private ProfileWithPosts GetProfileWithPostsPersonal(Profile profile)
        {
            return new ProfileWithPosts(profile, DALPost.GetAll().Where(x => x.PosterId == profile.Username));
        }

        [HttpGet]
		public IActionResult Edit(int? id)
		{
			if(id == null)
			{
				ViewData["Error"] = "No ID!";
				return View();
			}
			else
			{
				Profile? m = DALProfile.Get(id.Value);
				if(m == null)
				{
					ViewData["Error"] = "Unknown ID!";
					return View();
				}
				return View(m);
			}
		}

		[HttpPost] 
		public IActionResult Edit(Profile m)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}

			DALProfile.Update(m);
			TempData["Success"] = "Profile updated";
			return RedirectToAction("MyPage", "Media");
		}

		public IActionResult SearchPage(string filter)
		{
			return View(DALProfile.Search(filter).ToList());
		}

        public IActionResult CreatePost(string receiver)
        {
            Profile userProfile = DALProfile.GetAll().First(x => x.Username == User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(new Post("", "", User.FindFirstValue(ClaimTypes.NameIdentifier), receiver, User.FindFirstValue(ClaimTypes.Email), userProfile.DisplayName, userProfile.Id));
        }

        public IActionResult SeePosts()
        {
            Profile p = GetMatchingProfile();
            return View("MyPage", GetProfileWithPostsPersonal(p));
        }

        public IActionResult PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreatePost", "Media", new { receiver = post.ReceiverId });
            }

            DALPost.Add(post);
            return RedirectToAction("MyPage", "Media");
        }
    }
}
