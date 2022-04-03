using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public class QueueEntryRepository : BaseRepository<QueueEntry>
    {
        #region [Constructors]

        public QueueEntryRepository() : base() { }

        public QueueEntryRepository(Imis.Domain.IUnitOfWork uow) : base(uow) { }

        #endregion

        public bool TryGetPendingEntries(int batchSize, out List<QueueEntry> results, enQueueEntryType type)
        {
            results = BaseQuery.Where(x =>
                x.QueueEntryStatusInt == (int)enQueueEntryStatus.Running
                && x.QueueEntryTypeInt == (int)type
                && (x.RetryInterval == 0
                    || (x.RetryInterval != 0 && x.LastAttemptAt == null)
                    || (x.RetryInterval != 0 && x.LastAttemptAt != null && EntityFunctions.DiffSeconds(x.LastAttemptAt, DateTime.Now) > x.RetryInterval)))
                .OrderByDescending(x => x.QueueEntryPriorityInt)
                .ThenByDescending(x => x.ID)
                .Take(batchSize)
                .ToList();

            return results.Count != 0;
        }        
    }
}
