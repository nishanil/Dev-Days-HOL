using Microsoft.WindowsAzure.MobileServices;
using MyEvents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents
{
    public interface IDataManager
    {
        MobileServiceClient CurrentClient { get; }

        Task<IEnumerable<Session>> GetSessionsAsync();
        Task<IEnumerable<Speaker>> GetSpeakersAsync();

        Task SaveSpeakerAsync(Speaker speaker);

        Task SaveFeedbackAsync(Feedback feedback);

        Task<IEnumerable<Feedback>> GetFeedbackAsync();

    }
}
