using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RandomPasswordGenerator.Models;
using System.Reflection;

namespace RandomPasswordGenerator.Controllers
{
    public class PasswordGenerator : Controller
    {

        // GET: /<controller>/
        public IActionResult Index()
        {
            PasswordGeneratorViewModel model = new PasswordGeneratorViewModel();
            return View(model);
        }

        public IActionResult Reset()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PasswordGeneratorViewModel model)
        {
            GenerateRandomPWord(model);
            return View(model);
        }

        public static void GenerateRandomPWord(PasswordGeneratorViewModel model)
        {

            if (model.useUpper)
            {
                model.charTypesInUse.Add("upper");
            }
            if (model.useLower)
            {
                model.charTypesInUse.Add("lower");
            }
            if (model.useSpecial)
            {
                model.charTypesInUse.Add("special");
            }
            if (model.useNumber)
            {
                model.charTypesInUse.Add("number");
            }

            if (model.charTypesInUse.Count == 0)
            {
                foreach (string type in model.charTypes)
                {
                    model.charTypesInUse.Add(type);
                }
            }

            if (model.fillPassword)
            {
                model.fillLength = model.pWordLength - model.passwordInput.Length;
                model.password = String.Empty;
                GeneratePWord(model.fillLength, model);
            }
            else
            {
                GeneratePWord(model.pWordLength, model);
            }
        }


        public static void GeneratePWord(int length, PasswordGeneratorViewModel model)
        {
            for (int x = 0; x < length; x++)
            {
                model.randomCharType = model.random.Next(0, model.charTypesInUse.Count);

                if (model.charTypesInUse[model.randomCharType] == "upper")
                {
                    if (model.excludeSimilar && model.password.Contains(model.randomChar))
                    {
                        int randomNum = model.random.Next(0, 26);
                        model.randomChar = model.upper[randomNum];
                        model.newRandomChar = replaceCharWithNewUniqueRandomChar(model.password, model.randomChar, "upper", model);

                        model.password += model.newRandomChar;
                    }
                    else
                    {
                        int randomNum = model.random.Next(0, 26);
                        model.password += model.upper[model.random.Next(0, 26)];
                    }
                }
                else if (model.charTypesInUse[model.randomCharType] == "lower")
                {
                    if (model.excludeSimilar && model.password.Contains(model.randomChar))
                    {
                        model.randomChar = model.upper[model.random.Next(0, 26)].ToString().ToLower().ToCharArray()[0];
                        model.newRandomChar = replaceCharWithNewUniqueRandomChar(model.password, model.randomChar, "lower", model);
                        model.password += model.newRandomChar;
                    }
                    else
                    {
                        model.password += model.upper[model.random.Next(0, 26)].ToString().ToLower().ToCharArray()[0];
                    }
                }
                else if (model.charTypesInUse[model.randomCharType] == "special")
                {
                    if (model.excludeSimilar && model.password.Contains(model.randomChar))
                    {
                        model.randomChar = model.special[model.random.Next(0, 25)];
                        model.newRandomChar = replaceCharWithNewUniqueRandomChar(model.password, model.randomChar, "special", model);
                        model.password += model.newRandomChar;
                    }
                    else
                    {
                        model.password += model.special[model.random.Next(0, 25)];
                    }
                }
                else if (model.charTypesInUse[model.randomCharType] == "number")
                {
                    if (model.excludeSimilar && model.password.Contains(model.randomChar))
                    {
                        model.randomChar = model.random.Next(0, 10).ToString().ToCharArray()[0];
                        model.newRandomChar = replaceCharWithNewUniqueRandomChar(model.password, model.randomChar, "number", model);
                        model.password += model.newRandomChar;
                    }
                    else
                    {
                        model.password += model.randomChar = model.random.Next(0, 10).ToString().ToCharArray()[0];
                    }
                }
            }

            if (model.fillPassword)
            {
                model.password = $"{model.passwordInput}{model.password}";
            }
        }


        public static char replaceCharWithNewUniqueRandomChar(string str, char sChar, string charType, PasswordGeneratorViewModel model)
        {
            char uniqueChar = sChar;
            while (str.Contains(uniqueChar) == true)
            {
                if (charType == "upper")
                {
                    uniqueChar = model.upper[model.random.Next(0, 26)];
                }
                else if (charType == "lower")
                {
                    uniqueChar = model.upper[model.random.Next(0, 26)].ToString().ToLower().ToCharArray()[0];
                }
                else if (charType == "special")
                {
                    uniqueChar = model.special[model.random.Next(0, 25)];
                }
                else if (charType == "number")
                {
                    uniqueChar = model.randomChar = model.random.Next(0, 10).ToString().ToCharArray()[0];
                }
            }
            return uniqueChar;
        }
    }
}
