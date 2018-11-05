using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CoffeShop2.Models
{
    public class CoffeeRepository
    {
        public List<CoffeItem> CoffeeDB { get; set; } = new List<CoffeItem>();

        private static readonly string xmlFilepath =
            HttpContext.Current.Server.MapPath("~/App_Data/coffee_info.xml");
        
        public CoffeeRepository()
        {
            CoffeeDB = GetCoffeeList();
        }

        public CoffeItem GetCoffeeItem(int id)
        {
            var coffees = GetCoffeeList();
            if (coffees == null)
                return null;
            var coffee = GetCoffeeList().FirstOrDefault(x => x.IsEnable && x.Id == id);
            return coffee;
        }

        public List<CoffeItem> GetCoffeeList()
        {
            XDocument xdocument = XDocument.Load(xmlFilepath);
            List<CoffeItem> lst = new List<CoffeItem>();
            var query = from x in xdocument.Element("coffee").Elements("coffeeItem")
                        select new
                        {
                            Id = int.Parse(x.Attribute("id").Value),
                            Name = x.Attribute("name").Value,
                            Price = double.Parse(x.Attribute("price").Value),
                            isEnable= Convert.ToBoolean(x.Attribute("isenable").Value)
                        };
            query.ToList().ForEach(item => lst.Add(new CoffeItem
            {
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                IsEnable=item.isEnable
            }));
         
            return lst;
        }

        public bool Update(CoffeItem item)
        {
            try
            {
                XDocument xdocument = XDocument.Load(xmlFilepath);
                if (xdocument == null)
                    return false;
                var coffees = GetCoffeeList();
                if (coffees == null)
                    return false;
                var newCoffees = new List<CoffeItem>();
                if (!coffees.Any(x => x.Id == item.Id))
                {
                    XElement element = new XElement("coffeeItem", 
                        new XAttribute("id", item.Id), 
                        new XAttribute("price", item.Price.ToString()),
                        new XAttribute("name", item.Name),
                        new XAttribute("isenable", item.IsEnable));                  
                    xdocument.Element("coffee").Add(element);

                 

                }
                else
                {
                    var elem = xdocument.Element("coffee")
                  .Elements("coffeeItem")
                  .Where(e => int.Parse(e.Attribute("id").Value) == item.Id)
                  .Single();
                  if (elem != null)
                  {
                      elem.Attribute("name").Value = item.Name;
                      elem.Attribute("price").Value = item.Price.ToString();
                      elem.Attribute("isenable").Value = item.IsEnable.ToString();
                     xdocument.Save(xmlFilepath);                     
                  }
                  else return false;  
                }
                xdocument.Save(xmlFilepath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

   
    }
    
}