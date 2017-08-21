using SitecoreEventLog.Website.DataAccess.Repositories;
using System;

namespace DemoClient
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var eventContext = new EventRepository();
            var temp = eventContext.GetEventsByItemId(new Guid("0795a7ed-91b0-4dff-805f-38cab60b6e4d"), 2, 1);

            var count = eventContext.CountEvents();
            count = eventContext.CountEventsByItemId(new Guid("0795a7ed-91b0-4dff-805f-38cab60b6e4d"));

            var asd = eventContext.GetEventItemById(15);
        }
    }
}