using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public class InvalidDocumentException : Exception
    {
        public InvalidDocumentException(string message)
            : base(message) { }
    }

    public interface IDocument
    {
        string DocumentNumber { get; }
        DateTime Date { get; }
        decimal Amount { get; }
        void Show();
    }

    public interface IUserInterface
    {
        void DisplayMessage(string message);
        string GetUserInput();
    }

    public class Document : IDocument
    {
        public string DocumentNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }

        public Document(string documentNumber, DateTime date, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(documentNumber))
            {
                throw new InvalidDocumentException("Invalid document number");
            }

            DocumentNumber = documentNumber;
            Date = date;
            Amount = amount;
        }

        public virtual void Show()
        {
            Console.WriteLine($"Document Number: {DocumentNumber}");
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Amount: {Amount:C}");
        }
    }

    public class Invoice : Document, IComparable<Invoice>, IUserInterface
    {
        protected string Seller { get; }
        protected string Buyer { get; }
        protected string Description { get; }

        public Invoice(
            string documentNumber,
            DateTime date,
            decimal amount,
            string seller,
            string buyer,
            string description
        )
            : base(documentNumber, date, amount)
        {
            Seller = seller;
            Buyer = buyer;
            Description = description;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Seller: {Seller}");
            Console.WriteLine($"Buyer: {Buyer}");
            Console.WriteLine($"Description: {Description}");
        }

        public int CompareTo(Invoice other)
        {
            return Date.CompareTo(other.Date);
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Invoice Message: {message}");
        }

        public string GetUserInput()
        {
            Console.Write("Enter Invoice User Input: ");
            return Console.ReadLine();
        }
    }

    public class Receipt : Document, IUserInterface
    {
        protected string Payer { get; }

        public Receipt(string documentNumber, DateTime date, decimal amount, string payer)
            : base(documentNumber, date, amount)
        {
            Payer = payer;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Payer: {Payer}");
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Receipt Message: {message}");
        }

        public string GetUserInput()
        {
            Console.Write("Enter Receipt User Input: ");
            return Console.ReadLine();
        }
    }

    public class Waybill : Document, IUserInterface
    {
        protected string Sender { get; }
        protected string Receiver { get; }
        protected List<string> Goods { get; }

        public Waybill(
            string documentNumber,
            DateTime date,
            decimal amount,
            string sender,
            string receiver,
            List<string> goods
        )
            : base(documentNumber, date, amount)
        {
            Sender = sender;
            Receiver = receiver;
            Goods = goods;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine($"Sender: {Sender}");
            Console.WriteLine($"Receiver: {Receiver}");
            Console.WriteLine("Goods:");
            foreach (var item in Goods)
            {
                Console.WriteLine($"- {item}");
            }
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine($"Waybill Message: {message}");
        }

        public string GetUserInput()
        {
            Console.Write("Enter Waybill User Input: ");
            return Console.ReadLine();
        }
    }

    public class Task1
    {
        public static void Task1_()
        {
            // Call constructor with invalid document number
            try
            {
                var invoice = new Invoice("", DateTime.Now, 100, "Seller", "Buyer", "Description");
            }
            catch (InvalidDocumentException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();

            // ArrayTypeMismatchException
            try
            {
                Console.WriteLine("Creating ArrayTypeMismatchException : ");
                object[] array = new string[10];
                array[0] = 10;
            }
            catch (ArrayTypeMismatchException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            Console.WriteLine();

            // DivideByZeroException
            try
            {
                Console.WriteLine("Creating DivideByZeroException : ");
                int a = 1;
                int b = 0;
                int c = a / b;
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            Console.WriteLine();

            // IndexOutOfRangeException
            try
            {
                Console.WriteLine("Creating IndexOutOfRangeException : ");
                int[] array = new int[10];
                array[10] = 10;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }

            Console.WriteLine();

            // InvalidCastException
            try
            {
                Console.WriteLine("Creating InvalidCastException : ");
                object a = 1;
                string b = (string)a;
            }
            catch (InvalidCastException e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }

            Console.WriteLine();

            // OutOfMemoryException
            try
            {
                Console.WriteLine("Creating OutOfMemory exception:");
                int hugeAmount = int.MaxValue - 1;
                int[] arr = new int[hugeAmount];
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            Console.WriteLine();

            // OverflowException
            try
            {
                Console.WriteLine("Creating Overflow exception:");
                int a = int.MaxValue;
                int b = 1;
                int result = checked(a + b);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($" Exception: {ex.Message}");
            }

            Console.WriteLine();

            // StackOverflowException
            try
            {
                Console.WriteLine("Creating StackOverflow exception:");

                void function()
                {
                    function();
                }
                //function();
                throw new StackOverflowException();
            }
            catch (StackOverflowException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}
