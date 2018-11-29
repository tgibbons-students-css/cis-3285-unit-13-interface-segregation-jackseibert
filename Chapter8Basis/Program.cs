using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CrudImplementations;
using Model;

namespace Chapter8Basis
{
    class Program
    {
        static void Main(string[] args)
        {
            //Step One; creating a new order object and toString()
            Order ord = new Order();
            ord.product = "Vector Robot";
            ord.amount = 20;
            Console.WriteLine(ord.ToString());

            //Step Two; creating a new item object and toString()
            Item item = new Item();
            item.product = "Vector Robot";
            item.cost = 40;
            //Console.WriteLine(item.ToString());

            ItemController sep = CreateSeparateServices();
            sep.CreateItem(item);
            sep.DeleteItem(item);

            Console.WriteLine("=========CreateSeparateServices=========");
            OrderController sep = CreateSeparateServices();
            sep.CreateOrder(ord);
            sep.DeleteOrder(ord);

            Console.WriteLine("=========CreateSingleService=========");
            OrderController sing = CreateSingleService();
            sing.CreateOrder(ord);
            sing.DeleteOrder(ord);

            Console.WriteLine("=========GenericController<Order>=========");
            GenericController<Order> generic = CreateGenericServices();
            generic.CreateEntity(ord);

            Console.WriteLine("Hit any key to quit");
            Console.ReadKey();
        }

        static ItemController CreateSeparateService()
        {
            var reader = new Reader<Item>();
            var saver = new Saver<Item>();
            var deleter = new Deleter<Item>();
            return new ItemController(reader, saver, deleter);
        }

        static OrderController CreateSeparateServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            return new OrderController(reader, saver, deleter);
        }

        static OrderController CreateSingleService()
        {
            var crud = new Crud<Order>();
            return new OrderController(crud, crud, crud);
        }

        static GenericController<Order> CreateGenericServices()
        {
            var reader = new Reader<Order>();
            var saver = new Saver<Order>();
            var deleter = new Deleter<Order>();
            // This must be declared using reflection...
            GenericController<Order> ctl = (GenericController<Order>)Activator.CreateInstance(typeof(GenericController<Order>), reader, saver, deleter);
            //This does not work 
            //GenericController<Order> ctl = new GenericController(reader, saver, deleter);
            return ctl;
        }

        static GenericController<Item> CreateGenericServiceItem()
        {
            var reader = new Reader<Item>();
            var saver = new Saver<Item>();
            var deleter = new Deleter<Item>();
            // This must be declared using reflection...
            GenericController<Item> ctl = (GenericController<Item>)Activator.CreateInstance(typeof(GenericController<Item>), reader, saver, deleter);
            //This does not work 
            //GenericController<Order> ctl = new GenericController(reader, saver, deleter);
            return ctl;
        }

    }
}
