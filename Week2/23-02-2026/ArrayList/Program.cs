using System;
using System.Collections;

// namespace ArrayListDemo
// {
//     class UsingArrayList
//     {
//         static void Main(string[] args)
//         {
//             ArrayList listdata = new ArrayList();
//             listdata.Add(100);
//             listdata.Add(101);
//             listdata.Add(102);
//             listdata.Add(103);
//             listdata.Add(104);
//             listdata.Add("Done");
            
//             foreach (object item in listdata)
//             {
//                 Console.Write(item + " ");
//             }
//                 Console.WriteLine();

//             ArrayList listdata2 = new ArrayList();
//             listdata2.Add(100);
//             listdata2.Add(101);
//             listdata2.Add(102);
//             listdata2.Add(103);
//             listdata2.Add(104);
//             listdata2.Add("Done");
//             foreach (object i in listdata2)
//             {
//                 Console.Write(i + " ");
//             }
//         }
//     }
// }

// class String_list
// {
//     static void Main(string[] args)
//     {
//         string order = "Order#1001 | Laptop | Dell | 75000";
//         Console.WriteLine(order);

//         string trimOrder = order.Trim();
//         Console.WriteLine(trimOrder);
//         Console.WriteLine(order.Length);
//         Console.WriteLine(trimOrder.Length);

//         string[] parts = trimOrder.Split('|');
//         Console.WriteLine("After Split");
//         foreach (var item in parts)
//         {
//             Console.WriteLine(item.Trim());
//         }
//     }
// }


/*

Requirements:

Create an abstract class OrderProcessor

Properties:

OrderId

Amount

Abstract methods:

ProcessPayment()

GenerateInvoice()

SendNotification()

Concrete method:

DisplayOrderDetails()

Create a derived class OnlineOrder

Override all abstract methods.

Provide implementation for:

Online payment processing

Digital invoice generation

Email notification

In Main():

Create object using parent reference:

OrderProcessor order = new OnlineOrder();


Assign order details

Call all methods



*/


using System;

// namespace OrderProcessingDemo
// {
//     abstract class OrderProcessor
//     {
//         // Properties
//         public int OrderId { get; set; }
//         public double Amount { get; set; }

//         // Abstract Methods
//         public abstract void ProcessPayment();
//         public abstract void GenerateInvoice();
//         public abstract void SendNotification();

//         // Concrete Method
//         public void DisplayOrderDetails()
//         {

//             Console.WriteLine("Order Details:");
//             Console.WriteLine("Order ID: " + OrderId);
//             Console.WriteLine("Amount: " + Amount);
//         }
//     }
//     class OnlineOrder : OrderProcessor
//     {
//         public override void ProcessPayment()
//         {
//             Console.WriteLine("Processing payment");
//             Console.WriteLine("Payment Successful.");
//         }

//         public override void GenerateInvoice()
//         {
//             Console.WriteLine("Generating invoice.");
//         }

//         public override void SendNotification()
//         {
//             Console.WriteLine("Sending confirmation email to customer.");
//         }
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {

//             OrderProcessor order = new OnlineOrder();
//             order.OrderId = 101;
//             order.Amount = 2500.75;
//             order.DisplayOrderDetails();
//             order.ProcessPayment();
//             order.GenerateInvoice();
//             order.SendNotification();
//             Console.ReadLine();
//         }
//     }
// }


// class ExcDemo1
// {
//     public static void Main()
//     {
//         int[] nums = new int[4];
//         try
//         {
//             Console.WriteLine("Before exception is generated");
//             for (int i = 0; i < 10; i++)
//             {
//                 nums[i] = i;
//                 Console.WriteLine($"nums[{i}] = {nums[i]}");

//             }
//             Console.WriteLine("this won't be displayed");
//         }
//         catch (IndexOutOfRangeException ex)
//         {
//             Console.WriteLine("Exception caught");
//             Console.WriteLine(ex.Message);
//         }
//         Console.WriteLine("After exception is generated");
//     }
// }

// class Exedemo
// {
//     public static void Main()
//     {
//         int[] num = {4,8,14,32,64,128,256,512};
//         int[] deno = {2,0,4,4,0,8};

//         for(int i = 0; i < num.Length; i++)
//         {
//             try
//             {
//                 Console.WriteLine(num[i] + "/" + deno[i] +" is " + num[i] / deno[i]);
//             }
//             catch(DivideByZeroException ex)
//             {
//                 Console.WriteLine("Exception caught");
//                 Console.WriteLine(ex.Message);
//             }
//             catch(IndexOutOfRangeException ex)
//             {
//                 Console.WriteLine("Exception caught");
//                 Console.WriteLine(ex.Message);
//             }
//         }
//     }
// }

// class NestTrys
// {
//     public static void Main()
//     {
//         int[] num = {4,8,14,32,64,128,256,512};
//         int[] deno = {2,0,4,4,0,8};

//         for(int i = 0; i < num.Length; i++)
//         {
//             try
//             {
//                 Console.WriteLine(num[i] + "/" + deno[i] +" is " + num[i] / deno[i]);
//             }
//             catch(DivideByZeroException ex)
//             {
//                 Console.WriteLine("Exception caught");
//                 Console.WriteLine(ex.Message);
//             }
//             catch(IndexOutOfRangeException ex)
//             {
//                 Console.WriteLine("Exception caught");
//                 Console.WriteLine(ex.Message);
//             }
//         }
//     }
// }

// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Linq.Expressions;

// namespace CustomExceptionExampleCode
// {
//     class MyException : Exception
//     {
//         public MyException(string Messege) : base(Messege){}
//         public MyException(){}
//         public MyException(string Messege, Exception inner) : base(Messege, inner){}
//     }

//     class Program
//     {
//         static void Main(string[] args)
//         {
//             int a = 50;
//             int b = 10;
//             int k = a/b;
//             try
//             {
//                 if(k < 10)
//                 {
//                     throw new MyException("value of k is less than 10");
//                 }
//             }
//             catch (MyException e)
//             {
//                 Console.WriteLine("Caught My Exception");
//                 Console.WriteLine(e.Message);
//             }
//             Console.Read();

//         }
//     }
// }