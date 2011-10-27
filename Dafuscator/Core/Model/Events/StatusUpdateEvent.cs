namespace WaveTech.Dafuscator.Model.Events
{
	public class StatusUpdateEvent
	{
		public string Message { get; set; }

		public StatusUpdateEvent()
		{
			
		}

		public StatusUpdateEvent(string message)
		{
			Message = message;
		}
	}
}