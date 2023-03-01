using BackendDevEvaluation.Contracts;

namespace BackendDevEvaluation.Services
{
    public class KeyService : IKeyService
    {
        private long ID = 1;

        public long GetNextKey()
        {
            return ID++;
        }
    }
}
