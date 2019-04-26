using Microsoft.AspNetCore.Mvc;
using Avaliacao.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using Avaliacao.Models;
using System.Globalization;
using System.Threading;

namespace Avaliacao.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersDAL _dal;
        public OrdersController(IOrdersDAL dal)
        {
            _dal = dal;
        }

        public IActionResult Index()
        {
            var ordersList = _dal.GetAllOrders().ToList();
            var quantity = ordersList.Sum(q => q.Quantity * q.Price);

            TempData["Somatoria"] = "Receita Bruta total dos Registros R$" + quantity;
            return View(ordersList);
        }

        [HttpPost]
        public IActionResult GetFile(IFormFile dados)
        {
            string ext = dados != null ? dados.ContentType : "";
            if (ext == "text/plain")
            {
                List<Order> ordersToSave = new List<Order>();
                
                var result = string.Empty;
                using (var reader = new StreamReader(dados.OpenReadStream()))
                {
                    result = reader.ReadToEnd();
                }

                Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
                string[] lines = result.Split("\r\n", System.StringSplitOptions.RemoveEmptyEntries);

                if (lines.Count() > 1)
                {
                    for (int i = 1; i < lines.Length; i++)
                    {
                        string[] fields = lines[i].Split("\t", System.StringSplitOptions.RemoveEmptyEntries);
                        Order order = new Order();
                        order.Purchaser = fields[0];
                        order.Description = fields[1];
                        order.Price = Decimal.Parse(fields[2]);
                        order.Quantity = Decimal.Parse(fields[3]);
                        order.Address = fields[4];
                        order.Provider = fields[5];
                        ordersToSave.Add(order);
                    }
                    _dal.AddOrders(ordersToSave);
                }
            }
            else
            {
                string message = ext == "" ? "Insira um arquivo para importação" : "Formato do arquivo apenas .txt";
                TempData["$AlertMessage$"] = message;
            }

            return RedirectToAction("Index");
        }
    }
}


//