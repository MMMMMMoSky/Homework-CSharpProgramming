using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService
{

    class MainClass
    {
        public static void Main()
        {
            OrderService os = AddOrdersManually();
            SerializeTest(os);
        }

        public static void SerializeTest(OrderService os)
        {
            try
            {
                os.ExportToXml("test.xml");
                OrderService os2 = new OrderService();
                os2.ImportFromXml("test.xml");
                os2.ExportToXml("test2.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void QueryTest(OrderService os)
        {
            try
            {
                Console.WriteLine(">>> Get All Orders <<<");
                os.QueryAllOrders().ForEach(s => Console.WriteLine(s));
                
                Console.WriteLine(">>> Sort Orders by Id <<<");
                Console.WriteLine(">>> Get All Orders <<<");
                os.SortById();
                os.QueryAllOrders().ForEach(s => Console.WriteLine(s));

                Console.WriteLine(">>> GetOrdersByCustomerName:'Customer2' <<<");
                os.QueryByCustomerName("Customer2").ForEach(s => Console.WriteLine(s));

                Console.WriteLine(">>> GetOrdersByGoodsName:'apple' <<<");
                os.QueryByGoodsName("apple").ForEach(s => Console.WriteLine(s));

                Console.WriteLine(">>> Remove order(id=2) and qurey all <<<");
                os.RemoveOrder(2);
                os.QueryAllOrders().ForEach(od => Console.WriteLine(od));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static OrderService AddOrdersManually()
        {
            try
            {
                Customer customer1 = new Customer(1, "Customer1");
                Customer customer2 = new Customer(2, "Customer2");

                Goods milk = new Goods(1, "Milk", 69.9);
                Goods eggs = new Goods(2, "eggs", 4.99);
                Goods apple = new Goods(3, "apple", 5.59);

                OrderDetail orderDetails1 = new OrderDetail(1, apple, 8);
                OrderDetail orderDetails2 = new OrderDetail(2, eggs, 2);
                OrderDetail orderDetails3 = new OrderDetail(3, milk, 1);

                Order order1 = new Order(1, customer1);
                Order order2 = new Order(2, customer2);
                Order order3 = new Order(3, customer2);
                order1.AddDetails(orderDetails1);
                order1.AddDetails(orderDetails2);
                order1.AddDetails(orderDetails3);
                // order1.AddDetails(orderDetails3); // 订单明细不可重复
                order2.AddDetails(orderDetails2);
                order2.AddDetails(orderDetails3);
                order3.AddDetails(orderDetails3);

                OrderService os = new OrderService();
                os.AddOrder(order1);
                // os.AddOrder(order1); // 订单不可重复
                os.AddOrder(order2);
                os.AddOrder(order3);

                return os;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
