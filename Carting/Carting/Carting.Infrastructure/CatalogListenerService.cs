using Azure.Messaging.ServiceBus;

namespace CartingService.Carting.Infrastructure
{
    public class CatalogListenerService : BackgroundService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusProcessor _processor;
        private readonly ServiceBusProcessor _deadLetterQueueProcessor;

        public CatalogListenerService(ServiceBusClient serviceBusClient)
        {
            _serviceBusClient = serviceBusClient;

            var options =

            _processor = _serviceBusClient.CreateProcessor("update-catalog-data", new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 3,
                AutoCompleteMessages = false,
            });

            _deadLetterQueueProcessor = _serviceBusClient.CreateProcessor("update-catalog-data", new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 3,
                AutoCompleteMessages = false,
                SubQueue = SubQueue.DeadLetter
            });
        }

        public async Task MessageHandler(ProcessMessageEventArgs args)
        {
            
            var body = args.Message.Body.ToString();
            Console.WriteLine(body);
            
            await args.RenewMessageLockAsync(args.Message);
        }

        public async Task DlqMessageHandler(ProcessMessageEventArgs args)
        {

            var body = args.Message.Body.ToString();
            Console.WriteLine(body);
            Console.WriteLine(args.Message.DeadLetterReason);

            await args.CompleteMessageAsync(args.Message);
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
          
            Console.WriteLine(args.ErrorSource);
            
            Console.WriteLine(args.FullyQualifiedNamespace);
         
            Console.WriteLine(args.EntityPath);
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _processor.ProcessMessageAsync += MessageHandler;
            _processor.ProcessErrorAsync += ErrorHandler;

            _deadLetterQueueProcessor.ProcessMessageAsync += DlqMessageHandler;
            _deadLetterQueueProcessor.ProcessErrorAsync += ErrorHandler;

            await _processor.StartProcessingAsync(stoppingToken);
            await _deadLetterQueueProcessor.StartProcessingAsync(stoppingToken);
            Console.WriteLine("Starting ServiceBus Queue Listener");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping ServiceBus Queue Listener");
            await _processor.CloseAsync().ConfigureAwait(false);
            await _deadLetterQueueProcessor.CloseAsync().ConfigureAwait(false);
        }
    }
}
