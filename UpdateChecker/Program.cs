using System;
using System.Threading.Tasks;

namespace UpdateChecker
{
    class Program
    {
        void Main()
        {
            Waiter();
        }

        public async void Waiter()
        {
            while(true)
            {
                Console.WriteLine("OK");
                await Task.Delay(120000);
            }
        }
    }
}
