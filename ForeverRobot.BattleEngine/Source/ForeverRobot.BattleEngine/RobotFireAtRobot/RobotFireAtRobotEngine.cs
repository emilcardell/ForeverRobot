using System.Collections.Generic;
using System.Threading;
using Raven.Client;
using StructureMap;
using TinyHandler;

namespace ForeverRobot.BattleEngine.RobotFireAtRobot
{
    public class RobotFireAtRobotEngine
    {
        private static IDocumentStore _documentStore;
        public static Queue<RobotFireAtRobotCommand> RobotFireAtRobotCommandQueue = new Queue<RobotFireAtRobotCommand>();

        public RobotFireAtRobotEngine(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void StartEngine()
        {
            LoadQueueFromPersistance();
            ThreadPool.QueueUserWorkItem(x => ProcessQueue());
            ThreadPool.QueueUserWorkItem(x => PersistQueue());
        }

        private void LoadQueueFromPersistance()
        {
            using(var session = _documentStore.OpenSession())
            {
                var persistedQueue = session.Load<PersistedQueue>("battleQueue");
                if (persistedQueue != null && persistedQueue.Queue.Count > 0)
                    RobotFireAtRobotCommandQueue = persistedQueue.Queue;
            }
        }

        public void PersistQueue()
        {
            while (true)
            {
                Thread.Sleep(5000);
                if (RobotFireAtRobotCommandQueue.Count == 0)
                    continue;

                using (var session = _documentStore.OpenSession())
                {
                    var persistedQueue = new PersistedQueue();
                    persistedQueue.Id = "battleQueue";
                    persistedQueue.Queue = RobotFireAtRobotCommandQueue;

                    session.Store(persistedQueue);
                    session.SaveChanges();
                }
            }

        }

        public void ProcessQueue()
        {
            HandlerCentral.Process(RobotFireAtRobotCommandQueue.Dequeue());
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }
    }

    public class PersistedQueue
    {
        public string Id { get; set; }
        public Queue<RobotFireAtRobotCommand> Queue { get; set; }
    }
}