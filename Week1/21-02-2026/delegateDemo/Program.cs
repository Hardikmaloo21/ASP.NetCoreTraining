// using System;
// namespace delegateDemo
// {
//     public delegate void ArithmeticOperation(int a, int b);
//     class UsingDelegate
//     {   
//         static void Add(int a, int b)
//         {
//             Console.WriteLine(a + b);
//         }
//         static void Sub(int a, int b)
//         {
//             Console.WriteLine(a - b);
//         }
//         static void Mul(int a, int b)
//         {
//             Console.WriteLine(a * b);
//         }
//         static void Div(int a, int b)
//         {
//             Console.WriteLine(a / b);
//         }
//         static void Main(string[] args)
//         {
//             ArithmeticOperation obj = new ArithmeticOperation(Add);
//             obj += new ArithmeticOperation(Sub);
//             obj -= new ArithmeticOperation(Mul);
//             obj += new ArithmeticOperation(Div);
//             obj += new ArithmeticOperation(Mul);
//             obj(45, 30);
//             Console.ReadLine();           
//         }
//     }
// }


using System;
// namespace NS
// {
//     public delegate void NotificationSender(string message);
//     class Notifiers
//     {
//         static void SendEmail(string message)
//         {
//             Console.WriteLine(message);
//         }
//         static void SendSMS(string message)
//         {
//             Console.WriteLine(message);
//         }
//         static void SendPushNotification(string message)
//         {
//             Console.WriteLine(message);
//         }
//         public static void Main()
//         {
//             NotificationSender sender = new NotificationSender(SendEmail);
//             sender += new NotificationSender(SendSMS);
//             sender += new NotificationSender(SendPushNotification);
//             sender("Hello, world!");
//             Console.ReadLine();
//         }
//     }
    
// } 



// namespace NS
// {
//     public delegate void NotificationSender(string message);
//     class Notifiers
//     {
//         public void SendEmail(string message)
//         {
//             Console.WriteLine(message);
//         }
//         public void SendSMS(string message)
//         {
//             Console.WriteLine(message);
//         }
//         public void SendPushNotification(string message)
//         {
//             Console.WriteLine(message);
//         }
//         public static void Main()
//         {
//             Notifiers notifier = new Notifiers();
//             NotificationSender sender = notifier.SendEmail;
//             sender += notifier.SendSMS;
//             sender += notifier.SendPushNotification;
//             sender("Hello, world!");
//             Console.ReadLine();
//         }
//     }
    
// } 

