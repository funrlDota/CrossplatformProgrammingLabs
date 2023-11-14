using Lab5Classes;
using Lab5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using lab5.Models;

namespace Lab5.Controllers
{
    [Authorize]
    public class LabsController : Controller
    {
        public IActionResult Lab1()
        {
            return View();
        }

        public IActionResult Lab2()
        {
            return View();
        }

        public IActionResult Lab3()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(ModelLab model)
        {
            try
            {

                switch (model.NumberOfLab)
                {
                    case 1:
                        model.Output = lab1.Run(model.Input);
                        break;
                    case 2:
                        model.Output = lab2.Run(model.Input);
                        break;
                    case 3:
                        model.Output = lab3.Run(model.Input);
                        break;
                    default:
                        break;
                }

                return View($"Lab{model.NumberOfLab}", model);
            }
            catch (Exception e)
            {
                model.Output = $"Error: {e.Message}";

                return View($"Lab{model.NumberOfLab}", model);
            }

        }
    }
}