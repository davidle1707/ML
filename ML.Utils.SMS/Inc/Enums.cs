
namespace ML.Utils.SMS
{
    public enum SmsProvider : short
    {
        Twilio = 1,

        Tropo,

		Plivo

		//DONT MOVE POSITION ITEM
    }

    public enum SmsChannel : short
    {
        System = 1,
        Site
    }

    public enum SmsState : short
    {
        Success = 1,
		
        Unsuccess,

        Error

		//DONT MOVE POSITION ITEM
    }
}
