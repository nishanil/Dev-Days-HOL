using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using MyEvents.Helpers;
using MyEvents.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvents.Cloud
{
    public class AzureDataManager : IDataManager
    {
        static AzureDataManager defaultInstance = new AzureDataManager();
        MobileServiceClient client;

        private AzureDataManager()
        {
            Initialize();
        }

        public static AzureDataManager DefaultManager
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }

        #region Generic Azure Sync Table Helper Methods

        private void Initialize()
        {
            this.client = new MobileServiceClient(
               Constants.ApplicationURL);

            var store = new MobileServiceSQLiteStore("localstore.db");
            store.DefineTable<Session>();
            store.DefineTable<Speaker>();
            store.DefineTable<Feedback>();
            ConfigureAuth();
            //Initializes the SyncContext using the default IMobileServiceSyncHandler.
            this.client.SyncContext.InitializeAsync(store);
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>() where T : ModelBase
        {
            try
            {
                await this.SyncAsync<T>();

                return await this.client.GetSyncTable<T>().ToEnumerableAsync();
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SyncAsync<T>() where T : ModelBase
        {
            ReadOnlyCollection<MobileServiceTableOperationError> syncErrors = null;
            var identifier = typeof(T).Name;
            try
            {
                await this.client.SyncContext.PushAsync();
                await client.GetSyncTable<T>().PullAsync($"all{identifier}", this.client.GetSyncTable<T>().CreateQuery());
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult != null)
                {
                    syncErrors = exc.PushResult.Errors;
                }
            }

            // Simple error/conflict handling. A real application would handle the various errors like network conditions,
            // server conflicts and others via the IMobileServiceSyncHandler.
            if (syncErrors != null)
            {
                foreach (var error in syncErrors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
        }

        public async Task SaveItemAsync<T>(T item) where T : ModelBase
        {
            if (item.Id == null)
            {
                item.Id = Guid.NewGuid().ToString();
                await this.client.GetSyncTable<T>().InsertAsync(item);
            }
            else
            {
                await this.client.GetSyncTable<T>().UpdateAsync(item);
            }
        }

        #endregion

        #region IDataManager Methods
        public async Task<IEnumerable<Session>> GetSessionsAsync()
        {
            return await GetItemsAsync<Session>();
        }

        public async Task<IEnumerable<Speaker>> GetSpeakersAsync()
        {
            return await GetItemsAsync<Speaker>();
        }

        public async Task SaveSpeakerAsync(Speaker speaker)
        {
            await SaveItemAsync<Speaker>(speaker);
        }

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            await SaveItemAsync<Feedback>(feedback);
            await SyncAsync<Feedback>();
        }

        public async Task<IEnumerable<Feedback>> GetFeedbackAsync()
        {
            return await GetItemsAsync<Feedback>();
        }

        public void ConfigureAuth()
        {
            if (this.client.CurrentUser == null && Settings.UserId != null)
            {
                this.client.CurrentUser = new MobileServiceUser(Settings.UserId);
                this.client.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }
        }
        #endregion


    }
}
