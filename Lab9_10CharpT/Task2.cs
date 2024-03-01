using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9_10CharpT
{
    public delegate void StockEventHandler(object sender, StockEventArgs e);

    public class StockEventArgs : EventArgs
    {
        public string StockName { get; }
        public double CurrentPrice { get; }
        public string Message { get; }

        public StockEventArgs(string stockName, double currentPrice, string message)
        {
            StockName = stockName;
            CurrentPrice = currentPrice;
            Message = message;
        }
    }

    public class Stock
    {
        public event StockEventHandler Bullish; // Подія для "Биків"
        public event StockEventHandler Bearish; // Подія для "Ведмедів"

        private string stockName;
        private double currentPrice;
        private Random random;

        public Stock(string name, double initialPrice)
        {
            stockName = name;
            currentPrice = initialPrice;
            random = new Random();
        }

        protected virtual void OnBullish(StockEventArgs e)
        {
            Bullish?.Invoke(this, e);
        }

        protected virtual void OnBearish(StockEventArgs e)
        {
            Bearish?.Invoke(this, e);
        }

        public void SimulateMarketChanges()
        {
            for (int i = 0; i < 5; i++)
            {
                // Симулюємо випадкові зміни ціни акцій
                double percentageChange = (random.NextDouble() - 0.5) * 2; // Від -1 до 1
                currentPrice *= (1 + percentageChange);

                // Визначаємо, чи ціни ростуть чи падають
                if (percentageChange > 0)
                {
                    OnBullish(new StockEventArgs(stockName, currentPrice, "Prices are going up!"));
                }
                else
                {
                    OnBearish(new StockEventArgs(stockName, currentPrice, "Prices are falling!"));
                }

                System.Threading.Thread.Sleep(1000); // Затримка для візуалізації
            }
        }
    }

    public interface IStockTrader
    {
        void Subscribe(Stock stock);
        void Unsubscribe(Stock stock);

        void HandlStockEvent(object sender, StockEventArgs e);
    }

    public class Bull : IStockTrader
    {
        public void Subscribe(Stock stock)
        {
            stock.Bullish += HandlStockEvent;
        }

        public void Unsubscribe(Stock stock)
        {
            stock.Bullish -= HandlStockEvent;
        }

        public void HandlStockEvent(object sender, StockEventArgs e)
        {
            Console.WriteLine($"Bull: {e.StockName}, Price: {e.CurrentPrice}, {e.Message}");
        }
    }

    public class Bear : IStockTrader
    {
        public void Subscribe(Stock stock)
        {
            stock.Bearish += HandlStockEvent;
        }

        public void Unsubscribe(Stock stock)
        {
            stock.Bearish -= HandlStockEvent;
        }

        public void HandlStockEvent(object sender, StockEventArgs e)
        {
            Console.WriteLine($"Bear: {e.StockName}, Price: {e.CurrentPrice}, {e.Message}");
        }
    }

    public class Task2
    {
        public static void Task2_()
        {
            Stock stock = new Stock("Apple Stock", 200.0);
            Bull bull = new Bull();
            Bear bear = new Bear();

            bull.Subscribe(stock);
            bear.Subscribe(stock);

            stock.SimulateMarketChanges();

            bull.Unsubscribe(stock);
            bear.Unsubscribe(stock);
        }
    }
}
