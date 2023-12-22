using System;
using ML.Utils.OnlineFantasyData.Entities;
using ML.Utils.OnlineFantasyData.Providers;
using System.Collections.Generic;
using System.Linq;

namespace ML.Utils.OnlineFantasyData
{
	public class OnlineFantasyDataManager
	{
		//USE ONLY 1 PROVIDER FOR NOW AND IT WILL BE CHANGE IN THE FEATURE

		#region Constructors

		private readonly bool _storeToFile;
		private readonly List<OnlineDataProvider> _dataProviders;

		public OnlineFantasyDataManager(bool storeToFile, OnlineDataProvider provider = null)
		{
			_storeToFile = storeToFile;
			_dataProviders = new List<OnlineDataProvider>();

			AddDataProvider(provider ?? new NumberFireProvider { StoreToFile = storeToFile });
		}

		#endregion

		public List<ProjectedPlayer> GetProjectedPlayers(string sport, DateTime? estDate = null)
		{
			return _dataProviders[0].GetProjectedPlayers(sport, estDate);
		}

		public List<Player> GetMostUsedPlayers(string sport, DateTime? estDate = null)
		{
			return _dataProviders[0].GetMostUsedPlayers(sport, estDate);
		}

		public void Download(string sport, DateTime? estDate = null)
		{
			foreach (var dataProvider in _dataProviders)
			{
				dataProvider.Download(sport, estDate);
			}
		}

		#region Private

		private void AddDataProvider(OnlineDataProvider provider)
		{
			if (_dataProviders.All(p => p.GetType() != provider.GetType()))
			{
				provider.StoreToFile = _storeToFile;

				_dataProviders.Add(provider);
			}
		}

		#endregion
	}
}
