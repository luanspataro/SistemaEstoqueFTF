using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SistemaEstoqueFTF.Models;
using SistemaEstoqueFTF.Services;
using System.Linq;

namespace SistemaEstoqueFTF.Controllers
{
    public class ItensController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public ItensController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public async Task<IActionResult> Index(string searchString = null)
        {
            var itens = context.Itens.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                itens = itens.Where(n => n.Nome.Contains(searchString));
            }

            var itensList = await itens.OrderByDescending(p => p.Preco).ToListAsync();
            return View(itensList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemDto itemDto)
        {
            if (itemDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "A imagem é obrigatória");
            }

            if (!ModelState.IsValid)
            {
                return View(itemDto);
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(itemDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/itens/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                itemDto.ImageFile.CopyTo(stream);
            }

            Item item = new Item()
            {
                Nome = itemDto.Nome,
                Preco = itemDto.Preco,
                Raridade = itemDto.Raridade,
                Quantidade = itemDto.Quantidade,
                ImageFileName = newFileName,
                CreatedAt = DateTime.Now,
            };

            context.Itens.Add(item);
            context.SaveChanges();

            return RedirectToAction("index", "Itens");
        }

        public IActionResult Edit(int id)
        {
            var item = context.Itens.Find(id);

            if (item == null)
                return RedirectToAction("Index", "Itens");

            var itemDto = new ItemDto()
            {
                Nome = item.Nome,
                Preco = item.Preco,
                Raridade = item.Raridade,
                Quantidade = item.Quantidade,
            };

            ViewData["ItemId"] = item.Id;
            ViewData["ImageFileName"] = item.ImageFileName;
            ViewData["CreatedAt"] = item.CreatedAt.ToString("dd/MM/yyyy");

            return View(itemDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, ItemDto itemDto)
        {
            var item = context.Itens.Find(id);

            if (item == null)
                return RedirectToAction("Index", "Itens");

            if (!ModelState.IsValid)
            {
                ViewData["ItemId"] = item.Id;
                ViewData["ImageFileName"] = item.ImageFileName;
                ViewData["CreatedAt"] = item.CreatedAt.ToString("dd/MM/yyyy");

                return View(itemDto);
            }

            string newFileName = item.ImageFileName;
            if (itemDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(itemDto.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/itens/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    itemDto.ImageFile.CopyTo(stream);
                }

                // deleta a imagem antiga
                string oldImageFullPath = environment.WebRootPath + "/itens/" + item.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            item.Nome = itemDto.Nome;
            item.Preco = itemDto.Preco;
            item.Raridade = itemDto.Raridade;
            item.Quantidade = itemDto.Quantidade;
            item.ImageFileName = newFileName;

            context.SaveChanges();

            return RedirectToAction("Index", "Itens");
        }

        public IActionResult Delete(int id)
        {
            var item = context.Itens.Find(id);

            if (item == null)
                return RedirectToAction("Index", "Itens");

            string imageFullPath = environment.WebRootPath + "/itens/" + item.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Itens.Remove(item);
            context.SaveChanges(true);

            return RedirectToAction("Index", "Itens");
        }

        [HttpPost]
        public JsonResult More(int id)
        {
            var item = context.Itens.Find(id);
            if (item != null)
            {
                item.Quantidade += 1;
                context.SaveChanges();
            }
            return Json(new { quantidade = item?.Quantidade });
        }

        [HttpPost]
        public JsonResult Less(int id)
        {
            var item = context.Itens.Find(id);
            if (item != null && item.Quantidade > 0)
            {
                item.Quantidade -= 1;
                context.SaveChanges();
            }
            return Json(new { quantidade = item?.Quantidade });
        }

        [HttpGet]
        [HttpPost]
        public IActionResult AddXAll()
        {
            var items = context.Itens.Where(i => i.Raridade == "Lendário").ToList();

            if (items.Any())
            {
                foreach (var item in items)
                {
                    item.Quantidade += 1;
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Itens");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult SubXAll()
        {
            var items = context.Itens.Where(i => i.Raridade == "Lendário").ToList();

            if (items.Any(item => item.Quantidade < 1))
            {
                TempData["Erro"] = "Você precisa ter pelo menos 1 unidade de todos os lendários";
                return RedirectToAction("Index", "Itens");
            }

            foreach (var item in items)
            {
                item.Quantidade -= 1;
            }

            context.SaveChanges();
            return RedirectToAction("Index", "Itens");
        }


    }
}
