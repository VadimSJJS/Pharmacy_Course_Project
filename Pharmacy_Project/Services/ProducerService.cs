using Microsoft.Extensions.Caching.Memory;
using Pharmacy_Project;
using Pharmacy_Project.Models;

namespace Pharmacy_Project.Services
{
    public class ProducerService
    {
        private readonly PharmacyContext _context;
        private readonly IMemoryCache cache;

        public ProducerService(PharmacyContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        public IEnumerable<Producer> GetDisciplineTypes()
        {
            if (!cache.TryGetValue("Producer", out IEnumerable<Producer> ProducerTypes))
            {
                ProducerTypes = SetProducer();
            }
            return ProducerTypes;
        }

        public IEnumerable<Producer> SetProducer()
        {
            var ProducerTypes = _context.Producers
        .ToList();
            foreach (var producerType in ProducerTypes)
            {
                _context.Entry(producerType).Collection(l => l.Medicines).Load();
            }
            cache.Set("Producer", ProducerTypes, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(100000)));
            return ProducerTypes;
        }
    }
}
