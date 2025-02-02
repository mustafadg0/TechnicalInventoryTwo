using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TechnicalInventoryTwo;


class Program
{
    private static readonly ConcurrentQueue<Event> eventQueue = new();
    private static readonly List<Event> eventTable = new();
    private static readonly object alertLock = new();
    private static readonly Random random = new();
    private static bool isRunning = true;
    private static int eventCounter = 0;

    static async Task Main()
    {
        Task producerTask = RunEventProducer();
        Task consumerTask = RunEventConsumer();

        await Task.WhenAll(producerTask, consumerTask);
        Console.WriteLine("Tüm işlemler tamamlandı.");
    }

    private static async Task RunEventProducer()
    {
        while (eventCounter < 400 && isRunning)
        {
            var newEvent = new Event
            {
                Id = eventCounter,
                Priority = GetRandomPriority(),
                Timestamp = DateTime.Now
            };

            eventTable.Add(newEvent);
            eventQueue.Enqueue(newEvent);
            eventCounter++;

            Console.WriteLine($"[Producer] Event #{newEvent.Id} - {newEvent.Priority} eklendi.");
            await Task.Delay(3000); // 3 saniye bekle
        }
        isRunning = false;
    }

    private static async Task RunEventConsumer()
    {
        List<Event> buffer = new();

        while (isRunning || eventQueue.Count > 0)
        {
            if (eventQueue.TryDequeue(out var e))
            {
                buffer.Add(e);

                if (buffer.Count >= 3)
                {
                    var lastThree = buffer.TakeLast(3).ToList();
                    if (lastThree.All(ev => ev.Priority == lastThree[0].Priority))
                    {
                        GenerateAlert(lastThree);
                    }
                }
                await Task.Delay(5000); // 5 saniye event okuma süresi
            }
        }
    }

    private static void GenerateAlert(List<Event> events)
    {
        lock (alertLock)
        {
            Console.WriteLine($"[ALERT] 3 Ardışık {events[0].Priority} Event Tespit Edildi: {string.Join(", ", events.Select(e => e.Id))}");
        }
    }

    private static string GetRandomPriority()
    {
        string[] priorities = { "Düşük", "Orta", "Yüksek" };
        return priorities[random.Next(0, priorities.Length)];
    }

}
