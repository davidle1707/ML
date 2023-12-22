using System;
using System.ServiceModel;

namespace ML.Common.Error
{
	public static class FaultExceptionHelper
	{
		public static FaultException<ErrorManager> CreateErrorManager(Exception ex)
		{
			return new FaultException<ErrorManager>(new ErrorManager(ex)
			                                        	{
			                                        		Description = ex.Message
			                                        	}, new FaultReason("An unhandled clr exception has occurred"));
		}

		public static FaultException<ErrorManager> CreateErrorManager(string message)
		{
			return CreateErrorManager(new ErrorManager
			                          	{
			                          		Description = message
			                          	});
		}

		public static FaultException<ErrorManager> CreateErrorManager(ErrorManager error)
		{
			return new FaultException<ErrorManager>(
				error,
				new FaultReason("Validation Failure,Inspect the detail property to get the details"));
		}

		public static Exception CreateValidationErrorMessage(FaultException<ErrorManager> exception)
		{
			return CreateErrorManager(exception.Detail);
		}
		
	}
}
