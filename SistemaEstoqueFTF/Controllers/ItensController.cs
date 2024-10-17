using Microsoft.AspNetCore.Mvc;
using SistemaEstoqueFTF.Models;
using SistemaEstoqueFTF.Services;

namespace SistemaEstoqueFTF.Controllers
{
    public class ItensController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;
        private bool mostrarEditar = false;

        public ItensController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var itens = context.Itens.OrderByDescending(p => p.Preco).ToList();
            return View(itens);
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
            if (item != null)
            {
                item.Quantidade -= 1;
                context.SaveChanges();
            }
            return Json(new { quantidade = item?.Quantidade });
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
    }
}
