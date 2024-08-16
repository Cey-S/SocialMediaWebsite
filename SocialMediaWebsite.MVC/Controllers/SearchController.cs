using Microsoft.AspNetCore.Mvc;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
    public class SearchController : Controller
	{
		[HttpPost]
		public IActionResult Index(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				return NoContent();
			}

			string trimmedQuery = query.Trim();
			SearchData searchData = new SearchData();

			if (trimmedQuery.StartsWith('#'))
			{
				searchData.Indicator = '#'; 
				searchData.SearchedWord = trimmedQuery.Substring(1); 				
			}
			else if (trimmedQuery.StartsWith('@'))
			{
				searchData.Indicator = '@';
				searchData.SearchedWord = trimmedQuery.Substring(1);
			}

			return View(searchData);
		}
	}
}
