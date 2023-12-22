using System;
using System.Runtime.Serialization;

namespace ML.Common.Error
{
	[Serializable]
	[DataContract]
	public class ErrorManager
	{
		public ErrorManager()
		{
		}

		public ErrorManager(Exception ex) : this()
		{
			OriginalException = ex;
		}

		[DataMember]
		public string UniqueId { get; set; } = Guid.NewGuid().ToString();

        [DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Description { get; set; }

        [DataMember]
	    public bool SqlException { get; set; }

		[DataMember]
		public bool MarkLogicException { get; set; }

		[DataMember]
		public bool MongoDbException { get; set; }

		/// <summary>
		/// WCF: no expose this to client
		/// </summary>
		public Exception OriginalException { get; set; }
	}
}
