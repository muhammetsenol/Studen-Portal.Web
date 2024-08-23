using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Web.Data;
using StudentPortal.Web.Models;
using StudentPortal.Web.Models.Entities;

namespace StudentPortal.Web.Controllers
{
	public class StudentsController : Controller
	{
		public StudentsController(ApplicationDbContext DbContext)
		{
			this.DbContext = DbContext;
		}

		public ApplicationDbContext DbContext { get; }

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add(AddStudentViewModel ViewModel)
		{
			var student = new Student
			{
				Name = ViewModel.Name,
				Email = ViewModel.Email,
				Phone = ViewModel.Phone,
				Subscribed = ViewModel.Subscribed,
			};
			await DbContext.Students.AddAsync(student);
			await DbContext.SaveChangesAsync();
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var Students = await DbContext.Students.ToListAsync();

			return View(Students);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var Student = await DbContext.Students.FindAsync(id);
			return View(Student);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Student ViewModel)
		{
			var Student = await DbContext.Students.FindAsync(ViewModel.Id);

			if (Student is not null)
			{
				Student.Name = ViewModel.Name;
				Student.Email = ViewModel.Email;
				Student.Phone = ViewModel.Phone;
				Student.Subscribed = ViewModel.Subscribed;
				await DbContext.SaveChangesAsync();
			}
			return RedirectToAction("List", "Students");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(Student ViewModel)	
		{

			var Student = await DbContext.Students
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id == ViewModel.Id);
			if (Student is not null)
			{
				DbContext.Students.Remove(ViewModel);
				await DbContext.SaveChangesAsync();
			}
			return RedirectToAction("List", "Students");
		}
	 

    }

}